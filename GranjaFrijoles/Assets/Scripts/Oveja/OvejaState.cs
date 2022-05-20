using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvejaState : MonoBehaviour
{
    public bool closestToDoor { get; set; }
    public bool enEstablo { get; set; }
    public bool pastar { get; set; }
    public bool atrapada { get; set; }
    public bool muerta { get; set; }

    public bool loboCerca()
    {
        return false;
    }

    public bool doorOpen()
    {
        return false;
    }
}
