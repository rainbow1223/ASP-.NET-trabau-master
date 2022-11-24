using System.Xml.Serialization;

namespace TrabauClassLibrary.DLL.Models
{
    [XmlRoot("ContextMenu")]
    public class ContextMenu
    {
        [XmlElement("Menu")]
        public ContextMenuItems[] ContextMenuItems { get; set; }
    }

    public class ContextMenuItems
    {
        [XmlAttribute("Text")]
        public string Text { get; set; }

        [XmlAttribute("TargetNavId")]
        public string TargetNavId { get; set; }

        [XmlAttribute("IsChild")]
        public string IsChild { get; set; }
    }
}
