using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro : MonoBehaviour
{
    public bool takeOut { get; set; } = false;
    public bool takeIn { get; set; } = false;
    public bool sheepIsLost { get; set; } = false;
    public Transform chicken;
    public Transform lobo;
    public OvejasController sheepController;

    public Transform lostSheep { get; set; }
    public bool carringSheep { get; set; } = false;
    public int distanceWander;
    public float timeWandering;
    public float startWander = 0;
    public float countWanders = 0;
    [SerializeField]
    public float timer;
    private float timeLeftToSleep = 0;
    private NavMeshAgent agente;
    private bool arrived = true;
    private Vector3 pos = new Vector3();

    public Transform[] patrolPlaces { get; set; } = new Transform[4]; 
    public Transform dogHouse { get; set; }
    public Transform camp { get; set; }
    public Transform insideEstabo { get; set; }
    public Transform door { get; set; }

    public double anguloVistaHorizontal;   // Distancia maxima de vision
    public double distanciaVista;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public bool Sleeping()
    {
        if (GameManager.instance.getAudiosActivosPerro() == 1)
            return false;
        else return true;
    }

    public bool goBackTopSleep()
    {
        if (timer < timeLeftToSleep)
        {
            timeLeftToSleep = 0;
            return true;
        }
        if (timeLeftToSleep < timer)
            timeLeftToSleep += Time.deltaTime;

        return false;
    }

    public void Patrol()
    {
        if (countWanders < 5 && !arrived)
        {
            wander();
            countWanders++;
        }
        else if (!arrived)
        {
            int r = Random.Range(0, 4);
            agente.SetDestination(patrolPlaces[r].position);
            pos = patrolPlaces[r].position;
            arrived = false;
        }
        else if (transform.position == pos)
        {
            arrived = true;
        }
    }


    public bool seeWolf()
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

    public void setLostSheep()
    {
        lostSheep = sheepController.scapedSheep();
    }

    public void catchSheep()
    {
        lostSheep.parent = transform;
        lostSheep.GetComponent<OvejaState>().atrapada = true;
    }

    private void wander()
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
        while ((1 << NavMesh.GetAreaFromName("Ovejas") & navHit.mask) == 0);
        return navHit.position;
    }

    public void freeSheep()
    {
        lostSheep.parent = null;
        lostSheep.GetComponent<OvejaState>().atrapada = false;
    }

    public void Start()
    {
        Places p  = GameManager.instance.getPoints().GetComponent<Places>();
        dogHouse = p.dogHouse.transform;
        camp = p.campSide.transform;
        insideEstabo = p.insideEstablo.transform;
        door = p.door.transform;
        patrolPlaces[0] = p.patrol1.transform;
        patrolPlaces[1] = p.patrol2.transform;
        patrolPlaces[2] = p.patrol3.transform;
        patrolPlaces[3] = p.patrol4.transform;

    }
}
