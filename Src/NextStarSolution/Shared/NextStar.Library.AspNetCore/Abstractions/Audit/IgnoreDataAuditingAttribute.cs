namespace NextStar.Library.AspNetCore.Abstractions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class IgnoreDataAuditingAttribute : Attribute
{
}