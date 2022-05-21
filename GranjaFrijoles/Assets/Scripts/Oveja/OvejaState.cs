using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OvejaState : MonoBehaviour
{
    public float distanceWander;
    public float timeWandering;
    public float startWander = 0;
    public GameObject dog;

    public bool closestToDoor { get; set; }
    public bool enEstablo { get; set; }
    public bool pastar { get; set; }
    public bool atrapada { get; set; }
    public bool muerta { get; set; }
    public bool followingDog { get; set; }

    private NavMeshAgent agente;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public bool loboCerca()
    {
        return false;
    }

    public bool doorOpen()
    {
        return false;
    }

    public void wander()
    {
        if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            startWander -= Time.deltaTime;
            if (startWander <= 0)
            {
                startWander = timeWandering;
                agente.SetDestination(getRandPoint());
            }
        }

    }

    private Vector3 getRandPoint()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distanceWander;
        randomDirection += gameObject.transform.position;
        NavMeshHit navHit;
        do
        {
            randomDirection = UnityEngine.Random.insideUnitSphere * distanceWander;
            randomDirection += gameObject.transform.position;
            NavMesh.SamplePosition(randomDirection, out navHit, distanceWander, NavMesh.AllAreas);
        }
        while (((1 << NavMesh.GetAreaFromName("Ovejas") & navHit.mask) == 0 && !enEstablo) &&
            ((1 << NavMesh.GetAreaFromName("Pradera") & navHit.mask) == 0 && enEstablo));
        return navHit.position;
    }
}
