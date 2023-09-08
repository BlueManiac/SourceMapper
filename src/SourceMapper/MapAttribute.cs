namespace SourceMapper;

[AttributeUsage(AttributeTargets.Class)]
public class MapAttribute<T> : Attribute where T : class
{
}