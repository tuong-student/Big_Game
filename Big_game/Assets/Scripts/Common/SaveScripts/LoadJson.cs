using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Save
{
    public class LoadJson<T> where T : class
    {
        private static readonly string SAVE_PATH = Application.dataPath + "/Saves/";

        public static T LoadFromJson(string SAVE_FILE_NAME)
        {
            T newJsonObject = null;
            if (File.Exists(SAVE_PATH + SAVE_FILE_NAME))
            {
                string saveString = File.ReadAllText(SAVE_PATH + SAVE_FILE_NAME);
                Debug.Log(saveString);
                newJsonObject = JsonConvert.DeserializeObject<T>(saveString);
            }
            else
            {
                Debug.Log(SAVE_FILE_NAME + " file not exist");
            }

            return newJsonObject;
        }
    }
}
