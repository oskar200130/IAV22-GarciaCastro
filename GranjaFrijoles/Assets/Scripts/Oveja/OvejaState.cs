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
    public GameObject lobo;
    public OvejasController controller;
    public float maxVel;
    [Range(0, 180)]
    public double anguloVistaHorizontal;   // Distancia maxima de vision
    public double distanciaVista;

    public bool closestToDoor { get; set; }
    public bool enEstablo { get; set; } = true;
    public bool pastar { get; set; } = false;
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
        Vector3 pos = transform.position;
        Vector3 posLobo = lobo.transform.position;
        pos.y += 0.5f;
        posLobo.y += 0.5f;
        double fwdAngle = Vector3.Angle(transform.forward, posLobo - pos);

        if (fwdAngle < anguloVistaHorizontal && Vector3.Magnitude(pos - posLobo) <= distanciaVista)
        {
            RaycastHit vista;
            if (Physics.Raycast(pos, posLobo - pos, out vista, Mathf.Infinity) && vista.collider.gameObject == lobo)
            {
                return true;
            }
        }
        return false;
    }

    public bool doorOpen()
    {
        return controller.doorOpen;
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
        NavMeshHit navHit;
        do
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distanceWander;
            randomDirection += gameObject.transform.position;
            NavMesh.SamplePosition(randomDirection, out navHit, distanceWander, NavMesh.AllAreas);
        }
        while (((1 << NavMesh.GetAreaFromName("Ovejas") & navHit.mask) == 0 && enEstablo) ||
            ((1 << NavMesh.GetAreaFromName("Campo") & navHit.mask) == 0 && !enEstablo));
        return navHit.position;
    }

    public void runaway()
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
        while ((1 << NavMesh.GetAreaFromName("Pradera") & navHit.mask) == 0 );
        agente.SetDestination(navHit.position);
    }

    public void escapeWolf()
    {
        Vector3 pos = transform.position;
        Vector3 posLobo = lobo.transform.position;
        pos -= posLobo - pos;
        agente.SetDestination(pos);
    }

    public void Update()
    {
        pastar = controller.horaDePastar;
    }

    public void Start()
    {
        controller.pushToOvejas(transform);
    }

    public bool escaped()
    {
        return !enEstablo && !pastar;
    }
}
