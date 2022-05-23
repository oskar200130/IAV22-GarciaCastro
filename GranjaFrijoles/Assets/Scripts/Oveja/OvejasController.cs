using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvejasController : MonoBehaviour
{
    public bool horaDePastar { get; set; } = false;
    public bool doorOpen { get; set; } = false;
    public bool eatToday { get; set; } = false;
    public Transform puerta;
    private List<Transform> ovejas = new List<Transform>();

    public float timer = 10;
    private float timeLeft = 0;

    public void pushToOvejas(Transform t)
    {
        ovejas.Add(t);
    }

    public void Update()
    {
        if (!horaDePastar && !doorOpen)
        {
            if (timer < timeLeft)
            {
                float dist = 1000;
                int chosenOne = 0;
                for (int i = 0; i < ovejas.Count; i++)
                {
                    float n = Mathf.Abs((puerta.position - ovejas[i].position).magnitude);
                    if (dist > n)
                    {
                        ovejas[chosenOne].gameObject.GetComponent<OvejaState>().closestToDoor = false;
                        dist = n;
                        chosenOne = i;
                        ovejas[chosenOne].gameObject.GetComponent<OvejaState>().closestToDoor = true;
                    }
                }
                timeLeft = 0;
            }
        }
        if (timeLeft < timer)
            timeLeft += Time.deltaTime;
    }

    public Transform scapedSheep()
    {
        for (int i = 0; i < ovejas.Count; i++)
        {
            if (ovejas[i].GetComponent<OvejaState>().escaped())
            {
                return ovejas[i];
            }
        }
        return null;
    }

    public void followDog()
    {
        for (int i = 0; i < ovejas.Count; i++)
        {
            if (!ovejas[i].GetComponent<OvejaState>().escaped())
            {
                ovejas[i].GetComponent<OvejaState>().followingDog = true;
            }
        }
    }

    public void unfollowDog()
    {
        for (int i = 0; i < ovejas.Count; i++)
        {
            ovejas[i].GetComponent<OvejaState>().followingDog = false;
        }
    }
}
