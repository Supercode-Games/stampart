using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathCreatorForShape : MonoBehaviour
{

    // 0.55 , 0.75

   PathCreator path;
   public List<Transform> childNodes = new List<Transform>();

    

    public PathCreator createPathShape()
    {
        path = this.gameObject.GetComponent<PathCreator>();
        childNodes.AddRange(GetComponentsInChildren<Transform>());
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {

            points.Add(gameObject.transform.GetChild(i).transform.localPosition);

        }

        path.bezierPath = new BezierPath(points, true, PathSpace.xyz);

        return path;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
