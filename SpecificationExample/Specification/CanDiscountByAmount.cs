using System;
using Patterns.Specification;
using SpecificationExample.Domains.Order;
using System.Linq;


namespace SpecificationExample.Specification
{
    public class CanDiscountByAmount<T> : CompositeSpecification<T> where T : IOrderAggregate
    {
        private readonly Core.User _activeUser;
        private readonly double _additionalDiscount;
        private readonly double _userDiscountThreshold = 0;
        private T _order;

        public CanDiscountByAmount(Core.User activeUser, double additionalDiscount)
        {
            _activeUser = activeUser;
            _additionalDiscount = additionalDiscount;
            _userDiscountThreshold = getUserDiscountThreshold();
        }

        public override bool IsSatisfiedBy(T o)
        {
            _order = o;

            if (getDoesOrderHaveValue() == false){
                return false;
            }

            if(getDoesAdditionalDiscountMakeOrderValueNegative() == true){
                return false;
            }

            if(getDoesNewDiscountExceedUserThreshold() == true){
                return false;
            }

            return true;
        }

        private bool getDoesOrderHaveValue(){
            return _order.GetOrderTotal() > 0;
        }

        private bool getDoesAdditionalDiscountMakeOrderValueNegative(){
            return (_order.GetOrderTotal() - (_order.GetTotalDiscounts() + _additionalDiscount)) < 0;
        }

        private bool getDoesNewDiscountExceedUserThreshold(){
			var oldDiscountsAndNewDiscountTotal = _order.GetTotalDiscounts() + _additionalDiscount;

            return (oldDiscountsAndNewDiscountTotal / _order.GetOrderTotal()) > _userDiscountThreshold;
		}

        private double getUserDiscountThreshold(){
            double threshold = 0;

            if(getIsUserACustomerServiceRepresentative()){
                threshold = Math.Max(.15, threshold);
            }

            if(getIsUserACustomerServiceManager()){
                threshold = Math.Max(.35, threshold);
            }

            return threshold;
        }

        private bool getIsUserACustomerServiceManager(){
            return _activeUser.Roles.Contains(Core.User.Role.CustomerServiceManager);
        }

        private bool getIsUserACustomerServiceRepresentative(){
            return _activeUser.Roles.Contains(Core.User.Role.CustomerServiceRepresentative);
        }

    }
}
