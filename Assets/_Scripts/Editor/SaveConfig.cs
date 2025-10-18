using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace StaticData
{
    
    public class SaveConfig
    {
        [MenuItem("Config/Create")]
        static void CreateConfig()
        {
#if UNITY_EDITOR
            string path = Application.dataPath + "/Resources/Configs";
#else
            string path = Application.dataPath + "/Configs";
#endif
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string json = JsonConvert.SerializeObject(new GridData(1));
            File.WriteAllText(path + "/config.json", json);

            Debug.Log(path);
        }
    }
}
