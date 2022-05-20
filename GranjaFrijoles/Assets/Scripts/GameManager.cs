using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    static public GameManager instance;
    
    private bool day = false;
    [SerializeField]
    GameObject points;

    public void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public bool getDay() { return day; }
    public GameObject getPoints() { return points; }
}
