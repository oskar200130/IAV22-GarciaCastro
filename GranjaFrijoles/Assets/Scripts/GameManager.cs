using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    static public GameManager instance;
    
    private bool day = true;

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
    GameObject light;

    // Colores para las luces
    Color amaneceAtardece = new Color(1f, 0.4f, 0.3f, 1f);
    Color medioDia = new Color(1f, 0.96f, 0.84f, 1f);
    Color noche = new Color(0f, 0f, 0.96f, 1f);

    public void Start()
    {
        instance = this;

        angleLight = 360 / dayDuration;
        light.GetComponent<Light>().color = noche;
        lightTimeDuration = dayDuration / 6;
    }

    // Update is called once per frame
    public bool getDay() { return day; }
    public GameObject getPoints() { return points; }
    public GameObject getScenario() { return scenario; }
    public GameObject getSheepsCtrl() { return sheepsContr; }

    private void Update()
    {
        ActualizaDia();
    }

    void ActualizaDia()
    {
        actualTime += Time.deltaTime;

        // Si ha pasado medio dia es de noche
        if(actualTime >= dayDuration / 2)
        {
            day = false;
        }

        // Si se ha acabado el dia reseteamos el timer
        if(actualTime >= dayDuration)
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
}
