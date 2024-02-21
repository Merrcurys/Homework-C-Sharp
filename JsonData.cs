using Newtonsoft.Json;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    public static class JsonData
    {
        public static void Serialize<T>(T notes)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "note.json"); // bin\Debug\

            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(path, json);
        }

        public static T Deserialize<T>()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "note.json");

            string json = File.ReadAllText(path);
            T notes = JsonConvert.DeserializeObject<T>(json);
            return notes;
        }
    }
}


