using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lobo : MonoBehaviour
{
    public GameObject Cueva;

    public bool EsDeDia()
    {
        // Falta el ciclo de día y noche para hacer bien esta comprobación
        return false;
    }

    public void VuelveCueva()
    {

    }

    public bool EnCueva()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        return (1 << NavMesh.GetAreaFromName("Cueva") & navHit.mask) != 0;
    }

    public bool Visto()
    {
        // Devolverá true si el lobo tiene que huir del perro/granjero
        return false;
    }

    public bool TieneOveja()
    {
        // Devuelve true si tiene una oveja capturada
        return false;
    }
}
