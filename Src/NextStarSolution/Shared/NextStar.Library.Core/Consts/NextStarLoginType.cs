namespace NextStar.Library.Core.Consts;

[Flags]
public enum NextStarLoginType
{
    None = 2<<0,
    Google = 2<<1,
    Microsoft = 2<<2,
}