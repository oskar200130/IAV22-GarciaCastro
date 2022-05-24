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

    // Referencia al controlador de ovejas
    [SerializeField]
    OvejasController ovejas;

    // A la puerta
    [SerializeField]
    GameObject puerta;

    // Para guardarnos la comida
    Transform paja;

    // Para guardarnos el audio que hemos activado
    GameObject audioTrigger;


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
                if (!conComida)
                    cogerComida();
                else
                    soltarComida();
                conComida = !conComida;
            }
        }
        else if (sePuedeAbrir)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                abrirPuerta();
                Debug.Log("PuertaAbierta");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<AudioSource>().Play();
            if (sePuedeHacerRuido)
            {
                GameManager.instance.activaAudioTrigger(audioTrigger);
            }
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
    public void unsetComida()
    {
        paja = null;
    }
    public void setAudioTrigger(GameObject au)
    {
        audioTrigger = au;
    }

    void abrirPuerta()
    {
        if(!ovejas.doorOpen)
        {
            ovejas.doorOpen = true;
            puerta.GetComponent<Animator>().SetBool("Open", true);
            Debug.Log("Vas?");
        }
    }
}
