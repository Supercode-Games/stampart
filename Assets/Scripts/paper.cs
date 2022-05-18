using UnityEngine;

public class paper : MonoBehaviour
{
    public MeshRenderer finalMat;
    public MeshRenderer paintedMesh;

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
