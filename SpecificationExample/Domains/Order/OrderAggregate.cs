using System;
namespace SpecificationExample.Domains.Order
{
    public class OrderAggregate : IOrderAggregate
    {
        private double _orderTotal;
        private double _totalDiscounts;

        public OrderAggregate(double orderTotal, double totalDiscounts){
            _orderTotal = orderTotal;
            _totalDiscounts = totalDiscounts;
        }

        public void DiscountByAmount(double amount){
            
        }

        public double GetTotalDiscounts(){
            return _totalDiscounts;
        }

        public double GetOrderTotal(){
            return _orderTotal;
        }
    }
}
