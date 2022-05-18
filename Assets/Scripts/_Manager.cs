using System.Collections;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class _Manager : MonoBehaviour
{
    public static _Manager Agent;

    public GameObject diggableWood;
    public GameObject scrubber;


    public bool canCarve;

    public GameObject scrubberObject;
    public VacumeCleaner vacumeCleaner;

    public GameObject plainTerrain;
    public Animator blockAnimator;

    public Toggle soundToggle;
    public Toggle vibrationToggle;

    public AudioSource scrubberAudio;

    public static int currentLevel;

    public Text levelIndicator;

    public Animator settingspanel;
    bool settingsOff;

    public Image soundImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;


    public Image vibrationImage;
    public Sprite vibrationOnSprite;
    public Sprite vibrationOffSprite;



    private void Awake()
    {
        Agent = this;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
      settingsOff = true;

      initialiseCurrentLevel();
      FindObjectOfType<ThemeManager>().initialiseThemeBG();
        
      diggableWood.transform.rotation = Quaternion.Euler(90, 0, 0);
      diggableWood.transform.position -= new Vector3(12.5f * .5f, 0, 0);
      loadVibrationAndSounds();
    }

    void initialiseCurrentLevel()
    {
        currentLevel = PlayerPrefs.GetInt("current_level") % FindObjectOfType<NextButton>().cut_block.Count;
        levelIndicator.text = "LEVEL " + (PlayerPrefs.GetInt("current_level", 0) + 1).ToString();

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start_" + PlayerPrefs.GetInt("current_level"));
        AppMetrica.Instance.ReportEvent("level_start_levelnumber" + PlayerPrefs.GetInt("current_level"));
        AppMetrica.Instance.SendEventsBuffer();
    }
    

     void increaseLevel()
    {
        PlayerPrefs.SetInt("current_level", currentLevel);
    }

    public Vector3 pointOnPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 50f, 1 << LayerMask.NameToLayer("TouchPlane")))
        {
            return raycastHit.point;
        }

        return Vector3.zero;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canCarve)
        {
            var y = scrubber.transform.position.y;
            var targetPos = pointOnPlane();
            targetPos.z += 3f;
            targetPos.y = y;

            scrubber.transform.position = targetPos;
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            scrubberObject.SetActive(false);
            canCarve = false;

            vacumeCleaner.gameObject.SetActive(true);
            vacumeCleaner.canMove = true;
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            blockAnimator.Play("BlockAnimation", 0, 0);
            Invoke("deactivatePlainTerrain", .2f);
        }

    }

    void deactivatePlainTerrain()
    {
        plainTerrain.SetActive(false);
    }


   

    public void startVibrationSingle()
    {

#if UNITY_IOS

        MMVibrationManager.ContinuousHaptic(.1f, .1f,20f);

#endif

#if UNITY_ANDROID

        StopCoroutine("vibrateCorMM");
        MMVibrationManager.Vibrate();

#endif

    }



    bool frequnecy;
    public void singleVibration()
    {

#if UNITY_IOS

        MMVibrationManager.ContinuousHaptic(.1f, .1f,20f);

#endif

#if UNITY_ANDROID

        if (!frequnecy)
        {
            frequnecy = true;
            MMVibrationManager.Vibrate();
            Invoke("freF", .2f);

        }

#endif

    }

    void freF()
    {
        frequnecy = false;
    }
   

    public void startVibration()
    {

#if UNITY_IOS

        MMVibrationManager.ContinuousHaptic(.1f, .1f,20f);

#endif

#if UNITY_ANDROID

        StartCoroutine("vibrateCorMM");

#endif

    }

    IEnumerator vibrateCor()
    {
        while (true)
        {
            Vibration.Vibrate(50);
            yield return new WaitForSeconds(.1f);

        }
    }


    IEnumerator vibrateCorMM()
    {
        while (true)
        {
            MMVibrationManager.Vibrate();
            yield return new WaitForSeconds(.2f);

        }
    }



    public void stopVibration()
    {

#if UNITY_IOS

        MMVibrationManager.StopContinuousHaptic();

#endif

#if UNITY_ANDROID

        StopCoroutine("vibrateCorMM");
#endif


    }


    void loadVibrationAndSounds()
    {
        if (!PlayerPrefs.HasKey("firskey"))
        {

            PlayerPrefs.SetInt("firskey", 1);
            PlayerPrefs.SetInt("sound_", 1);
            PlayerPrefs.SetInt("vibration_", 1);
        }



        if (PlayerPrefs.GetInt("sound_")==1)
        {
            soundToggle.isOn = true;
            soundImage.sprite = soundOnSprite;
        }
        else
        {
            soundToggle.isOn = false;
            soundImage.sprite = soundOffSprite;
        }


        if (PlayerPrefs.GetInt("vibration_") == 1)
        {
            vibrationToggle.isOn = true;
            vibrationImage.sprite = vibrationOnSprite;
        }
        else
        {
            vibrationToggle.isOn = false;
            vibrationImage.sprite = vibrationOffSprite;

        }


        setToggles();

    }


    void setToggles()
    {

        if(PlayerPrefs.GetInt("sound_")==0)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;

        }


        if (PlayerPrefs.GetInt("vibration_") == 0)
        {
            MMVibrationManager.SetHapticsActive(false);
        }
        else
        {
            MMVibrationManager.SetHapticsActive(true);

        }

    }


   public void soundToggleButton()
    {
        if(soundImage.sprite != soundOnSprite)
        {
            PlayerPrefs.SetInt("sound_", 1);
        }
        else
        {
            PlayerPrefs.SetInt("sound_", 0);

        }
        setToggles();
        loadVibrationAndSounds();

    }

    public void vibrationToggleButton()
    {
        if (vibrationImage.sprite != vibrationOnSprite)
        {
            PlayerPrefs.SetInt("vibration_", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vibration_", 0);

        }
        setToggles();
        loadVibrationAndSounds();

    }

    public void settingsClicked()
    {
        if(settingsOff)
        {
            settingsOff = false;
            settingspanel.SetTrigger("out");
        }
        else
        {
            settingsOff = true;

            settingspanel.SetTrigger("in");

        }
    }

    IEnumerator changeCameraAngleCor()
    {
        var currentPos = transform.position;
        var currentRot = transform.rotation;

        while (true)
        {
            yield return null;
        }
    }

}
