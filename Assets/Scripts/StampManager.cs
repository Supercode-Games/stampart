using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public MeshRenderer finalStampMat;
    public MeshRenderer paintedMesh;

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
        finalStampMat.material = paintedMesh.material;

    }
}
