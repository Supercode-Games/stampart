using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;


public class StencilManager : MonoBehaviour
{

    public List<GameObject> sheets;


    public P3dPaintableTexture paint_;


    public List<GameObject> sprayBottles;

    public List<Texture> innerTextures;

    int currentIndex;

    public Text levelIndicator;

    public GameObject nextButton;

    public GameObject frame;
    Vector3 frameInitPos;


    // Start is called before the first frame update
    void Start()
    {
        frameInitPos = frame.transform.position;
        frame.transform.position -= new Vector3(20f, 0, 0);
        

        levelIndicator.text = "LEVEL " + (PlayerPrefs.GetInt("current_level", 0) + 1).ToString(); 
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

    IEnumerator getFrame()
    {
        Vector3 currentPos = frame.transform.position;
        Vector3 targetPos = frameInitPos;
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime;
            var x = Mathf.Clamp(t / .4f, 0f, 1f);
            frame.transform.position = Vector3.Lerp(currentPos, targetPos, x);

            yield return null;

        }
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

            sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);
            FindObjectOfType<StampRotator>().stamp();
            StartCoroutine(getFrame());
            nextButton.SetActive(false);

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
