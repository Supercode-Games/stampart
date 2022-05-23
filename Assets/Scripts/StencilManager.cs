using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class StencilManager : MonoBehaviour
{

    [System.Serializable]
    public class LevelObjects
    {
        public List<Texture> outerTextures;
        public List<Texture> innerTextures;
        public GameObject objectModelParent;
        public Texture finalTexture;
    }

    public MeshRenderer finalTextureOverlay;

    public List<GameObject> sheets;


    public P3dPaintableTexture paint_;


    int currentIndex;

    public Text levelIndicator;

    public GameObject nextButton;
    public Button nextButt;

    public GameObject frame;
    Vector3 frameInitPos;

    public List<LevelObjects> levelObjects = new List<LevelObjects>();

    int currentLevel;

    bool levelFinished;

    public GameObject spraySelector;

    public GameObject dragToSPray;
    public GameObject gamePlayPage;

    public GameObject levelCompletedPage;

    public Text level;

    public int testLevel;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ThemeManager>().initialiseThemeBG();

        frameInitPos = frame.transform.position;
        frame.transform.position -= new Vector3(20f, 0, 0);

        //nextButt.interactable = false;
        levelIndicator.text = "LEVEL " + (PlayerPrefs.GetInt("current_level", 0) + 1).ToString();

        currentLevel = PlayerPrefs.GetInt("current_level")%levelObjects.Count;
        currentLevel = testLevel;
        activatePhase(0);
        
        sheets[0].gameObject.GetComponent<Animator>().Play("in", 0, 0);

        for (int i = 0; i < levelObjects[currentLevel].outerTextures.Count; i++)
        {

            sheets[i].GetComponent<MeshRenderer>().material.mainTexture = levelObjects[currentLevel].outerTextures[i];
            var pt = sheets[i].GetComponent<P3dPaintableTexture>();
            pt.LocalMaskTexture = levelObjects[currentLevel].outerTextures[i];
            pt.Texture = levelObjects[currentLevel].outerTextures[i];
            pt.Activate();
            sheets[i].GetComponent<P3dPaintable>().DirtyMaterials();
        }


    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(dragToSPray.activeInHierarchy)
            dragToSPray.SetActive(false);
        }

    }

    void activatePhase(int index)
    {

        finalTextureOverlay.material.mainTexture = levelObjects[currentLevel].finalTexture;
        levelObjects[currentLevel].objectModelParent.SetActive(true);


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
    void activateButton()
    {
        if(!nextButt.gameObject.activeInHierarchy)
        nextButt.gameObject.SetActive(true);

        if (!nextButt.interactable)
            nextButt.interactable = true;
    }

    public void goToNextPhase()
    {

        if (!levelFinished)
        {
            currentIndex++;
            if (currentIndex == levelObjects[currentLevel].innerTextures.Count)
            {
                sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);



                FindObjectOfType<StampRotator>().stamp();
                StartCoroutine(getFrame());
                levelFinished = true;
                foreach (var item in FindObjectOfType<SpraySelector>().sprayBottles)
                {
                    item.SetActive(false);
                }
                spraySelector.SetActive(false);
                gamePlayPage.SetActive(false);
                level.text = "LEVEL " + (PlayerPrefs.GetInt("current_level", 0) + 1) + " COMPLETED!";
                nextButt.gameObject.SetActive(false);
                Invoke("LevelFinishedLevelPage", 4f);
            }
            else
            {
                activatePhase(currentIndex);
                currentIndex = currentIndex % levelObjects[currentLevel].innerTextures.Count;


                sheets[currentIndex - 1].gameObject.GetComponent<Animator>().Play("out", 0, 0);

                sheets[currentIndex].gameObject.GetComponent<Animator>().Play("in", 0, 0);

               
            }

           

        }
        else
        {
            var l = PlayerPrefs.GetInt("current_level", 0);
            l++;
            PlayerPrefs.SetInt("current_level", l);
            SceneManager.LoadScene(0);

        }


    }

    void LevelFinishedLevelPage()
    {
        levelCompletedPage.SetActive(true);

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete_" + PlayerPrefs.GetInt("current_level"));
        AppMetrica.Instance.ReportEvent("level_finish_levelnumber" + PlayerPrefs.GetInt("current_level"));
        AppMetrica.Instance.SendEventsBuffer();

        var l = PlayerPrefs.GetInt("current_level", 0);
        l++;
        PlayerPrefs.SetInt("current_level", l);
    }

    public void goToNextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
