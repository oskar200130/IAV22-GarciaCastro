using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huerto_Interact : MonoBehaviour
{

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
        if(other.GetComponent<PolloController>())
        {
            Debug.Log("Pollodentro");
            other.GetComponent<PolloInteract>().sePuedePisar = true;
        }
    }
}
