using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class StencilManager : MonoBehaviour
{

    public List<GameObject> sheets;


    public P3dPaintableTexture paint_;


    public List<GameObject> sprayBottles;

    public List<Texture> innerTextures;

    int currentIndex;



    // Start is called before the first frame update
    void Start()
    {
        activatePhase(0);
        sheets[0].gameObject.GetComponent<Animator>().Play("in", 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void activatePhase(int index)
    {
        
        foreach (var item in sprayBottles)
        {
            item.SetActive(false);
        }


        sheets[index].SetActive(true);
        paint_.Texture = innerTextures[index];
        paint_.LocalMaskTexture = innerTextures[index];
        paint_.LocalMaskChannel = P3dChannel.Alpha;
        sprayBottles[index].SetActive(true);


    }

    public void goToNextPhase()
    {
        currentIndex++;
        if (currentIndex == 3)
        {
            
            foreach (var item in sprayBottles)
            {
                item.SetActive(false);
            }

            Camera.main.gameObject.GetComponent<Animator>().Play("finish", 0, 0);
            sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);

        }
        else
        {
            activatePhase(currentIndex);
            currentIndex = currentIndex % sheets.Count;


            sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);

            sheets[currentIndex].gameObject.GetComponent<Animator>().Play("in", 0, 0);
        }

         
        

    }
}
