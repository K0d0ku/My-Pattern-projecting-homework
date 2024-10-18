using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7_PR_7_2_Observer
{
    public class StockExchange : ISubject
    {
        private Dictionary<string, double> stocks = new Dictionary<string, double>();
        private List<IObserver> observers = new List<IObserver>();
        private Dictionary<IObserver, int> notifCount = new Dictionary<IObserver, int>();
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
            if (!notifCount.ContainsKey(observer))
            {
                notifCount[observer] = 0;
            }
        }
        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
            notifCount.Remove(observer);
        }
        public async Task AsyncNotif(string stock, double price)
        {
            var tasks = observers
                .Where(observer => observer.PriceCondition(stock, price))
                .Select(async observer =>
                {
                    await observer.Update(stock, price);
                    notifCount[observer]++;
                });
    
            await Task.WhenAll(tasks);
        }
        public async Task UpdateStockPriceAsync(string stock, double newPrice)
        {
            stocks[stock] = newPrice;
            await AsyncNotif(stock, newPrice);
        }
        public Dictionary<IObserver, int> GetNotificationCounts()
        {
            return notifCount;
        }
    }
}
