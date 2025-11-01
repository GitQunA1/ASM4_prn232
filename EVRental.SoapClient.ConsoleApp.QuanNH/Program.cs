// See https://aka.ms/new-console-template for more information
using EVRentalWCFServiceReference;

Console.WriteLine("Hello, World!");

ICheckOutQuanNhSoapService soapClient = new CheckOutQuanNhSoapServiceClient(CheckOutQuanNhSoapServiceClient.EndpointConfiguration.BasicHttpBinding_ICheckOutQuanNhSoapService);

try
{
    Console.WriteLine("SOAP Client >> GetAll:");
    var items = await soapClient.GetAllAsync();

    if (items != null && items.Length > 0)
    {
        Console.WriteLine($"Found {items.Length} items:");
        foreach (var item in items)
        {
            Console.WriteLine(string.Format("{0} - {1} - {2}", item.CheckOutQuanNhid, item.Notes, item.TotalCost));
        }
    }
    else
    {
        Console.WriteLine("No items found or items is null.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();