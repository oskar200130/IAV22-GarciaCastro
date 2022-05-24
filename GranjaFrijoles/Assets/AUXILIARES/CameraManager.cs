using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa o desactiva los distintos puntos de vista según la entrada
 */

public class CameraManager : MonoBehaviour
{
    public Camera main, pollo;

    // Referencia al pollo para cambiar la camara
    [SerializeField]
    PolloController polloRef;


    private void Start()
    {
        main.enabled = true;
        pollo.enabled = false;
        //polloRef.setCamera(main);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            main.enabled = true;
            pollo.enabled = false;
            //polloRef.setCamera(main);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            main.enabled = false;
            pollo.enabled = true;
            //polloRef.setCamera(pollo);
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    main.enabled = false;
        //    pollo.enabled = false;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    main.enabled = false;
        //    pollo.enabled = false;
        //}
    }
}


