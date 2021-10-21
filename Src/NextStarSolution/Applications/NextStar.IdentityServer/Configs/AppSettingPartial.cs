using System.Collections.Generic;
using IdentityServer4.Models;
using NextStar.Framework.Core.Consts;

namespace NextStar.IdentityServer.Configs
{
    public class AppSettingPartial:AppSetting
    {
        public IdentityServer IdentityServer { get; set; }
        public Certificates Certificates { get; set; }
    }

    public class IdentityServer
    {
        public List<Client> Clients { get; set; }
    }
    public class Certificates
    {
        public string Path { get; set; }
        public string Password { get; set; }
    }

}