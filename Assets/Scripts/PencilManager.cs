using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PencilManager : MonoBehaviour
{
   GameObject pencilAnimationObject;
   Animator pencilAnimator;

   public List<string> animationNames;


   public float speedOfAnimation;

   public float yPos;

   public float t = 0f;
   float x;

   bool finished;

   AudioSource myAudio;
   public GameObject tapAndHold;

   bool gameFinished;

   public List<PathCreatorForShape> pathShapes;

   public PathCreator currentPath;

   public Vector3 initOffset = new Vector3(2f,0,0);

    public AudioClip pencilStart;
    public AudioClip pencilLoop;
    public AudioClip pencilEnd;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        loadPencilAnimation();
        StartCoroutine("pencilDrawPath");


    }

    void loadPencilAnimation()
    {

        pencilAnimator = GameObject.FindGameObjectWithTag("PencilAnimator").GetComponent<Animator>();
        pencilAnimationObject = GameObject.FindGameObjectWithTag("Pencil");
        currentPath = pathShapes[_Manager.currentLevel].createPathShape();


    }

    public void startCor()
    {
        
        StopCoroutine("pencilDrawPath");
        StartCoroutine("pencilDrawPath");
    }

    IEnumerator pencilDrawAround()
    {
        var reached = false;

        while (!reached)
        {
            x = Mathf.Clamp(t / 2f, 0f, 1f);
            x = x * x * (3 - 2 * x);

            pencilAnimator.Play(animationNames[_Manager.currentLevel], 0, x);

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

    IEnumerator pencilDrawPath()
    {
        var reached = false;

        while (!reached)
        {
            x = Mathf.Clamp(t / 2f, 0f, 1f);
            x = x * x * (3 - 2 * x);
            Vector3 point = currentPath.path.GetPointAtDistance(Mathf.Lerp(0, currentPath.path.length, x));

            var targetPos = point;
            targetPos.y = yPos;
            transform.position = targetPos + initOffset;

            if (x >= 1f)
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

        if(Input.GetMouseButtonDown(0) && !gameFinished)
        {
            myAudio.PlayOneShot(pencilStart);
        }

        if (Input.GetMouseButton(0) && !gameFinished)
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
        else if(Input.GetMouseButtonUp(0))
        {
            if (myAudio.isPlaying)
            {
                myAudio.Stop();
                if (!gameFinished)
                {
                    myAudio.PlayOneShot(pencilEnd);
                }
                _Manager.Agent.stopVibration();

            }


        }
        

        if (!finished && x>=1)
        {
            finished = true;
            FindObjectOfType<NextButton>().activateButton(2);
        }


        initOffset = Vector3.Lerp(initOffset, Vector3.zero, Time.deltaTime * 7f);
    }
}
