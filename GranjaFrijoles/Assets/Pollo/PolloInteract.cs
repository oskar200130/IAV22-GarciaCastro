using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloInteract : MonoBehaviour
{
    // Variables para controlar las interacciones
    public bool sePuedePisar;
    public bool sePuedeAbrir;
    public bool sePuedeCogerComida;

    // Referencia al Huerto
    [SerializeField]
    Huerto_Behaviour huerto;


    void Start()
    {
        
    }

    void Update()
    {
        Interact();
    }

    void Interact()
    {
        // Si estamos en la zona de pisar el huerto
        if(sePuedePisar)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                huerto.estaPisado = true;
                Debug.Log("Lohepisao");
            }
        }
    }
}
