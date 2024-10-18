using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7_PR_7_2_Observer
{
    public class AutoTradingRobot : IObserver
    {
        private double _buyLim;
        private double _sellLim;
        private Logger _logger;
        public AutoTradingRobot(double buyLim, double sellLim, Logger logger)
        {
            _buyLim = buyLim;
            _sellLim = sellLim;
            _logger = logger;
        }
        public async Task Update(string stock, double price)
        {
            await Task.Delay(500);
            string message = string.Empty;
            if (price <= _buyLim)
            {
                message = $"AutoTradingRobot: buying {stock} at {price}";
            }
            else if (price >= _sellLim)
            {
                message = $"AutoTradingRobot: selling {stock} at {price}";
            }
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
                _logger.Log(message);
            }
        }
        public bool PriceCondition(string stock, double price)
        {
            return price <= _buyLim || price >= _sellLim;
        }
    }
}