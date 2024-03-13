using System.Text;
using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.Core.Extensions;

public static class CatalogExtensions
{
    public static string ToString(this Dictionary<UsbType, int> usbSpecification)
    {
        var sb = new StringBuilder();
        foreach (var usbSpec in usbSpecification)
        {
            var usbTypeString = usbSpec.Key switch
            {
                UsbType.USB2 => "USB 2.0",
                UsbType.USB3 => "USB 3.0",
                UsbType.USBC => "USB C",
                _ => string.Empty
            };

            sb.Append($"{usbSpec.Value} x {usbTypeString}, ");
        }

        return sb.ToString().TrimEnd(' ', ',');
    }
}