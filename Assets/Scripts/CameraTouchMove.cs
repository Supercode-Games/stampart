using UnityEngine;
using MoreMountains.NiceVibrations;

public class CameraTouchMove : MonoBehaviour
{
    public GameObject targetObject;

    Vector3 offset;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        setToggles();
    }

    void setToggles()
    {

        if (PlayerPrefs.GetInt("sound_") == 0)
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

    void Start()
    {
        offset = transform.position - targetObject.transform.position;
    }

    void LateUpdate()
    {
        var targePos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        transform.position = targePos + new Vector3(offset.x,0,0);
    }
}
