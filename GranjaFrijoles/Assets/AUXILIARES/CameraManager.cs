using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa o desactiva los distintos puntos de vista según la entrada
 */

public class CameraManager : MonoBehaviour
{
    public Camera main, pollo, granjero, pollo2, perro, lobo, campo, libre;

    // Referencia al pollo para cambiar la camara
    [SerializeField]
    PolloController polloRef;


    private void Start()
    {
        main.enabled = true;
        pollo.enabled = false;
        granjero.enabled = false;
        pollo2.enabled = false;
        perro.enabled = false;
        lobo.enabled = false;
        campo.enabled = false;
        libre.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            main.enabled = true;
            pollo.enabled = false;
            granjero.enabled = false;
            pollo2.enabled = false;
            perro.enabled = false;
            lobo.enabled = false;
            campo.enabled = false;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            main.enabled = false;
            pollo.enabled = true;
            granjero.enabled = false;
            pollo2.enabled = false;
            perro.enabled = false;
            lobo.enabled = false;
            campo.enabled = false;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            main.enabled = false;
            pollo.enabled = false;
            granjero.enabled = true;
            pollo2.enabled = false;
            perro.enabled = false;
            lobo.enabled = false;
            campo.enabled = false;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            main.enabled = false;
            pollo.enabled = false;
            granjero.enabled = false;
            pollo2.enabled = true;
            perro.enabled = false;
            lobo.enabled = false;
            campo.enabled = false;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            main.enabled = false;
            pollo.enabled = false;
            granjero.enabled = false;
            pollo2.enabled = false;
            perro.enabled = true;
            lobo.enabled = false;
            campo.enabled = false;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            main.enabled = false;
            pollo.enabled = false;
            granjero.enabled = false;
            pollo2.enabled = false;
            perro.enabled = false;
            lobo.enabled = true;
            campo.enabled = false;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            main.enabled = false;
            pollo.enabled = false;
            granjero.enabled = false;
            pollo2.enabled = false;
            perro.enabled = false;
            lobo.enabled = false;
            campo.enabled = true;
            libre.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            main.enabled = false;
            pollo.enabled = false;
            granjero.enabled = false;
            pollo2.enabled = false;
            perro.enabled = false;
            lobo.enabled = false;
            campo.enabled = false;
            libre.enabled = true;
        }
    }
}


