using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class DrillerManager2 : MonoBehaviour
{
    public float leftX = -5.06f;
    public float rightX = 6.09f;

    public float topZ = 12.34f;
    public float bottomZ = 0.64f;

    public GameObject follower;
    public GameObject tutorial;

    public float sensitivity;

    PathCreator pathCreator;
    public List<PathCreatorForShape> pathShapes;
    public GameObject pencil;
    public MeshRenderer laserIndic;

    public Material laserMat;
    public Material laserIndicMat;

    public ParticleSystem smoke;

    public GameObject refObjPrefab;
    List<Ref> refs = new List<Ref>();
    bool finished;

    public AudioSource myAudioSource;

    public bool isPencil;

    public float pencilY;
    public float drillerY;

    void Start()
    {
        follower.transform.position += new Vector3(5, 0, 0);
    }

    public void loadPath()
    {
        myAudioSource = GetComponent<AudioSource>();

        var PATHS = new GameObject("PATHS");
        pathCreator = pathShapes[_Manager.currentLevel].createPathShape();
        float dist = 0f;

        while (dist<=pathCreator.path.length)
        {
            var point = pathCreator.path.GetPointAtDistance(dist);
            point.y = refObjPrefab.transform.position.y;
            var k = Instantiate(refObjPrefab, point, Quaternion.identity);
            k.transform.SetParent(PATHS.transform);
           refs.Add(k.GetComponent<Ref>());
            dist += .1f;
        }
        isPencil = false;
        transform.position = refs[0].transform.position;

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            tutorial.SetActive(false);

        }



        if (Input.GetMouseButton(0))
        {
            var x = Input.GetAxis("Mouse X") * sensitivity;
            var y = Input.GetAxis("Mouse Y") * sensitivity;

            transform.position += new Vector3(x, 0, y);
            if (!smoke.isPlaying && !isPencil)
            {
                smoke.Play();
                myAudioSource.Play();
            }

        }
        else
        {
            if (smoke.isPlaying && !isPencil)
            {
                smoke.Stop();
                myAudioSource.Stop();
            }

        }

        var closestPoint = pathCreator.path.GetClosestPointOnPath(transform.position);
        if (Vector2.Distance(new Vector2(closestPoint.x, closestPoint.z), new Vector2(transform.position.x, transform.position.z)) < 6f)
        {
            var p = follower.transform.position;
            p.x = closestPoint.x;
            p.z = closestPoint.z;
            p.y = 9.1f;

            var h = p;
            h.y = pencil.transform.position.y;

            laserIndic.material = laserMat;

            follower.transform.position = Vector3.Lerp(follower.transform.position, p, Time.deltaTime * 20f);
            pencil.transform.position = h;

            smoke.gameObject.transform.position = new Vector3(h.x, -0.29f, h.z);

        }
        else
        {
            var p = follower.transform.position;
            p.x = transform.position.x;
            p.z = transform.position.z;
            p.y = 9.1f;

            laserIndic.material = laserIndicMat;

            follower.transform.position = Vector3.Lerp(follower.transform.position, p, Time.deltaTime * 5f);


        }


        if (refs.Count > 0 && !finished)
        {
            int target = refs.Count;
            int current = 0;
            foreach (var item in refs)
            {
                if (item.hit)
                {
                    current++;
                }
            }


            if (current >= (target - 10))
            {
                finished = true;
                smoke.gameObject.SetActive(false);
                myAudioSource.Stop();
                FindObjectOfType<NextButton>().finishPhase1();
            }

        }
    }
}
