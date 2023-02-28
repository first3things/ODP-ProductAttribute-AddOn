# Description

Import Product Attribute data from Optimizely Customized Commerce to your Optimizely Data Platform (ODP) Product Catalog. 

**For .NET 5/.NET 6, Optimizely CMS 12 and Optimizely Customized Commerce 14**

# Installation

Install the package directly from the Optimizley Nuget repository.

```
dotnet add package First3Things.ODPProductAttributeConnector
```

## Configuration

### Startup.cs

```
using First3Things.ODPProductAttributeConnector.DependencyInjection;
```
...
```
services.AddOdpProductAttributeConnector(_configuration);
```

### Product Attributes

Add the [OdpProductSync] attribute to Product and Variant Content Type properties that you want to sync to ODP

```
[OdpProductSync("brand")]
[Searchable]
[CultureSpecific]
[Tokenize]
[IncludeInDefaultSearch]
[BackingType(typeof(PropertyString))]
[Display(Name = "Brand", GroupName = SystemTabNames.Content, Order = 15)]
public virtual string Brand { get; set; }
```

Set the value in the attribute constructor to the field name of the product attribute in ODP

![image](https://user-images.githubusercontent.com/19771039/221570297-62d4a39e-ab1a-4f28-94ae-5dbb546e7e1b.png)


### Scheduled Job

Run the 'ODP Product Attribute Connector' scheduled job to sync attribute values to the ODP Product Catalog

![image](https://user-images.githubusercontent.com/19771039/221544669-fa35e11e-910b-450f-8621-8e3b64d60238.png)

# Notes

## Retrive Catalog Business Logic

Multiple catalogs are not supported out of the box.

The business logic executed by the Schedyled Job picks the first Catalog.

If you need to overwrite this logic, inject a new implementation for ICatalogService.GetCatalogRoot()

## ODP Commerce Cloud Connecotor (App Directory)

This package is responseible for updating the Attribute Values of Products and Variants only. It does not affect the Product / Variant relationships. 

It is recommended to use the Commerce Cloud connector in the ODP App Directory to maintain Product / Variant relationships as well as other data it maintains such as Product  Name, Image and Price.

Read more on the ODP Commerce Cloud Connector: https://docs.developers.optimizely.com/digital-experience-platform/v1.5.0-optimizely-data-platform/docs/import-data-from-optimizely-commerce-cloud#commerce-cloud-and-odp-fields

## Useful Documentation

Product Batch Request API: https://docs.developers.optimizely.com/digital-experience-platform/v1.5.0-optimizely-data-platform/reference/batch-requests

Recommended Product Fields: https://docs.developers.optimizely.com/digital-experience-platform/v1.5.0-optimizely-data-platform/docs/usecase-products#recommended-fields

# Version History

 |Version| Details|
 |:---|:---------------|
 |1.0| Initial Release|
 |1.1| Added "FieldName" property to OdpProductSyncAttribute. Set this to ODP Field Name for each property that is synced|
 |1.2| Batched Product API requests per 500 products. Imporved error logging and Job History message|
