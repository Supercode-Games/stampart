using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targePos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        transform.position = targePos + new Vector3(offset.x,0,0);
    }
}
