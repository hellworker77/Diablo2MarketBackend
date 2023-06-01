using Entities;
using Filters.Abstractions;
using Filters.Deals;

namespace Filters
{
    public class DealFilterSpecifications
    {
        public static AbstractFilterSpecification<Deal> Last24Hours = new Last24HoursDealsSpecification();
        public static AbstractFilterSpecification<Deal> InProgress = new InProgressDealsSpecification();
        public static AbstractFilterSpecification<Deal> Suspended = new SuspendedDealsSpecification();
        public static AbstractFilterSpecification<Deal> Success = new SuccessDealsSpecification();
    }
}
