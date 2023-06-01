using Entities;
using Filters.Abstractions;
using System.Linq.Expressions;

namespace Filters.Deals
{
    internal class Last24HoursDealsSpecification : AbstractFilterSpecification<Deal>
    {
        public override Expression<Func<Deal, bool>> SpecificationExpression =>
            (deal) => deal.Status == Entities.Enums.DealStatus.Success && deal.UpdatedStatusTime.AddHours(24).ToUniversalTime() >= DateTime.Now.ToUniversalTime();
    }
}
