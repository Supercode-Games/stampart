using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public MeshRenderer finalStampMat;
    GameObject paintedMesh;

    public GameObject parentStamp;

    public Material twoSided;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void activateStamp()
    {
        paintedMesh = GameObject.FindGameObjectWithTag("PaintedTex");
        //parentStamp.transform.position = paintedMesh.transform.position;
        //parentStamp.transform.rotation = paintedMesh.transform.rotation;
        //parentStamp.transform.position += (parentStamp.transform.forward * -.2f);
        //finalStampMat.material = paintedMesh.material;


       var m = Instantiate(paintedMesh, paintedMesh.transform.position, paintedMesh.transform.rotation);
        m.transform.position += m.transform.forward * .14f;
        m.transform.SetParent(paintedMesh.transform);
        m.transform.localScale = Vector3.one;
        m.transform.SetParent(null, true);
        m.GetComponent<MeshRenderer>().material = twoSided;
        m.GetComponent<MeshRenderer>().material.mainTexture = paintedMesh.GetComponent<MeshRenderer>().material.mainTexture;

    }
}
