using System;
using System.Collections.Generic;

namespace NextStar.Library.AspNetCore.ApplicationDbModels
{
    public partial class ApplicationConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
