namespace NextStar.Library.Core.Consts;

[Flags]
public enum NextStarLoginType
{
    None = 1<<0,
    Google = 1<<1,
    Microsoft = 1<<2,
}