using System;
using SpecificationExample.Domains.Order;

namespace SpecificationExample.Specification
{
    public static class Factory<T> where T : IOrderAggregate
    {
        public static UserCanDiscount<T> GetUserCanDiscountSpecification(SpecificationExample.Core.User activeUser)
        {
            return new UserCanDiscount<T>(activeUser);
        }

        public static CanDiscountByAmount<T> GetCanDiscountSpecification(SpecificationExample.Core.User activeUser, double additionalDiscount){
            return new CanDiscountByAmount<T>(activeUser, additionalDiscount);
        }

        public static Patterns.Specification.ExpressionSpecification<T> GetUserCanDiscountByAmountSpecification(Core.User activeUser, double additionalDiscount){
            return new Patterns.Specification.ExpressionSpecification<T>(o => {
                var userCanDiscountSpec = GetUserCanDiscountSpecification(activeUser);
                var canDiscountByAmountSpec = GetCanDiscountSpecification(activeUser, additionalDiscount);

                return userCanDiscountSpec.IsSatisfiedBy(o) == true && canDiscountByAmountSpec.IsSatisfiedBy(o) == true;
            });
        }
    }
}
