using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class NextButton : MonoBehaviour
{

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

    public GameObject paintAddituve;
    public GameObject paintSubtractive;

    public GameObject drillerClipper;
    public GameObject dirtClipper;

    public List<MeshRenderer> actualCutWood;
    public List<GameObject> cut_block;
    public GameObject PlainTerrain;

    public GameObject paintPlane;


    public GameObject tapAndHold;



    // Start is called before the first frame update
    void Start()
    {
        
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
                button.SetActive(false);
                clipperDirtWood.SetActive(false);
                makeVacumeMovable();
                dirtClipper.SetActive(false);

                break;

            case 1:

                tapAndHold.SetActive(true);
                vacumeCleaner.gameObject.SetActive(false);

                foreach (var item in FindObjectsOfType<CarveParticle_prefab>())
                {

                    item.gameObject.SetActive(false);

                }
                pencil.SetActive(true);
                p3DHitBetween.gameObject.SetActive(true);
                p3DHitBetween.enabled = true;
                button.SetActive(false);

                break;

            case 2:


                tapAndHold.SetActive(true);
                driller.SetActive(true);
                drillerClipper.SetActive(true);

                pencil.GetComponentInChildren<MeshRenderer>().enabled = false;
                pencil.GetComponent<PencilManager>().t = 0;
                pencil.GetComponent<PencilManager>().speedOfAnimation = FindObjectOfType<DrillerManager>().speedOfAnimation;

                paintAddituve.GetComponent<PaintIn3D.P3dPaintSphere>().Color = Color.black;
               



                // paintAddituve.SetActive(false);
                // paintSubtractive.SetActive(true);

                pencil.GetComponent<PencilManager>().startCor();
               
                button.SetActive(false);

                break;

            case 3:

                paintPlane.SetActive(false);
                driller.SetActive(false);
                PlainTerrain.SetActive(false);
                actualCutWood[_Manager.Agent.currentLevel].enabled = true;
                cut_block[_Manager.Agent.currentLevel].SetActive(true);
                drillerClipper.SetActive(false);
                cut_block[_Manager.Agent.currentLevel].GetComponent<Animator>().Play("BlockAnimation", 0, 0);
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


    void makeVacumeMovable()
    {

        vacumeCleaner.canMove = true;

    }

    void deactivateCutBlockAnim()
    {

        currentPhase = 4;
        button.SetActive(true);
        cut_block[_Manager.Agent.currentLevel].GetComponent<Animator>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(actualCutWood[_Manager.Agent.currentLevel]!=null && actualCutWood[_Manager.Agent.currentLevel].enabled)
        {

            actualCutWood[_Manager.Agent.currentLevel].transform.Translate(Vector3.right * Time.deltaTime * 10f);


        }
    }
}
