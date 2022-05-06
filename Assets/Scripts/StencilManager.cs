using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StencilManager : MonoBehaviour
{

    [System.Serializable]
    public class LevelObjects
    {
        public List<Texture> outerTextures;
        public List<Texture> innerTextures;
        public List<GameObject> sheets;
        public GameObject sheetsParent;
        public GameObject spraysParent;
        public GameObject objectModelParent;
    }



    //public List<GameObject> sheets;


    public P3dPaintableTexture paint_;


    //public List<GameObject> sprayBottles;

    //public List<Texture> innerTextures;

    int currentIndex;

    public Text levelIndicator;

    public GameObject nextButton;

    public GameObject frame;
    Vector3 frameInitPos;

    public List<LevelObjects> levelObjects = new List<LevelObjects>();

    int currentLevel;

    bool levelFinished;

    public GameObject spraySelector;


    // Start is called before the first frame update
    void Start()
    {
        frameInitPos = frame.transform.position;
        frame.transform.position -= new Vector3(20f, 0, 0);
        

        levelIndicator.text = "LEVEL " + (PlayerPrefs.GetInt("current_level", 0) + 1).ToString();

        currentLevel = 0;
        activatePhase(0);
        
        levelObjects[currentLevel].sheets[0].gameObject.GetComponent<Animator>().Play("in", 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void activatePhase(int index)
    {
        levelObjects[currentLevel].sheetsParent.SetActive(true);
        levelObjects[currentLevel].spraysParent.SetActive(true);
        levelObjects[currentLevel].objectModelParent.SetActive(true);




        levelObjects[currentLevel].sheets[index].SetActive(true);

        //sheets[index].GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", levelObjects[currentLevel].outerTextures[index]);


        paint_.Texture = levelObjects[currentLevel].innerTextures[index];
        paint_.LocalMaskTexture = levelObjects[currentLevel].innerTextures[index];
        paint_.LocalMaskChannel = P3dChannel.Alpha;


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

        if (!levelFinished)
        {
            currentIndex++;
            if (currentIndex == levelObjects[currentLevel].innerTextures.Count)
            {
                levelObjects[currentLevel].sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);



                FindObjectOfType<StampRotator>().stamp();
                StartCoroutine(getFrame());
                levelFinished = true;
                foreach (var item in FindObjectOfType<SpraySelector>().sprayBottles)
                {
                    item.SetActive(false);
                }
                spraySelector.SetActive(false);

            }
            else
            {
                activatePhase(currentIndex);
                currentIndex = currentIndex % levelObjects[currentLevel].innerTextures.Count;


                levelObjects[currentLevel].sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);

                levelObjects[currentLevel].sheets[currentIndex].gameObject.GetComponent<Animator>().Play("in", 0, 0);


            }
        }
        else
        {
            SceneManager.LoadScene(0);

        }


    }
}
