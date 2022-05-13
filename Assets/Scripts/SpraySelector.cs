using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpraySelector : MonoBehaviour
{

    public List<Image> sprayCanButtons;
    public List<GameObject> sprayBottles;

    public Sprite normal;
    public Sprite shine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void selectSprayBottle(int index)
    {
        foreach (var item in sprayCanButtons)
        {
            item.sprite = normal;
        }

        sprayCanButtons[index].sprite = shine;

        foreach (var item in sprayBottles)
        {
            item.SetActive(false);
        }

        sprayBottles[index].SetActive(true);

    }

  

}
