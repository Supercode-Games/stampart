using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    public GameObject myOBJ;

    public enum RotationDir
    {

        X,
        Y,
        Z

    }

    public RotationDir rotationDir;

    public bool canRotate;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            switch (rotationDir)
            {
                case RotationDir.X:

                    myOBJ.transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed,Space.Self);

                    break;

                case RotationDir.Y:

                    myOBJ.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed, Space.Self);

                    break;

                case RotationDir.Z:

                    myOBJ.transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed, Space.Self);

                    break;


                default:
                    break;
            }
        }
    }
}
