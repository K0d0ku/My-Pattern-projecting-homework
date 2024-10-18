using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7_PR_7_2_Observer
{
    public class Trader : IObserver
    {
        private string _name;
        private double _priceLim;
        private Logger _logger;   
        public Trader(string name, double priceLim, Logger logger)
        {
            _name = name;
            _priceLim = priceLim;
            _logger = logger;
        }
        public async Task Update(string stock, double price)
        {
            await Task.Delay(1000);
            string message = $"trader {_name} notified: {stock} is now {price}";
            Console.WriteLine(message);
            _logger.Log(message);
        }
        public bool PriceCondition(string stock, double price)
        {
            return price > _priceLim;
        }
    }
}