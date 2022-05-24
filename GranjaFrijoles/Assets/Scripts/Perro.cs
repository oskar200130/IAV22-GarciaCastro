using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro : MonoBehaviour
{
    public bool takeOut { get; set; } = false;
    public bool sheepIsLost { get; set; } = false;
    public Transform chicken;
    public Transform lobo;
    public OvejasController sheepController;

    public Transform lostSheep { get; set; }
    public bool carringSheep { get; set; } = false;
    public int distanceWander;
    public float timeWandering;
    public float startWander = 0;
    private int countWanders = 0;
    [SerializeField]
    public float timer;
    private float timeLeftToSleep = 0;
    private NavMeshAgent agente;
    private bool arrived = true;
    private Vector3 pos = new Vector3();
    Animator anim;
    bool walk = false;

    public Transform[] patrolPlaces { get; set; } = new Transform[4]; 
    private Transform[] routePlaces { get; set; } = new Transform[4]; 
    public Transform dogHouse { get; set; }
    public Transform camp { get; set; }
    public Transform insideEstabo { get; set; }
    public Transform door { get; set; }

    private int routeStep = 0;

    public double anguloVistaHorizontal;   // Distancia maxima de vision
    public double distanciaVista;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public bool Sleeping()
    {
        if (GameManager.instance.getAudiosActivosPerro() == 1)
        {
            GameManager.instance.deactivatePerroAudioTrigger();
            return false;
        }
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
        if (countWanders < 5 && arrived)
        {
            wander();
        }
        else if (arrived)
        {
            int r = Random.Range(0, 4);
            agente.SetDestination(patrolPlaces[r].position);
            pos = patrolPlaces[r].position;
            arrived = false;
        }
        else if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            arrived = true;
            countWanders = 0;
        }
    }

    public bool Ruta()
    {
        if (routeStep == 0)
        {
            agente.SetDestination(door.position);
            routeStep++;
        }
        else if (routeStep <= 4)
        {
            if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
            {
                sheepController.followDog();
                agente.SetDestination(routePlaces[routeStep-1].position);
                routeStep++;
            }
        }
        else if (routeStep == 5 && (transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            agente.SetDestination(insideEstabo.position);
            if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
            {
                routeStep++;
                sheepController.unfollowDog();
                sheepController.horaDePastar = false;
                agente.SetDestination(door.position);
            }
        }
        else if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            sheepController.doorOpen = false;
            sheepController.horaDePastar = false;
            GameManager.instance.getScenario().GetComponent<Scenario>().puertaValla.GetComponent<Animator>().SetBool("Open", false);
            takeOut = false;
            routeStep = 0;
            return true;
        }
        return false;
    }

    public bool seeWolf()
    {
        NavMeshHit navHit;
        Vector3 pos = transform.position;
        Vector3 posLobo = lobo.transform.position;
        NavMesh.SamplePosition(posLobo, out navHit, distanceWander, NavMesh.AllAreas);
        pos.y += 0.5f;
        posLobo.y += 0.5f;
        double fwdAngle = Vector3.Angle(transform.forward, posLobo - pos);
        if (fwdAngle < anguloVistaHorizontal && Vector3.Magnitude(pos - posLobo) <= distanciaVista && (1 << NavMesh.GetAreaFromName("CampoEscape") & navHit.mask) == 0)
        {
            RaycastHit vista;
            if (Physics.Raycast(pos, posLobo - pos, out vista, Mathf.Infinity) && vista.collider.gameObject == lobo.gameObject)
            {
                lobo.gameObject.GetComponent<Lobo>().visto = true;
                return true;
            }
        }
        lobo.gameObject.GetComponent<Lobo>().visto = false;
        return false;
    }

    public void setLostSheep()
    {
        lostSheep = sheepController.scapedSheep();
    }

    public bool catchSheep()
    {
        if ((transform.position - lostSheep.position).magnitude <= 0.1 + agente.stoppingDistance)
        {
            lostSheep.parent = transform;
            lostSheep.GetComponent<OvejaState>().atrapada = true;
            lostSheep.GetComponent<Collider>().isTrigger = true;
            return true;
        }
        return false;
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
                countWanders++;
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
        while ((1 << NavMesh.GetAreaFromName("Ovejas") & navHit.mask) == 0 &&
        (1 << NavMesh.GetAreaFromName("Campo") & navHit.mask) == 0 &&
        (1 << NavMesh.GetAreaFromName("Perro") & navHit.mask) == 0 &&
        (1 << NavMesh.GetAreaFromName("Granja") & navHit.mask) == 0 &&
        (1 << NavMesh.GetAreaFromName("CampoEscape") & navHit.mask) == 0 &&
        (1 << NavMesh.GetAreaFromName("CampoPerseguir") & navHit.mask) == 0);
        return navHit.position;
    }

    public bool freeSheep()
    {
        if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            lostSheep.parent = null;
            lostSheep.GetComponent<OvejaState>().atrapada = false;
            lostSheep.GetComponent<OvejaState>().enEstablo = true;
            lostSheep.GetComponent<Collider>().isTrigger = false;
            return true;
        }
        return false;
    }

    public void Start()
    {
        Places p  = GameManager.instance.getPoints().GetComponent<Places>();
        dogHouse = p.casaPerro.transform;
        camp = p.campo.transform;
        insideEstabo = p.dentroEstablo.transform;
        door = p.puertaValla.transform;
        patrolPlaces[0] = p.patrol1.transform;
        patrolPlaces[1] = p.patrol2.transform;
        patrolPlaces[2] = p.patrol3.transform;
        patrolPlaces[3] = p.patrol4.transform;
        routePlaces[0] = p.route1.transform;
        routePlaces[1] = p.route2.transform;
        routePlaces[2] = p.route3.transform;
        routePlaces[3] = p.route4.transform;

    }

    public void Update()
    {
        // Animator
        if (agente.enabled && agente.velocity.magnitude > 0.1 && !walk)
        {
            anim.SetBool("walking", true);
            walk = true;
        }
        else if (walk)
        {
            anim.SetBool("walking", false);
            walk = false;
        }
    }
}
