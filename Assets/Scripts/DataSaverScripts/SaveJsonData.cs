using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace DataSaverScripts
{
    public interface ISaver
    {
        void Save();
        void Load();
    }

    public class WorldData
    {
        public List<Vector3> Points = new List<Vector3>();
        public float Time = 0f;
        public string SelectedType = "";

        public WorldData(List<Vector3> points, float time, string type)
        {
            this.Points = points;
            this.Time = time;
            this.SelectedType = type;
        }
    }
    
    public class SaveJsonData: ISaver
    {
        private string dataPath = SaverLoaderScript.SavePath;

        public void Save()
        {
            List<Vector3> points = LoadDataFromCloud.SavePoints();
            float time = LoadDataFromCloud.SaveTime();
            string type = LoadDataFromCloud.SaveType();

            var data = new WorldData(points, time, type);
            
            File.WriteAllText(
                dataPath,
                JsonConvert.SerializeObject(
                    data,Formatting.Indented,new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
                );
            
                #if UNITY_EDITOR
                            Debug.Log("Json Complete: " + Application.persistentDataPath + "/jsonWorldData.json");
                #endif
        }

        public void Load()
        {
            if (dataPath != null)
            {
                WorldData worldData =
                    JsonConvert.DeserializeObject<WorldData>(
                        File.ReadAllText(dataPath));
                if (worldData != null)
                {
                    LoadDataFromCloud.LoadPoints(worldData);
                    LoadDataFromCloud.LoadTime(worldData);
                    LoadDataFromCloud.LoadType(worldData);
                }
            }
            else
            {
                LoadDataFromCloud.Instance.LoadData();
            }
        }
    }
}