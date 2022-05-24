using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    static public GameManager instance;
    
    private bool day = true;
    private bool meet = false;

    [SerializeField]
    float dayDuration;
    float actualTime = 0.0f;
    float angleLight;
    float lightTime = 0.0f;
    float lightTimeDuration;

    [SerializeField]
    GameObject points;
    [SerializeField]
    GameObject scenario;
    [SerializeField]
    GameObject sheepsContr;
    [SerializeField]
    GameObject perro;
    [SerializeField]
    GameObject lobo;

    [SerializeField]
    GameObject light;

    [SerializeField]
    GameObject[] audioTriggersGranjero;
    int audiosActivosGranjero = 0; // para indicar cuantos ha activado la gallina
    [SerializeField]
    GameObject[] audioTriggersPerro;
    int audiosActivosPerro = 0; // para indicar cuantos ha activado la gallina
    [SerializeField]
    Material MatAudio1;
    [SerializeField]
    Material MatAudio2;

    // Colores para las luces
    Color amaneceAtardece = new Color(1f, 0.4f, 0.3f, 1f);
    Color medioDia = new Color(1f, 0.96f, 0.84f, 1f);
    Color noche = new Color(0f, 0f, 0.96f, 1f);

    public void Awake()
    {
        instance = this;

        angleLight = 360 / dayDuration;
        light.GetComponent<Light>().color = noche;
        lightTimeDuration = dayDuration / 6;
    }

    // Update is called once per frame
    public bool getDay() { return day; }
    public bool getMeet() { return meet; }
    public GameObject getPoints() { return points; }
    public GameObject getScenario() { return scenario; }
    public GameObject getSheepsCtrl() { return sheepsContr; }

    public void destroyGameObject(GameObject o)
    {
        Destroy(o);
    }

    private void Update()
    {
        ActualizaDia();
        ManejaAudios();
    }

    void ActualizaDia()
    {
        actualTime += Time.deltaTime;

        // Si ha pasado medio dia es de noche
        if(actualTime >= dayDuration / 2)
        {
            day = false;
            meet = false;
        }
        if (actualTime >= dayDuration / 4)
        {
            meet = true;
        }
        // Si se ha acabado el dia reseteamos el timer
        if (actualTime >= dayDuration)
        {
            day = true;
            actualTime = 0.0f;
        }

        // Rotar la luz en funcion del tiempo
        light.transform.Rotate(Vector3.right, angleLight * Time.deltaTime);



        // Color de la luz en funcion de la "hora"
        // Amanecer
        if (actualTime <= dayDuration / 6)
            light.GetComponent<Light>().color = Color.Lerp(noche, amaneceAtardece, lightTime);
        // Mediodia
        else if (actualTime > dayDuration / 6 && actualTime <= (dayDuration / 6) * 2)
            light.GetComponent<Light>().color = Color.Lerp(amaneceAtardece, medioDia, lightTime);
        // Atardecer
        else if (actualTime > (dayDuration / 6) * 2 && actualTime <= (dayDuration / 6) * 3)
            light.GetComponent<Light>().color = Color.Lerp(medioDia, amaneceAtardece, lightTime);
        // Anochece
        else if (actualTime > (dayDuration / 6) * 3 && actualTime <= (dayDuration / 6) * 4)
            light.GetComponent<Light>().color = Color.Lerp(amaneceAtardece, noche, lightTime);

        // Timer para el lerp del color
        if (lightTime < 1)
            lightTime += Time.deltaTime / lightTimeDuration;
        else 
            lightTime = 0.0f;
    }

    void ManejaAudios()
    {
        if(day)
        {
            audiosActivosGranjero = 0;
            audiosActivosPerro = 0;
            for (int i = 0; i < audioTriggersGranjero.Length; i++)
            {
                audioTriggersGranjero[i].GetComponent<MeshRenderer>().material = MatAudio1;
                audioTriggersGranjero[i].SetActive(false);
            }
            for (int i = 0; i < audioTriggersPerro.Length; i++)
            {
                audioTriggersPerro[i].GetComponent<MeshRenderer>().material = MatAudio1;
                audioTriggersPerro[i].SetActive(false);
            }
        }
        else 
        {
            for (int i = 0; i < audioTriggersGranjero.Length; i++)
                audioTriggersGranjero[i].SetActive(true);
            for (int i = 0; i < audioTriggersPerro.Length; i++)
                audioTriggersPerro[i].SetActive(true);
        }
    }

    public void activaAudioTrigger(GameObject a)
    {
        if (a.tag == "AudioGranjero")
            audiosActivosGranjero++;
        else
            audiosActivosPerro++;

        Debug.Log("Granjero: " + audiosActivosGranjero + " Perro: " + audiosActivosPerro);

        a.GetComponent<MeshRenderer>().material = MatAudio2; 
    }

    // Esto lo suyo es que se use en la maquina de estados del perro o granjero
    public int getAudiosActivosGranjero()
    {
        return audiosActivosGranjero;
    }
    public int getAudiosActivosPerro()
    {
        return audiosActivosPerro;
    }

    public GameObject getPerro()
    {
        return perro;
    }
    public GameObject getLobo()
    {
        return lobo;
    }
}
