﻿using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Laboratory_Schedule.Resource
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;
        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }
        public LocalizedString Loc(String key)
        {
            return _localizer[key];
        }
    }
}
