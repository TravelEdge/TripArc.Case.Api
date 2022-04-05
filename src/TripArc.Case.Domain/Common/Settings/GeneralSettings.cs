using System.Diagnostics.CodeAnalysis;

namespace TripArc.Case.Domain.Common.Settings
{
    [ExcludeFromCodeCoverage]
    public class GeneralSettings
    {
        public string Version { get; set; }
        public string Environment { get; set; }        
    }
}