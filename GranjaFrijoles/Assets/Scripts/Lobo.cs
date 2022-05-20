using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lobo : MonoBehaviour
{
    public bool EsDeDia()
    {
        // Falta el ciclo de día y noche para hacer bien esta comprobación
        return false;
    }

    public bool EnCueva()
    {
        // Cuando esté el mapa aquí habrá que comprobar si ha llegado a la cueva o no
        return false;
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
