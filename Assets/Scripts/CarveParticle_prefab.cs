using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarveParticle_prefab : MonoBehaviour
{

    public List<GameObject> carveParticles;


    // Start is called before the first frame update
    void Start()
    {
        carveParticles = Shuffle(carveParticles);
        carveParticles[0].SetActive(false);
        carveParticles[1].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<T> Shuffle<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }

        return ts;
    }
}
