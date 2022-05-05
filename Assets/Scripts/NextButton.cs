using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;


public class NextButton : MonoBehaviour
{

    public GameObject tutorial;
    public int currentPhase;
    public GameObject button;

    public GameObject dirtWoodTerrain;
    public _Manager manager;
    public GameObject scrubber;
    public VacumeCleaner vacumeCleaner;
    public GameObject clipperDirtWood;
    public PaintIn3D.P3dHitBetween p3DHitBetween;
    public GameObject pencil;
    public GameObject driller;

    public GameObject driller2_manager;
    public GameObject driller2_follower;


    public GameObject paintAddituve;
    public GameObject paintSubtractive;

    public GameObject drillerClipper;
    public GameObject dirtClipper;

    public List<MeshRenderer> actualCutWood;
    public List<GameObject> cut_block;
    public GameObject PlainTerrain;

    public GameObject paintPlane;


    public GameObject tapAndHold;

    public GameObject loadingPanel;

    public Text tapAndHoldText;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("deActivateLoadingPanel", 2f);
    }

    void deActivateLoadingPanel()
    {
        loadingPanel.SetActive(false);
    }

    public void activateButton(int currentPhase)
    {
        button.SetActive(true);
        this.currentPhase = currentPhase;
    }


    public void goToNextPhase()
    {

#if UNITY_IOS
                    MMVibrationManager.TransientHaptic(1f, 1f);
#elif UNITY_ANDROID
        _Manager.Agent.stopVibration();
        MMVibrationManager.Vibrate();
#endif

        switch (currentPhase)
        {
            case 0:

                dirtWoodTerrain.SetActive(false);
                manager.canCarve = false;
                scrubber.SetActive(false);
                vacumeCleaner.gameObject.SetActive(true);
                FindObjectOfType<VacumeCleaner>().initOffset = 4f;
                button.SetActive(false);
                clipperDirtWood.SetActive(false);
                makeVacumeMovable();
                dirtClipper.SetActive(false);
                tapAndHold.SetActive(true);
                tapAndHoldText.text = "TAP TO VACCUME";


                break;

            case 1:

                tapAndHold.SetActive(true);
                tapAndHoldText.text = "HOLD HERE TO DRAW";


                vacumeCleaner.gameObject.SetActive(false);

                foreach (var item in FindObjectsOfType<CarveParticle_prefab>())
                {

                    item.gameObject.SetActive(false);

                }
                pencil.SetActive(true);

                FindObjectOfType<PencilManager>().initOffset = new Vector3(2, 2, 0);


                Invoke("panintablesActivate", 1f);
                button.SetActive(false);

                break;

            case 2:


                tutorial.SetActive(true);

                driller2_manager.SetActive(true);

                driller2_follower.SetActive(true);
                drillerClipper.SetActive(true);

                FindObjectOfType<DrillerManager2>().loadPath();
                pencil.GetComponentInChildren<MeshRenderer>().enabled = false;
                pencil.GetComponent<PencilManager>().t = 0;
              //  pencil.GetComponent<PencilManager>().speedOfAnimation = FindObjectOfType<DrillerManager>().speedOfAnimation;

                paintAddituve.GetComponent<PaintIn3D.P3dPaintSphere>().Color = Color.black;
               



                // paintAddituve.SetActive(false);
                // paintSubtractive.SetActive(true);

                //pencil.GetComponent<PencilManager>().startCor();
               
                button.SetActive(false);

                break;

            case 3:

                paintPlane.SetActive(false);
                driller.SetActive(false);
                PlainTerrain.SetActive(false);
                actualCutWood[_Manager.currentLevel].enabled = true;
                cut_block[_Manager.currentLevel].SetActive(true);
                drillerClipper.SetActive(false);
                cut_block[_Manager.currentLevel].GetComponent<Animator>().Play("BlockAnimation", 0, 0);
                Invoke("deactivateCutBlockAnim", 1.5f);


                break;

            case 4:

                button.SetActive(false);
                SceneManager.LoadScene(1);

                break;


            default:
                break;
        }

    }


    public void finishPhase1()
    {
        driller2_follower.SetActive(false);
        driller2_manager.SetActive(false);
        paintPlane.SetActive(false);
        driller.SetActive(false);
        PlainTerrain.SetActive(false);
        actualCutWood[_Manager.currentLevel].enabled = true;
        cut_block[_Manager.currentLevel].SetActive(true);
        drillerClipper.SetActive(false);
        cut_block[_Manager.currentLevel].GetComponent<Animator>().Play("BlockAnimation", 0, 0);
        Invoke("deactivateCutBlockAnim", 1.5f);

    }

    
    

    void makeVacumeMovable()
    {

        vacumeCleaner.canMove = true;

    }

    void deactivateCutBlockAnim()
    {

        currentPhase = 4;
        button.SetActive(true);
        cut_block[_Manager.currentLevel].GetComponent<Animator>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(actualCutWood[_Manager.currentLevel]!=null && actualCutWood[_Manager.currentLevel].enabled)
        {

            actualCutWood[_Manager.currentLevel].transform.Translate(Vector3.right * Time.deltaTime * 10f);


        }
    }

    void panintablesActivate()
    {
        p3DHitBetween.gameObject.SetActive(true);
        p3DHitBetween.enabled = true;
    }
}
