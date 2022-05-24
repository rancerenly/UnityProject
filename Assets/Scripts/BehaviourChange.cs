using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public class BehaviourChange : MonoBehaviour
{
    public enum TypeOfMovement
    {
        Open,
        Loop        
    }
    [HideInInspector]
    public static BehaviourChange Instance;
    
    [SerializeField]
    private Dropdown dropdown;

    [HideInInspector]
    public static MovingObjectOnTrajectory Controller;

    [HideInInspector]
    public TypeOfMovement selectedType;
    
    private readonly TypeOfMovement[] typesMovements =
    {
        TypeOfMovement.Open,
        TypeOfMovement.Loop
    };

    private List<string> _types = new List<string>(); 
    
    private void Awake()
    {
        Instance = this;
        InsertOptions();
    }
    private void Start()
    {
        Controller = CreateController(selectedType);
    }
    private void InsertOptions()
    {
        foreach (var type in typesMovements)
        {
            _types.Add(type.ToString());
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(_types);
    }
    public MovingObjectOnTrajectory CreateController(TypeOfMovement type)
    {
        switch (type)
        {
            case TypeOfMovement.Loop:
                Controller =  new MovingOnLoopTrajectory();
                break;
            case TypeOfMovement.Open:
                Controller = new MovingOnOpenTrajectory();
                break;
        }
        return Controller;
    }

    public void ChangeTypeOfMovement(Dropdown change)
    {
        Enum.TryParse(change.value.ToString(), out selectedType);
        CreateController(selectedType);
    }
}
