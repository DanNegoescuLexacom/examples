// See https://aka.ms/new-console-template for more information
using PropertyComparison;

var address = new Address
{
    Street = "123 Main St",
    City = "Anytown",
    State = "WA",
    Zip = "12345"
};

var invoiceAddress = InvoiceAddress.FromAddress(address);

var addressProps = typeof(Address).GetProperties();
var invoiceAddressProps = typeof(InvoiceAddress).GetProperties();

foreach (var addressProp in addressProps)
{
    var invoiceAddressProp = invoiceAddressProps.FirstOrDefault(p => p.Name == addressProp.Name);
    if (invoiceAddressProp != null)
    {
        var addressValue = addressProp.GetValue(address);
        var invoiceAddressValue = invoiceAddressProp.GetValue(invoiceAddress);
        if (!addressValue.Equals(invoiceAddressValue))
        {
            // test failure
            Console.WriteLine($"{addressProp.Name} is different");
        }
    }
    else
    {
        // test failure
        Console.WriteLine($"{addressProp.Name} is missing");
    }
}
