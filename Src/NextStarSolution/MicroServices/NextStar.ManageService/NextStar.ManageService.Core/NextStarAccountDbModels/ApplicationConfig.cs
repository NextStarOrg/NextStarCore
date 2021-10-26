using System;
using System.Collections.Generic;

#nullable disable

namespace NextStar.ManageService.Core.NextStarAccountDbModels
{
    public partial class ApplicationConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
        public string ConfigKey { get; set; }
    }
}
