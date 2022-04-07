using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paper : MonoBehaviour
{
    public MeshRenderer finalMat;
    public MeshRenderer paintedMesh;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void StampOnThis()
    {
        finalMat.material = paintedMesh.material; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            StampOnThis();
        }
    }
}
