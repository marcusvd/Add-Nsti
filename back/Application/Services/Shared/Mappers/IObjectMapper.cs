
namespace Application.Services.Shared.Mappers.BaseMappers;

public interface IObjectMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources);
}