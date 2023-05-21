using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Game.Save
{
    public class SaveJson
    {
        private static readonly string SAVE_PATH = Application.dataPath + "/Saves/";

        public static void SaveToJson(object objectToSave, string SAVE_FILE_NAME)
        {
            if (!Directory.Exists(SAVE_PATH))
            {
                Directory.CreateDirectory(SAVE_PATH);
            }


            string jsonString = JsonConvert.SerializeObject(objectToSave);
            File.WriteAllText(SAVE_PATH + SAVE_FILE_NAME, jsonString);
        }

        public static void DeleteJson(string SAVE_FILE_NAME)
        {
            if(!Directory.Exists(SAVE_PATH))
            {
                Directory.CreateDirectory(SAVE_PATH);
            }

            string jsonString = "";
            File.WriteAllText(SAVE_PATH + SAVE_FILE_NAME, jsonString);
        }
    }
}
