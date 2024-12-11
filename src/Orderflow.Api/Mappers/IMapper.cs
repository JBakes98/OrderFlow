namespace Orderflow.Mappers;

public interface IMapper<in TSource, out TDestination>
{
    TDestination Map(TSource source);
}