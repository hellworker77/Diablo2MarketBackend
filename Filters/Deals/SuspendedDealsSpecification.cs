﻿using Entities;
using Filters.Abstractions;
using System.Linq.Expressions;

namespace Filters.Deals
{
    internal class SuspendedDealsSpecification : AbstractFilterSpecification<Deal>
    {
        public override Expression<Func<Deal, bool>> SpecificationExpression =>
            (deal) => deal.Status == Entities.Enums.DealStatus.Suspended;
    }
}