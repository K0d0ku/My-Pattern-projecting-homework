using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7_PR_7_2_Observer
{
    public interface IObserver
    {
        Task Update(string stock, double price);
        bool PriceCondition(string stock, double price);
    }
}