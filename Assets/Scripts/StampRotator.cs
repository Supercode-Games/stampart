using System.Collections;
using UnityEngine;

public class StampRotator : MonoBehaviour
{

    public void stamp()
    {
        StartCoroutine("stampMove");
    }

    IEnumerator stampMove()
    {
        GameObject.FindWithTag("PaintedTex").transform.SetParent(this.transform);

        Vector3 initialPos = transform.position;
        float velocity = 20f;
        float gravity = 45f;
        float timeOFlight = (2 * velocity) / gravity;
        float t = 0f;
        Quaternion initRot = transform.rotation;
        Quaternion targetRot = initRot * Quaternion.Euler(0, 0, 180);


        while (t<timeOFlight)
        {
            var x = Mathf.Clamp(t / timeOFlight, 0f, 1f);
            transform.rotation = Quaternion.Lerp(initRot, targetRot, x);
            transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
            t += Time.deltaTime;
            velocity -= Time.deltaTime * gravity;

            yield return null;
        }

        transform.position = initialPos;
        transform.rotation = targetRot;


        FindObjectOfType<StampManager>().activateStamp();
        Camera.main.gameObject.GetComponent<Animator>().Play("finish", 0, 0);
        yield return new WaitForSeconds(.4f);
        t = 0f;
        velocity = 20f;
        while (t<=3f)
        {

            t += Time.deltaTime;
            transform.Translate(new Vector3(velocity * Time.deltaTime, velocity * Time.deltaTime),Space.World);

            yield return null;
        }



    }





}
