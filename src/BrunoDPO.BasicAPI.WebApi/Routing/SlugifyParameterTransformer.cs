﻿using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace BrunoDPO.BasicAPI.WebApi.Routing
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value) =>
            value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
