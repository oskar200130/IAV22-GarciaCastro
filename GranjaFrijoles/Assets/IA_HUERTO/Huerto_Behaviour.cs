using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Huerto_Behaviour : MonoBehaviour
{
    // Prefabs de los tipos de huerto
    [SerializeField]
    GameObject regado;
    [SerializeField]
    GameObject seco;
    [SerializeField]
    GameObject pisado;

    // Referencias a los huertos 
    GameObject huertoIz;
    GameObject huertoDer;
    // Y sus respectivas posiciones
    [SerializeField]
    Transform izPos;
    [SerializeField]
    Transform derPos;


    // Start is called before the first frame update
    void Start()
    {
        // Instanciamos huertos secos porque es por la mañana
        huertoIz = Instantiate(seco, izPos);
        huertoDer = Instantiate(seco, derPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Elimina los huertos que hay puestos, usar antes de instanciar nuevos
    private void destroyHuerto()
    {
        Destroy(huertoIz);
        Destroy(huertoDer);
    }

    // Se llama al pasar al estado regado
    public void setRegado()
    {
        // Destruimos los antiguos huertos
        destroyHuerto();

        // Creamos los nuevos 
        huertoDer = Instantiate(regado, derPos);
        huertoIz = Instantiate(regado, izPos);
    }

    // Se llama al pasar al estado seco
    public void setSeco()
    {
        // Destruimos los antiguos huertos
        destroyHuerto();

        // Creamos los nuevos 
        huertoDer = Instantiate(seco, derPos);
        huertoIz = Instantiate(seco, izPos);
    }

    // Se llama al pasar al estado pisado
    public void setPisado()
    {
        // Destruimos los antiguos huertos
        destroyHuerto();

        // Creamos los nuevos 
        huertoDer = Instantiate(pisado, derPos);
        huertoIz = Instantiate(pisado, izPos);
    }
}
