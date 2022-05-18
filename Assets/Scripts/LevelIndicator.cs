using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    public List<Image> levelDots;
    public int currentPhase;

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
        for (int i = 0; i < currentPhase+1; i++)
        {
            levelDots[i].enabled = true;
        }
    }
}
