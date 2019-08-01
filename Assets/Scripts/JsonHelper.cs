using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    public class JsonHelper
    {
        public string[] ReadData(string path)
        {
            return File.ReadAllLines(path);
        }

        public CountryInfo GetCountynfoFromJson(string line)
        {
            return JsonUtility.FromJson<CountryInfo>(line);
        }

        public string ConvertToJson(CountryInfo countryInfo)
        {
            return JsonUtility.ToJson(countryInfo);
        }

        public void WriteData(CountryInfo[] countries, string path)
        {
            var json = new string[countries.Length];
            for (int i = 0; i < countries.Length; i++)
            {
                json[i] = ConvertToJson(countries[i]);
            }
            File.WriteAllLines(path, json);
        }

        public CountryInfo[] GetCountiesFromJson(string path)
        {
            var lines = ReadData(path);
            var counties = new CountryInfo[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                counties[i] = GetCountynfoFromJson(lines[i]);
            }

            return counties;
        }

        public void CheckJsonExist(string fileName)
        {
            var path = Path.Combine(Application.persistentDataPath, fileName);

            //if (!File.Exists(path))
            //{
                var json = Resources.Load(fileName) as TextAsset;
                File.WriteAllText(path, json.text);
            //}
        }
    }
}
