using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class MovingScript : MonoBehaviour
{
    [HideInInspector]
    public static MovingScript Instance;
    
    [SerializeField]
    public List<Transform> points;
    
    [SerializeField]
    private GameObject go;

    [SerializeField]
    private Slider speedSliper;

    [SerializeField]
    private TMP_Text text;
    
    [HideInInspector]
    public float Time;
    
    private float speed;
    
    private Vector3 _lastPoint;
    private Vector3 _firstPoint;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        _lastPoint = points.Last().position;
        _firstPoint = points.First().position;
    }
    private void FixedUpdate()
    {
        Moving();
    }
    private void Moving()   
    {
        Vector3 goPos = go.transform.position;
        
        if (speed !=0)
            BehaviourChange.Controller.MovingObject(go, points, speed, goPos);
    }

    public void ChangedSpeed()
    {
        speed = speedSliper.value;
    }

    public void Timer()
    {
        if (speed !=0)
            Time = BehaviourChange.Controller.TimerForMoving(points, speed, _lastPoint, _firstPoint);

        text.text = "Время прохождения пути: " + Time;
    }
    
    public void DeleteAllPoints()
    {
        points.Clear();
    }

}
