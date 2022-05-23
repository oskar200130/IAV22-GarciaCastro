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
    [SerializeField]
    private float timer;
    private float timeLeftToSleep;

    public Transform[] patrolPlaces { get; set; } = new Transform[4]; 
    public Transform dogHouse { get; set; }
    public Transform camp { get; set; }
    public Transform insideEstabo { get; set; }
    public Transform door { get; set; }

    public bool Sleeping()
    {
        return false;
    }

    public void Patrol()
    {

    }

    public bool goBackTopSleep()
    {
        return false;
    }

    public bool seeWolf()
    {
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
