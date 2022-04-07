using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    public List<GameObject> levelDots;

    public int currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        updatePhase();
    }

    public void increasePhase()
    {
        currentPhase++;
        updatePhase();
    }

    void updatePhase()
    {
        for (int i = 0; i < currentPhase; i++)
        {
            levelDots[i].SetActive(true);
        }
        
    }
}
