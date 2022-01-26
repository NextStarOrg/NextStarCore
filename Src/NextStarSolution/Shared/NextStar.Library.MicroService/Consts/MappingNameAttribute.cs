namespace NextStar.Library.MicroService.Consts;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field)]
public class MappingNameAttribute:Attribute
{
    public string Name { get; set; } = string.Empty;

    public MappingNameAttribute()
    {
        
    }

    public MappingNameAttribute(string name)
    {
        Name = name;
    }
}