# Description

Import Product Attribute data from Optimziely Customized Commerce to your Optimizely Data Platform (ODP) Product Catalog. 

**For .NET 6, Optimizely CMS 12 and Optimizely Customized Commerce 14

# Installation

Install the package directly from the Optimizley Nuget repository.

```
dotnet add package First3Things.ODPProductAttributeConnector
```

## Configuration

### Startup

Startup.cs

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
[OdpProductSync]
[Searchable]
[CultureSpecific]
[Tokenize]
[IncludeInDefaultSearch]
[BackingType(typeof(PropertyString))]
[Display(Name = "Brand", GroupName = SystemTabNames.Content, Order = 15)]
public virtual string Brand { get; set; }
```

### Scheduled Job

Run the 'ODP Product Attribute Connector' scheduled job to sync attribute values to the ODP Product Catalog

![image](https://user-images.githubusercontent.com/19771039/221544669-fa35e11e-910b-450f-8621-8e3b64d60238.png)

# Version History

 |Version| Details|
 |:---|:---------------|
 |1.0|Initial Release|
