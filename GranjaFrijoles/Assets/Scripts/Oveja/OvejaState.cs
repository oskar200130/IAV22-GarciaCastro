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
    Animator anim;
    bool walk = false;

    public bool closestToDoor { get; set; }
    public bool enEstablo { get; set; } = true;
    public bool pastar { get; set; } = false;
    public bool atrapada { get; set; }
    public bool muerta { get; set; }
    public bool followingDog { get; set; }

    private NavMeshAgent agente;
    private Transform[] randPoints = new Transform[8];

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        int rand = Random.Range(0, randPoints.Length);
        agente.SetDestination(randPoints[rand].position);
        sheepIsLost();
        enEstablo = false;
    }

    public void sheepIsLost()
    {
        dog.GetComponent<Perro>().sheepIsLost = true;
    }

    public void escapeWolf()
    {
        Vector3 pos = transform.position;
        Vector3 posLobo = lobo.transform.position;
        pos -= posLobo - pos;
        NavMeshHit navHit;
        NavMesh.SamplePosition(pos, out navHit, distanceWander, NavMesh.AllAreas);
        if ((1 << NavMesh.GetAreaFromName("Ovejas") & navHit.mask) != 0 && enEstablo ||
            (1 << NavMesh.GetAreaFromName("Campo") & navHit.mask) != 0 && !enEstablo)
            agente.SetDestination(pos);
    }

    public void Update()
    {
        pastar = controller.horaDePastar;

        // Animator
        if(agente.enabled && agente.velocity.magnitude > 0.1 && !walk)
        {
            anim.SetBool("walking", true);
            walk = true;
        }
        else if(walk)
        {
            anim.SetBool("walking", false);
            walk = false;
        }

    }

    public void Start()
    {
        controller.pushToOvejas(transform);
        Places p = GameManager.instance.getPoints().GetComponent<Places>();
        randPoints[0] = p.campSide.transform;
        randPoints[1] = p.patrol1.transform;
        randPoints[2] = p.patrol2.transform;
        randPoints[3] = p.patrol3.transform;
        randPoints[4] = p.route1.transform;
        randPoints[5] = p.route2.transform;
        randPoints[6] = p.route3.transform;
        randPoints[7] = p.route4.transform;
    }

    public bool escaped()
    {
        return !enEstablo && !pastar;
    }

    public void setAnimIdle()
    {
        //anim.SetBool("walking", false);
    }
    public void setAnimWal()
    {
        //anim.SetBool("walking", true);
    }
}
