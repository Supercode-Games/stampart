using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MeshPathGenerator : MonoBehaviour
{
    public MeshFilter mesh_;
    public PathCreator path_creator;

    public GameObject sphear;

    void Start()
    {

        var p = gameObject.transform.TransformPoint(mesh_.mesh.vertices[0]);


        var o = getMeshPoints(mesh_.mesh);
        path_creator.bezierPath = new BezierPath(getMeshPoints(mesh_.sharedMesh), false, PathSpace.xyz);
    }


    Vector3[] getMeshPoints(Mesh mesh)
    {
        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        var meshPoints = new List<Vector3>();
        for (int i = 0; i < mesh.vertices.Length; i = i + 5)
        {
            var p = localToWorld.MultiplyPoint3x4(mesh.vertices[i]);
            meshPoints.Add(new Vector3(p.x, 0, p.y));
            Instantiate(sphear).transform.position = new Vector3(p.x, 0, p.y);
        }

        return meshPoints.ToArray();

    }

    VertexPath GeneratePath(Vector3[] points, bool closedPath)
    {
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xy);
        return new VertexPath(bezierPath, this.transform, .1f);
    }

}
