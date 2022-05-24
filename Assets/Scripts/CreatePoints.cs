using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFieldX;
    
    [SerializeField]
    private TMP_InputField inputFieldY;
    
    [SerializeField]
    private TMP_InputField inputFieldZ;
    
    [SerializeField]
    public GameObject PrefabPoint;
    
    [SerializeField]
    public GameObject ParentOfPoints;
    
    [HideInInspector]
    public static CreatePoints Instance;

    private List<Transform> points;

    private GameObject newPoint;
    
    private float x, y, z;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        points = GameObject.FindGameObjectWithTag("GameController").GetComponent<MovingScript>().points;
    }

    private Vector3 CreateVectorByAxis()
    {
        float.TryParse(inputFieldX.text, out x);
        float.TryParse(inputFieldY.text, out y);
        float.TryParse(inputFieldZ.text, out z);
        
        return new Vector3(x, y, z);
    }

    public void InstantiatePoint()
    {
        newPoint = CreatePoint(PrefabPoint, CreateVectorByAxis(), ParentOfPoints);

        void SetPoint()
        {
            points.Add(newPoint.transform);
        }
        
        SetPoint();
    }

    public GameObject CreatePoint(GameObject prefab, Vector3 position, GameObject parent)
    {
        newPoint = Instantiate(prefab, position, Quaternion.identity, parent.transform);
        return newPoint;
    }

}
