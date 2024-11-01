/*output:
choose a delivery service:
1. Internal Delivery Service
2. External Logistics Service A
3. External Logistics Service B
Enter your choice (1-3): 2
shipping item 404 by External Logistics Service A.
11/2/2024 12:59:06 AM: order 404 delivered by External Logistics Service A.
11/2/2024 12:59:06 AM: checked delivery status for order 404 by External Logistics Service A.
tracking shipment 404 by External Logistics Service A: in Transit.
*/

using System;
/*Adapter*/
public class program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("choose a delivery service:");
        Console.WriteLine("1. Internal Delivery Service");
        Console.WriteLine("2. External Logistics Service A");
        Console.WriteLine("3. External Logistics Service B");
        Console.Write("Enter your choice (1-3): ");
        string input = Console.ReadLine();
        IInternalDeliveryService deliveryService;

        switch (input)
        {
            case "1":
                deliveryService = DeliveryServiceFactory.CreateDeliveryService("internal");
                break;
            case "2":
                deliveryService = DeliveryServiceFactory.CreateDeliveryService("externalA");
                break;
            case "3":
                deliveryService = DeliveryServiceFactory.CreateDeliveryService("externalB");
                break;
            default:
                Console.WriteLine("non existing choice, restart.");
                return;
        }
        string orderId = "404";
        deliveryService.DeliverOrder(orderId);
        Console.WriteLine(deliveryService.GetDeliveryStatus(orderId));
    }
}
public interface IInternalDeliveryService
{
    void DeliverOrder(string orderId);
    string GetDeliveryStatus(string orderId);
}
public class InternalDeliveryService : IInternalDeliveryService
{
    public void DeliverOrder(string orderId)
    {
        Console.WriteLine($"delivering order {orderId} by internal delivery service.");
    }
    public string GetDeliveryStatus(string orderId)
    {
        return $"status of order {orderId}: delivered.";
    }
}
public class ExternalLogisticsServiceA
{
    public void ShipItem(int itemId)
    {
        Console.WriteLine($"shipping item {itemId} by External Logistics Service A.");
    }
    public string TrackShipment(int shipmentId)
    {
        return $"tracking shipment {shipmentId} by External Logistics Service A: in Transit.";
    }
}
public class ExternalLogisticsServiceB
{
    public void SendPackage(string packageInfo)
    {
        Console.WriteLine($"sending package with info '{packageInfo}' by External Logistics Service B.");
    }
    public string CheckPackageStatus(string trackingCode)
    {
        return $"status of package with tracking code {trackingCode} by External Logistics Service B: delivered.";
    }
}
public class LogisticsAdapterA : IInternalDeliveryService
{
    private readonly ExternalLogisticsServiceA _externalService;
    private readonly Action<string> _logAction;
    public LogisticsAdapterA(ExternalLogisticsServiceA externalService, Action<string> logAction)
    {
        _externalService = externalService;
        _logAction = logAction;
    }
    public void DeliverOrder(string orderId)
    {
        int itemId = int.Parse(orderId);
        _externalService.ShipItem(itemId);
        _logAction($"order {orderId} delivered by External Logistics Service A.");
    }
    public string GetDeliveryStatus(string orderId)
    {
        int shipmentId = int.Parse(orderId);
        string status = _externalService.TrackShipment(shipmentId);
        _logAction($"checked delivery status for order {orderId} by External Logistics Service A.");
        return status;
    }
}
public class LogisticsAdapterB : IInternalDeliveryService
{
    private readonly ExternalLogisticsServiceB _externalService;
    private readonly Action<string> _logAction;
    public LogisticsAdapterB(ExternalLogisticsServiceB externalService, Action<string> logAction)
    {
        _externalService = externalService;
        _logAction = logAction;
    }
    public void DeliverOrder(string orderId)
    {
        _externalService.SendPackage(orderId);
        _logAction($"order {orderId} delivered by External Logistics Service B.");
    }
    public string GetDeliveryStatus(string orderId)
    {
        string status = _externalService.CheckPackageStatus(orderId);
        _logAction($"checked delivery status for order {orderId} by External Logistics Service B.");
        return status;
    }
}
public class DeliveryServiceFactory
{
    public static IInternalDeliveryService CreateDeliveryService(string type)
    {
        return type switch
        {
            "internal" => new InternalDeliveryService(),
            "externalA" => new LogisticsAdapterA(new ExternalLogisticsServiceA(), Logger.LogAction),
            "externalB" => new LogisticsAdapterB(new ExternalLogisticsServiceB(), Logger.LogAction),
            _ => throw new ArgumentException("non existing delivery service, restart.")
        };
    }
}
public static class Logger
{
    private static string _logFilePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\9\logs\log.txt";
    public static void LogAction(string message)
    {
        string logMessage = $"{DateTime.Now}: {message}";
        Console.WriteLine(logMessage);
        File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
    }
}
