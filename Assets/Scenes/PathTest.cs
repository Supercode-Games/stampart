using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathTest : MonoBehaviour
{
    public GameObject follower;

    public float sensitivity;

    public PathCreator pathCreator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            var x = Input.GetAxis("Mouse X") * sensitivity;
            var y = Input.GetAxis("Mouse Y") * sensitivity;

            transform.position += new Vector3(x, 0, y);

        }

        var closestPoint = pathCreator.path.GetClosestPointOnPath(transform.position);
        if(Vector2.Distance(new Vector2(closestPoint.x,closestPoint.z),new Vector2(transform.position.x,transform.position.z))<1f)
        {
            var p = follower.transform.position;
            p.x = closestPoint.x;
            p.z = closestPoint.z;

            follower.transform.position = Vector3.Lerp(follower.transform.position, p, Time.deltaTime * 20f);
        }
        else
        {
            var p = follower.transform.position;
            p.x = transform.position.x;
            p.z = transform.position.z;

            follower.transform.position = Vector3.Lerp(follower.transform.position, p, Time.deltaTime * 20f);
        }

    }
}
