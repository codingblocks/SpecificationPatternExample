using System;
using Patterns.Specification;
using SpecificationExample.Domains.Order;
using System.Linq;

namespace SpecificationExample.Specification
{
    public class UserCanDiscount<T> : CompositeSpecification<T> where T : IOrderAggregate
    {
        private Core.User _activeUser;

        public UserCanDiscount(Core.User activeUser)
        {
            _activeUser = activeUser;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return _activeUser.Roles.Any(x =>
                (
                    x == Core.User.Role.CustomerServiceManager
                    || x == Core.User.Role.CustomerServiceRepresentative
                )
               );

        }
    }
}
