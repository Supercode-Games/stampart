using UnityEngine;

public class StampManager : MonoBehaviour
{
    public MeshRenderer finalStampMat;


    public MeshRenderer twoSided;
    public MeshRenderer shine;


    // Start is called before the first frame update
    void Start()
    {
        twoSided.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            activateStamp();
        }
    }

    public void activateStamp()
    {
        shine.enabled = true;
        twoSided.enabled = true;
        twoSided.material.mainTexture = finalStampMat.material.mainTexture;

    }
}
