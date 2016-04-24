using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace MyUtilities
{
    public static class DeepSerialization
    {
        public static void Serialize<T>(string address, T data)
        {
            var mySerializer = new DataContractSerializer(typeof(T));
            using (var writer = XmlWriter.Create(address, new XmlWriterSettings { Indent = true }))
            {
                mySerializer.WriteObject(writer, data);
            }
        }

        public static bool Deserialize<T>(string address, ref T data)
        {
            try
            {
                var mySerializer = new DataContractSerializer(typeof(T));

                FileStream myFileStream = new FileStream(address, FileMode.Open);
                data = (T)mySerializer.ReadObject(myFileStream);
                myFileStream.Close();

                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }
}
