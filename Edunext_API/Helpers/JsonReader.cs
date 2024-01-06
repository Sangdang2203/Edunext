using Edunext_API.Models;
using Newtonsoft.Json;

namespace Edunext_API.Helpers
{
    public class JsonReader
    {
        public static IEnumerable<Address> ReadFile(string path, string filename)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), path, filename);

            IEnumerable<Address> addresses = new List<Address>();

            using (var streamReader = new StreamReader(filepath))
            {
                string json = streamReader.ReadToEnd();
                addresses = JsonConvert.DeserializeObject<IEnumerable<Address>>(json);
            }

            return addresses;
        }
    }
}
