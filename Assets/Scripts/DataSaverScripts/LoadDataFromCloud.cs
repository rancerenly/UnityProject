using System;
using System.Collections;
using System.Collections.Generic;
using DataSaverScripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class LoadDataFromCloud : MonoBehaviour
{
    [HideInInspector]
    public static LoadDataFromCloud Instance;
    
    private string jsonURL = "https://drive.google.com/uc?export=download&id=1pdCYWDEIDWmVAp9kfQBQ7ALwBNqrm5-G";

    public static List<Vector3> SavePoints()
    {
        List<Vector3> pointsData = new List<Vector3>();

        foreach (var p in MovingScript.Instance.points)
        {
            pointsData.Add(p.position);
        }

        return pointsData;
    }

    public static float SaveTime()
    {
        float time = 0f;
        if (MovingScript.Instance.Time != 0f)
        {
            time = MovingScript.Instance.Time;
        }

        return time;
    }

    public static string SaveType()
    {
        string type = BehaviourChange.Instance.selectedType.ToString();
        return type;
    }

    public static void LoadPoints(WorldData worldData)
    {
        MovingScript.Instance.DeleteAllPoints();

        GameObject prefPoint = CreatePoints.Instance.PrefabPoint;
        GameObject parentPoint = CreatePoints.Instance.ParentOfPoints;

        foreach (Vector3 pointPos in worldData.Points)
        {
            GameObject newPoint = CreatePoints.Instance.CreatePoint(prefPoint, pointPos, parentPoint);

            MovingScript.Instance.points.Add(newPoint.transform);
        }
    }

    public static void LoadTime(WorldData worldData)
    {
        MovingScript.Instance.Time = worldData.Time;
    }

    public static void LoadType(WorldData worldData)
    {
        BehaviourChange.TypeOfMovement selType;
        Enum.TryParse(worldData.SelectedType, out selType);
        BehaviourChange.Instance.selectedType = selType;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadData()
    {
        StartCoroutine(GetData(jsonURL));
    }

    private IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isDone)
        {
            WorldData worldData = JsonConvert.DeserializeObject<WorldData>(request.downloadHandler.text);
            LoadPoints(worldData);
            LoadTime(worldData);
            LoadType(worldData);
        }

        request.Dispose();
    }
}