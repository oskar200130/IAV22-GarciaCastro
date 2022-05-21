using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloInteract : MonoBehaviour
{
    // Variables para controlar las interacciones
    public bool sePuedePisar;
    public bool sePuedeAbrir;
    public bool sePuedeCogerComida;
    public bool sePuedeHacerRuido;
    bool conComida = false;

    // Referencia al Huerto
    [SerializeField]
    Huerto_Behaviour huerto;

    // Para guardarnos la comida
    Transform paja;


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
        else if(sePuedeCogerComida)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                conComida = !conComida;
                if (!conComida)
                    cogerComida();
                else
                    soltarComida();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void cogerComida()
    {
        paja.SetParent(transform);
        Debug.Log("Man");
    }
    void soltarComida()
    {
        paja.SetParent(null);
        paja = null;
        Debug.Log("Man");
    }
    public void setComida(Transform com)
    {
        paja = com;
    }
}
