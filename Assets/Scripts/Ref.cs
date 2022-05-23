using UnityEngine;

public class Ref : MonoBehaviour
{
    public bool hit;

    public GameObject carveParticlesPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if(!hit && collision.gameObject.tag=="ref")
        {
            hit = true;
            Instantiate(carveParticlesPrefab, transform.position + new Vector3(0, .2f, 0), carveParticlesPrefab.transform.rotation).transform.localScale=Vector3.one*.3f;
           

        }
    }

}
