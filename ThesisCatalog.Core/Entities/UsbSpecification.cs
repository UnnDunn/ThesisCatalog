using System.Text;

namespace ThesisCatalog.Core.Entities;

public record UsbSpecification
{
    public UsbSpecification(Dictionary<UsbType, short> usbPorts)
    {
        UsbPorts = usbPorts;
    }

    public Dictionary<UsbType, short> UsbPorts { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var usbSpec in UsbPorts)
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