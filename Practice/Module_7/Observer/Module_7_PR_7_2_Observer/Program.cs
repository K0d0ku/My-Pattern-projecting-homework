using Module_7_PR_7_2_Observer;
using System;
using static Module_7_PR_7_2_Observer.StockExchange;
/* it was hard to do , and some of the code in this specific task i searched thru internet
also i for the first time tried workin with classes separately and might just upload the whole project to git*/
public class program
{
    static async Task Main(string[] args)
    {
        string logFilePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\7\Module_7_PR_7_2_Observer\logs\trading_log.txt";
        string reportFilePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\practice\7\Module_7_PR_7_2_Observer\reports\subscriber_report.txt";
        /*UPDATE, the logs and reports have been executed to separate folders logs and reports within the project*/
        Logger logger = new Logger(logFilePath);
        StockExchange stockExchange = new StockExchange();

        Trader trader1 = new Trader("Kuro", 100.0, logger);
        Trader trader2 = new Trader("Baha", 200.0, logger);
        AutoTradingRobot robot = new AutoTradingRobot(100.0, 300.0, logger);
        
        stockExchange.Attach(trader1);
        stockExchange.Attach(trader2);
        stockExchange.Attach(robot);

        await stockExchange.UpdateStockPriceAsync("Nvidia", 250.0);
        await stockExchange.UpdateStockPriceAsync("Intel", 150.0);
        await stockExchange.UpdateStockPriceAsync("Nvidia", 390.0);
        await stockExchange.UpdateStockPriceAsync("Intel", 80.0);

        ReportGenerator reportGenerator = new ReportGenerator(reportFilePath);
        reportGenerator.GenerateReport(stockExchange.GetNotificationCounts(), new List<IObserver> { trader1, trader2, robot });
    }
}
