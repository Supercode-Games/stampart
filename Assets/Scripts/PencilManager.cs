using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilManager : MonoBehaviour
{
    GameObject pencilAnimationObject;
    Animator pencilAnimator;

    public List<string> animationNames;

    public int currentAnimationIndex;

    public float speedOfAnimation;

    public float yPos;

    public float t = 0f;
    float x;

    bool finished;

    AudioSource myAudio;
    public GameObject tapAndHold;

    bool gameFinished;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        loadPencilAnimation();
        StartCoroutine("pencilDrawAround");

    }

    void loadPencilAnimation()
    {

        pencilAnimator = GameObject.FindGameObjectWithTag("PencilAnimator").GetComponent<Animator>();
        pencilAnimationObject = GameObject.FindGameObjectWithTag("Pencil");

    }

    public void startCor()
    {
        StopCoroutine("pencilDrawAround");
        StartCoroutine("pencilDrawAround");
    }

    IEnumerator pencilDrawAround()
    {


        var reached = false;

        while (!reached)
        {
            x = Mathf.Clamp(t / 2f, 0f, 1f);
            x = x * x * (3 - 2 * x);

            pencilAnimator.Play(animationNames[currentAnimationIndex], 0, x);

            var targetPos = pencilAnimationObject.transform.position;
            targetPos.y = yPos;
            transform.position = targetPos;

            if(x>=1f)
            {
                reached = true;
                gameFinished = true;
                myAudio.Stop();
                _Manager.Agent.stopVibration();

            }

            yield return null;

        }


    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButton(0) && !gameFinished)
        {

            t += Time.deltaTime * speedOfAnimation;

            if(!myAudio.isPlaying)
            {
                myAudio.Play();
                _Manager.Agent.startVibration();
            }
            if(tapAndHold.activeInHierarchy)
            {
                tapAndHold.SetActive(false);
            }
            

        }
        else
        {
            if (myAudio.isPlaying)
            {
                myAudio.Stop();
                _Manager.Agent.stopVibration();

            }
        }

        if(!finished && x>=1)
        {
            finished = true;
            FindObjectOfType<NextButton>().activateButton(2);
        }
    }
}
