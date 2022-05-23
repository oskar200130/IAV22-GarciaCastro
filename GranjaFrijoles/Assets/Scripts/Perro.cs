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
    [SerializeField]
    private float timer;
    private float timeLeftToSleep;

    
    public void Sleeping()
    {

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
}
