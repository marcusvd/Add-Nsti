
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Services.Shared.Mappers.BaseMappers;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
    TSource Map(TDestination destination);
    IEnumerable<TDestination> Map(IEnumerable<TSource> sources);
    IEnumerable<TSource> Map(IEnumerable<TDestination> destinations);

}

public class BaseMapper<TSource, TDestination> : IMapper<TSource, TDestination>
{

    private static readonly Lazy<Action<TSource, TDestination>> _mapToDestination;
    private static readonly Lazy<Action<TDestination, TSource>> _mapToSource;

    static BaseMapper()
    {
        _mapToDestination = new Lazy<Action<TSource, TDestination>>(() => CreateMapping<TSource, TDestination>());
        _mapToSource = new Lazy<Action<TDestination, TSource>>(() => CreateMapping<TDestination, TSource>());
    }

    public virtual TDestination Map(TSource source)
    {
        if (source == null) return default;

        var destination = Activator.CreateInstance<TDestination>();

        _mapToDestination.Value(source, destination);

        return destination;
    }
    public virtual TSource Map(TDestination destination)
    {
        if (destination == null) return default;

        var source = Activator.CreateInstance<TSource>();
        _mapToSource.Value(destination, source);
        return source;

    }
    public virtual IEnumerable<TDestination> Map(IEnumerable<TSource> sources)
    {
        return sources?.Select(Map) ?? Enumerable.Empty<TDestination>();
    }
    public virtual IEnumerable<TSource> Map(IEnumerable<TDestination> destinations)
    {
        return destinations.Select(Map) ?? Enumerable.Empty<TSource>();
    }

    private static Action<TFrom, TTo> CreateMapping<TFrom, TTo>()
    {

        var fromProperties = typeof(TFrom).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var toProperties = typeof(TTo).GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.CanWrite)
        .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

        var sourceParam = Expression.Parameter(typeof(TFrom), "source");
        var destParam = Expression.Parameter(typeof(TTo), "dest");

        var assingnments = new List<Expression>();

        foreach (var fromProp in fromProperties)
        {
            if (toProperties.TryGetValue(fromProp.Name, out var toProp) &&
            toProp.PropertyType == fromProp.PropertyType)
            {
                var sourceProp = Expression.Property(sourceParam, fromProp);
                var destProp = Expression.Property(destParam, toProp);
                var assignment = Expression.Assign(destProp, sourceProp);
                assingnments.Add(assignment);
            }
        }

        var body = Expression.Block(assingnments);

        return Expression.Lambda<Action<TFrom, TTo>>(body, sourceParam, destParam).Compile();
    }
}