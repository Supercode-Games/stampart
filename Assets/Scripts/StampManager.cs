using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public MeshRenderer finalStampMat;
    MeshRenderer paintedMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            finalStampMat.material = paintedMesh.material;
        
        }
    }

    public void activateStamp()
    {
        paintedMesh = GameObject.FindGameObjectWithTag("PaintedTex").GetComponent<MeshRenderer>(); 
        finalStampMat.transform.position = paintedMesh.transform.position;
        finalStampMat.transform.position += (finalStampMat.transform.forward * -.2f);
        finalStampMat.material = paintedMesh.material;

    }
}
