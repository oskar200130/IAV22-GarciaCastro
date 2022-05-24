using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lobo : MonoBehaviour
{
    public GameObject Cueva;
    public List<GameObject> Ovejas;
    public bool visto = false;
    GameObject ovejaObjetivo;
    NavMeshAgent agente;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public bool EsDeDia()
    {
        return GameManager.instance.getDay();
    }

    public bool EnCueva()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        return (1 << NavMesh.GetAreaFromName("Cueva") & navHit.mask) != 0;
    }

    public bool Visto()
    {
        return visto;
    }

    public bool TieneOveja()
    {
        return transform.childCount > 2;
    }

    public void SetOveja()
    {
        int i = Random.Range(0, Ovejas.Count);
        ovejaObjetivo = Ovejas[i];
    }

    public Vector3 GetOvejaPos()
    {
        return ovejaObjetivo.transform.position;
    }

    public void CogeOveja()
    {
        if(Vector3.Distance(transform.position, GetOvejaPos()) < 2)
        {
            ovejaObjetivo.transform.parent = transform;
            ovejaObjetivo.GetComponent<OvejaState>().atrapada = true;
        }
    }

    public void MataOveja()
    {
        ovejaObjetivo.GetComponent<OvejaState>().muerta = true;
        ovejaObjetivo.SetActive(false);
        ovejaObjetivo.transform.SetParent(null);
        Ovejas.Remove(ovejaObjetivo);
        ovejaObjetivo = null;
    }
}
