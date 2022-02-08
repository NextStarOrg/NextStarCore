﻿using System;
using System.Collections.Generic;

namespace NextStar.SystemService.Core.ManagementDbModels
{
    public partial class ApplicationConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
