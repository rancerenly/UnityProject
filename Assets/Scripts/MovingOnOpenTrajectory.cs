using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOnOpenTrajectory :  MovingObjectOnTrajectory
{
    public override float TimerForMoving(List<Transform> points, float speed, Vector3 lastPoint, Vector3 firstPoint)
    {
        float pathLength = 0f;
        
        for (int i = 0; i < points.Count-1; i++)
        {
            pathLength += Vector3.Distance(points[i].transform.position, points[i+1].transform.position);
        }
        return (pathLength / speed) * 2;
    }

    public override void MovingObject(GameObject go, List<Transform> points, float speed, Vector3 goPos)
    {
        if (Vector3.Distance(points[PointId].position, goPos) <= 0)
        {
            PointId++;
        }
        
        if (PointId >= points.Count)
        {
            PointId = 0;

            points.Reverse();
        }

        Vector3 targetPos = points[PointId].transform.position;
        
        go.transform.position = Vector3.MoveTowards(goPos, points[PointId].position, speed * Time.deltaTime);
    }
}
