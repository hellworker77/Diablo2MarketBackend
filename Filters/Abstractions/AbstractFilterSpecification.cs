using System.Linq.Expressions;

namespace Filters.Abstractions
{
    public abstract class AbstractFilterSpecification <T>
    {
        public abstract Expression<Func<T, bool>> SpecificationExpression { get; }
    }
}
