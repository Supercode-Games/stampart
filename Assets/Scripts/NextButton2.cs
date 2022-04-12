using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.SceneManagement;


public class NextButton2 : MonoBehaviour
{

   public int currentPhase;
    public GameObject button;

    public GameObject paintedBlock;
    public GameObject sprayCan;
    public GameObject paper;

    public GameObject stampObj;

    public Animator finalPaperAnim;

    public GameObject finalUI;

    public GameObject stamper;

    public GameObject paperAnim;
    public Animator blockAnim;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("activateButtonInvoke", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void activateButtonInvoke()
    {

        activateButton(0);

    }

    public void activateButton(int phase)
    {

        this.currentPhase = phase;
        button.SetActive(true);

    }

    public void goToNextPhase()
    {

#if UNITY_IOS
                    MMVibrationManager.TransientHaptic(1f, 1f);
#elif UNITY_ANDROID
                    MMVibrationManager.Vibrate();
#endif

        switch (currentPhase)
        {
            case 0:

                sprayCan.SetActive(false);

                stampObjActivate();

                button.SetActive(false);
                break;

            default:
                break;
        }

    }


    void stampObjActivate()
    {

        paperAnim.GetComponent<Animator>().Play("paperIn", 0, 0);
        blockAnim.Play("blockReverse", 0, 0);
        Invoke("stampRemove", 2.5f);
        //Invoke("stamperActivate", .9f+1f);
        Invoke("stampTExtActivate", 2f);

    }

    void stamperActivate()
    {
        stamper.SetActive(true);
    }

    void stampTExtActivate()
    {
        FindObjectOfType<StampManager>().activateStamp();
    }

    void stampRemove()
    {

        Camera.main.gameObject.GetComponent<AudioSource>().Play(0);
        finalUI.SetActive(true);
        FindObjectOfType<Camera>().gameObject.GetComponent<Animator>().Play("finalCamera", 0, 0);
        blockAnim.Play("BlockAway", 0, 0);

    }


    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}