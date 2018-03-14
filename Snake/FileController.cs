using System.IO;
using System.Xml.Serialization;

namespace Snake
{
    public static class FileController
    {
        public static bool Exist(string fileName)
        {
            return File.Exists(fileName);
        }

        public static void Save<T>(T data, string file)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(file))
            {
                serializer.Serialize(writer, data);
            }
        }

        public static T Load<T>(string file)
        {
            T data;
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(file))
            {
                data = (T) serializer.Deserialize(reader);
            }
            return data;
        }
    }
}