using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    public List<Image> levelDots;
    //public List<Sprite> brownVersion;
    //public List<Sprite> greenVersion;

   
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
        for (int i = 0; i < currentPhase+1; i++)
        {
            levelDots[i].enabled = true;
        }

        //for (int i = currentPhase+1; i < levelDots.Count; i++)
        //{  
        //    levelDots[i].sprite = brownVersion[i];
        //}

        //levelDots[currentPhase].sprite = greenVersion[currentPhase];
    }
}
