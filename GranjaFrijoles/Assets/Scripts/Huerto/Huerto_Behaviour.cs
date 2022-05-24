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
    [SerializeField]
    GameObject huertoIz;
    [SerializeField]
    GameObject huertoDer;
    // Y sus respectivas posiciones
    [SerializeField]
    Transform izPos;
    [SerializeField]
    Transform derPos;

    // Booleano para comprobar si esta pisado el huerto
    public bool estaPisado = false;
    public bool estaRegado = false;
    public bool estaSeco = false;

    // Se guarda el ultimo estado (regado o seco)
    string ultimoEstado = "";

    // Start is called before the first frame update
    void Start()
    {
        
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
        estaPisado = estaSeco = false;
        estaRegado = true;
    }

    // Se llama al pasar al estado seco
    public void setSeco()
    {
        // Destruimos los antiguos huertos
        destroyHuerto();

        // Creamos los nuevos 
        huertoDer = Instantiate(seco, derPos);
        huertoIz = Instantiate(seco, izPos);
        estaPisado = estaRegado = false;
        estaSeco = true;
    }

    // Se llama al pasar al estado pisado
    public void setPisado()
    {
        // Destruimos los antiguos huertos
        destroyHuerto();

        // Creamos los nuevos 
        huertoDer = Instantiate(pisado, derPos);
        huertoIz = Instantiate(pisado, izPos);
        estaSeco = estaRegado = false;
        estaPisado = true;
    }

    public bool estaPisadoComprueba()
    {
        return estaPisado;
    }
    public bool estaRegadoComprueba()
    {
        return estaRegado;
    }
    public bool estaSecoComprueba()
    {
        return estaSeco;
    }
    public void guardaEstado(string estado)
    {
        ultimoEstado = estado;
    }
    public bool estaReparadoSeco()
    {
        return !estaPisado && ultimoEstado == "seco";
    }
    public bool estaReparadoRegado()
    {
        return !estaPisado && ultimoEstado == "regado";
    }

}
