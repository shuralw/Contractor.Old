using System.IO;

namespace Contractor.CLI.Metamodell
{
    public class XmlClassDiagramReader
    {
        public Mxfile Read(string path)
        {
            return DeserializeToObject<Mxfile>(path);
        }
        public T DeserializeToObject<T>(string filepath) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(filepath))
            {
                return (T)ser.Deserialize(sr);
            }
        }
    }
}