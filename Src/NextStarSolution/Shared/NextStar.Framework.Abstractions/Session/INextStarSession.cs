using System;

namespace NextStar.Framework.Abstractions.Session
{
    public interface INextStarSession
    {
        Guid? UserKey { get; }
        Guid? SessionId { get; }
        string Name { get; }
        string Email { get; }
        long? Phone { get; }
    }
}