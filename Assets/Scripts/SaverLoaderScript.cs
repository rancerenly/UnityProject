using System;
using System.Collections;
using System.Collections.Generic;
using DataSaverScripts;
using UnityEngine;

public class SaverLoaderScript : MonoBehaviour
{
    [HideInInspector]
    public static string SavePath { get; private set;}

    private Dictionary<string, ISaver> saver = new Dictionary<string, ISaver>();

    private void Awake()
    {
        SavePath = Application.persistentDataPath + "/jsonWorldData.json";
    }

    private void Start()
    {
        saver.Add("JsonSaver", new SaveJsonData());
        saver["JsonSaver"].Load();
    }

    public void SaveData()
    {
        saver["JsonSaver"].Save();
    }
}
