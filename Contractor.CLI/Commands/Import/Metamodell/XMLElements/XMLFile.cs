using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Contractor.CLI.Metamodell
{
    public enum CellType
    {
        None,
        Class,
        Attribute,
        Association,
        AssociationEnd,
        Multiplicity
    }
    [XmlRoot(ElementName = "mxfile")]
    public class Mxfile
    {
        [XmlElement(ElementName = "diagram")]
        public Diagram Diagram { get; set; }


    }
    public class Diagram
    {
        [XmlElement("mxGraphModel")]
        public MxGraphModel MxGraphModel { get; set; }
    }

    public class MxGraphModel
    {
        [XmlElement("root")]
        public Root Root { get; set; }
    }

    public class Root
    {
        [XmlElement(ElementName = "mxCell")]
        public List<MxCell> MxCells { get; set; }
        public MxCell this[string id]
        {
            get { return MxCells.FirstOrDefault(s => string.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase)); }
        }


    }
    public class MxCell
    {
        public MxCell() { }

        public MxCell(string id, string value, string style, string parent, string vertex, string source, string target)
        {
            this.Id = id;
            this.Value = value;
            this.Style = style;
            this.Parent = parent;
            this.Vertex = vertex;
            this.SourceId = source;
            this.TargetId = target;
        }

        public CellType CellType { get; set; }
        public void DetermineType()
        {
            if (Id == "0" || Id == "1")
            {
                CellType = CellType.None;
            }
            else if (Style.StartsWith("swimlane"))
            {
                CellType = CellType.Class;

            }
            else if (Style.StartsWith("text"))
            {
                CellType = CellType.Attribute;
            }
            else if (Style.StartsWith("endArrow=open"))
            {
                CellType = CellType.Association;
            }
            else if (Style.StartsWith("resizable"))
            {
                CellType = CellType.AssociationEnd;
            }
            else if (Style.StartsWith("edgeLabel"))
            {
                CellType = CellType.Multiplicity;
            }
            else
            {
                string errorMessage = "Id: " + "ist kein gültiges Klassendiagrammelement. Der Style ist: " + Style;
                throw new Exception(errorMessage);
            }
        }

        [XmlElement(ElementName = "mxGeometry")]
        public MxGeometry MxGeometry { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "style")]
        public string Style { get; set; }

        [XmlAttribute(AttributeName = "parent")]
        public string Parent { get; set; }

        [XmlAttribute(AttributeName = "vertex")]
        public string Vertex { get; set; }

        [XmlAttribute(AttributeName = "source")]
        public string SourceId { get; set; }

        [XmlAttribute(AttributeName = "target")]
        public string TargetId { get; set; }
    }

    public class MxGeometry
    {
        [XmlAttribute(AttributeName = "x")]
        public string x { get; set; }

        [XmlAttribute(AttributeName = "y")]
        public string y { get; set; }

        [XmlAttribute(AttributeName = "width")]
        public string width { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public string height { get; set; }
    }
}
