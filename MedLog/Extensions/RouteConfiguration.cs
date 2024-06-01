using System.Text.RegularExpressions;

#pragma warning disable
namespace MedLog.Extensions
{
    public class RouteConfiguration : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            // Slugify value
            return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }

}
