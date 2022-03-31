using NextStar.Library.MicroService.Consts;

namespace NextStar.Library.MicroService.Extensions;

public static class EnumExtensions
{
    public static string GetMappingName(this Enum en)
    {
        var type = en.GetType();
        var name = Enum.GetName(type, en);
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var field = type.GetField(name);
        if (field == null)
            return string.Empty;

        if (!(Attribute.GetCustomAttribute(field, typeof(MappingNameAttribute)) is MappingNameAttribute descAttr))
            return string.Empty;

        return descAttr.Name;
    }
}