using UnityEngine;

public class Ref : MonoBehaviour
{
    public bool hit;

    private void OnCollisionEnter(Collision collision)
    {
        if(!hit && collision.gameObject.tag=="ref")
        {
            hit = true;
        }
    }

}
