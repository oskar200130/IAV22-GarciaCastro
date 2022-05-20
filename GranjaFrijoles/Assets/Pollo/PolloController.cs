using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloController : MonoBehaviour
{
    GameObject obj;


    [SerializeField]
    GameObject marcador;
    GameObject marcadorGenerado;

    MeshRenderer mesh;

    [SerializeField]
    Material matPermitido;
    [SerializeField]
    Material matNegado;

    UnityEngine.AI.NavMeshAgent navMesh;

    // Animacion
    Animator anim;
    bool walk = false;

    // Start is called before the first frame update
    void Start()
    {
        // Instanciamos el marcador en el suelo
        marcadorGenerado = Instantiate(marcador, transform.position, Quaternion.identity);
        mesh = marcadorGenerado.GetComponent<MeshRenderer>();
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Lanzamos un raycast desde la camara hasta la posicion del raton en el suelo
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        RaycastHit hit;

        // Si el rayo choca con una superficie con capa pointer
        // Esto hay que cambiarlo para que el material se cambie por si es zona navegable o no
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Pointer")))
        {
            // Cambiamos el material 
            mesh.material = matPermitido;

            // Si ademas hemos pulsado movemos al pollo
            if ((Input.GetButton("Fire1") || Input.GetButton("Fire2")) && Camera.main.enabled)
            {
                //obj.transform.position = hit.point;
                if (navMesh.enabled)
                {
                    // Movemos al agente a la posicion
                    navMesh.SetDestination(hit.point);

                }
            }
        }
        // Si choca con otra capa
        else
        {
            // Cambiamos el material 
            mesh.material = matNegado;
        }

        // Ponemos el marcador en el puntero
        marcadorGenerado.transform.position = hit.point + new Vector3(0, (float)0.5, 0);

        // ANIMACION
        if (!walk)
        {
            if (navMesh.velocity.magnitude > 0)
            {
                anim.SetBool("walking", true);
                walk = true;
            }
        }
        else 
        { 
            if(navMesh.velocity.magnitude < 0.5)
            {
                anim.SetBool("walking", false);
                walk = false;
            }
        }

    }

    void Interact()
    {

    }

}

