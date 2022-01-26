﻿using NextStar.Library.MicroService.Inputs;

namespace NextStar.SystemService.Core.Entities.ApplicationConfig;

public class SelectInput:PageSearchTextInput
{
    public string Environment { get; set; } = string.Empty;
}