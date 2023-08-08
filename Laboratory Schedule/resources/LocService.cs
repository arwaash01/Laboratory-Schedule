using DocumentFormat.OpenXml.Drawing;
using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Laboratory_Schedule.resources
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
