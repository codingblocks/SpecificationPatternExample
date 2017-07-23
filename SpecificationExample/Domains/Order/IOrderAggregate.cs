using System;

namespace SpecificationExample.Domains.Order
{
    public interface IOrderAggregate
    {
        void DiscountByAmount(double amount);
        double GetTotalDiscounts();
        double GetOrderTotal();
	}
}
