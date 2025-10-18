using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace StaticData
{
    public class LoadConfig
    {

        public GridData Load()
        {
#if UNITY_EDITOR
            string path = Application.dataPath + "/Resources/Configs";
#else
            string path = Application.dataPath + "/Configs";
#endif

            string json;
            if (File.Exists(path + "/config.json"))
            {
                json = File.ReadAllText(path + "/config.json");
                return JsonConvert.DeserializeObject<GridData>(json);
            }
            return new GridData(0);
        }
    }
}
