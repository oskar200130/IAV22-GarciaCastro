using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_Interact : MonoBehaviour
{
    bool sePuedePisar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si ha entrado el pollo
        if(other.GetComponent<PolloInteract>())
        {
            other.GetComponent<PolloInteract>().sePuedeAbrir = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Comprobamos si ha entrado el pollo
        if (other.GetComponent<PolloInteract>())
        {
            other.GetComponent<PolloInteract>().sePuedeAbrir = false;
        }
    }
}
