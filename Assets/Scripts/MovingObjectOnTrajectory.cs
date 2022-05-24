using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObjectOnTrajectory
{
    protected int PointId = 0;
    public abstract float TimerForMoving(List<Transform> points, float speed, Vector3 lastPoint, Vector3 firstPoint);
    public abstract void MovingObject(GameObject go, List<Transform> points, float speed,  Vector3 goPos);
}
