using System;
using System.IO;
using System.Xml.Serialization;

namespace Diablo_Wannabe.XmlSerialization
{
    public class XmlSerializer<T>
    {
        public Type Type { get; set; }

        public T Load(string path)
        {
            T serialize;
            using (var reader = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                serialize = (T)xml.Deserialize(reader);
            }
            return serialize;
        }

        public void Save(string path, object obj)
        {
            using (var writer = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}