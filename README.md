# cs-api-dotnet
This library allows you to push/pull data out of the CaseStack Supply Chain Management System from your .NET apps.

For complete documentation visit [http://docs.casestack.io/](http://docs.casestack.io/)

## Installation

Install via nuget

	PM> Install-Package CaseStackAPI

You can also directly add the source into your Project Dependencies.

## Usage

**Authentication**

Before you can make any API calls, you'll have to authenticate and pick your environment. Get your keys under Settings > CaseStack API.

```C#
// establish connection
CaseStackApi api = new CaseStackApi();
api.Authenticate("<put your API Key here>", "<put your Company ID here>");

// set this to 'true' to operate on production data
api.UseProduction(false);
````

**Getting objects by their ID**
	
```C#
// get a customer by ID
const string customerId = "38fc6a35";
Customer customer = api.GetCustomer(customerId);
Console.WriteLine("Customer with ID '{0}' has name '{1}' accounting code '{2}'", customerId, customer.name, customer.billing.accounting_code);

// get a shipment by ID
const int shipmentId = 1;
Shipment shipment = api.GetShipment(shipmentId);
Console.WriteLine("Shipment with ID '{0}' has status '{1}'", shipmentId, shipment.status);

// get a carrier by ID
const string carrierId = "ec60fa20";
Carrier carrier = api.GetCarrier(carrierId);
Console.WriteLine("Carrier with ID '{0}' has name '{1}' and accounting code '{2}'", carrierId, carrier.name, carrier.billing.accounting_code);
```

**Locking shipment with ID '1'**

```C#
const int shipmentId = 1;    
Console.WriteLine("Locking Shipment with ID '{0}'", shipmentId);
api.LockShipment(shipmentId, true);
```

**Changing status of shipment with ID '1'**

```C#
const int shipmentId = 1;    
Console.WriteLine("Setting status of Shipment with ID '{0}' to 'Delivery Apt Scheduled'", shipmentId);
api.SetShipmentStatus(shipmentId, CaseStackApi.ShipmentStatus.DeliveryAppointmentScheduled);
```    

**Getting all Custom Fields.**

This only has to be done once. It can be retrieved and cached through the life-cycle of the application.

```C#
CustomFields carrierCustomFields = api.GetCustomFields("carrier");
CustomFields customerCustomFields = api.GetCustomFields("customer");
CustomFields shipmentCustomFields = api.GetCustomFields("shipment");
```

**Stitching together custom fields IDs and their labels**

This example uses LINQ.

```C#
// get a carrier by ID
const string carrierId = "ec60fa20";
Carrier carrier = api.GetCarrier(carrierId);
Console.WriteLine("Carrier with ID '{0}' has name '{1}' and accounting code '{2}'", carrierId, carrier.name, carrier.billing.accounting_code);

Console.WriteLine("Custom Fields for Carrier:");
foreach (string key in carrier.custom_fields.Keys)
{
    Field field = carrierCustomFields.fields.Single(s => s.id == key);
    String value = carrier.custom_fields[key];
    Console.WriteLine("\t => {0}: {1} ({2})", field.name, value, field.type);
}
```	
