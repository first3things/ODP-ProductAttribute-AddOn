using System.Reflection;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using First3Things.ODPProductAttributeConnector.Service;
using Microsoft.Extensions.Logging;

namespace First3Things.ODPProductAttributeConnector;

[ScheduledPlugIn(   
    DisplayName = "ODP Product Attribute Connector", 
    Description = "Synchronise Product Attribute Values to ODP", 
    GUID = "5fc97774-4a18-4682-bf30-04e3547b73b0",
    SortIndex = 2000)]
public class OdpProductCatalogScheduledJob : ScheduledJobBase
{
    private bool _stopSignaled;
    private readonly IProductContentTypeRepository _productContentTypeRepository;
    private readonly ILogger<OdpProductCatalogScheduledJob> _logger;

    public OdpProductCatalogScheduledJob(IProductContentTypeRepository productContentTypeRepository, ILogger<OdpProductCatalogScheduledJob> logger)
    {
        _productContentTypeRepository = productContentTypeRepository;
        _logger = logger;
    }

    public override string Execute()
    {
        //Call OnStatusChanged to periodically notify progress of job for manually started jobs
        OnStatusChanged($"Starting execution of {this.GetType()}");

        try
        {
            // get product types
            var productContentTypes = _productContentTypeRepository.GetProductContentTypes();

            // get properties that we need to sync
            Dictionary<ContentType, IEnumerable<PropertyInfo>> contentTypes = new Dictionary<ContentType, IEnumerable<PropertyInfo>>();
            foreach (var contentType in productContentTypes)
            {
                var propertyInfos = _productContentTypeRepository.GetContentTypeProperties(contentType);

                if (propertyInfos.Any())
                {
                    contentTypes.Add(contentType, propertyInfos);
                }
            }

            // loop content types and invoke CatalogSyncService
            foreach (var contentType in contentTypes)
            {
                Type[] productType = new Type[] {Type.GetType(contentType.Key.ModelTypeString)};

                var mi = typeof(OdpCatalogSyncService).GetMethod("ProcessProductContentType");
                var method = mi.MakeGenericMethod(productType);

                method.Invoke(new OdpCatalogSyncService(), new [] {contentType.Value});
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "OdpProductCatalogScheduledJob - Unhandled Exception ");
            throw;
        }

        //For long running jobs periodically check if stop is signaled and if so stop execution
        if (_stopSignaled)
        {
            return "Stop of job was called";
        }

        return "Products Attributes have been successfully synced to ODP";
    }

    public override void Stop() => _stopSignaled = true;
}