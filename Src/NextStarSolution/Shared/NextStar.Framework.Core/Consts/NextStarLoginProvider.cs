namespace NextStar.Framework.EntityFrameworkCore.Input.Consts
{
    public enum NextStarLoginProvider
    {
        None = 2 << 1,
        Microsoft = 2 << 2,
        Google = 2 << 3,
    }
}