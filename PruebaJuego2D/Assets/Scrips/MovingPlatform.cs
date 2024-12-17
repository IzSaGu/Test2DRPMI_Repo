using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //Velocidad de la plataforma
    [SerializeField] int startingPoint; //Numero para determinar el index del punto de inicio de movimiento
    [SerializeField] Transform[] points; //Array de puntos de posicion a los que la plataforma seguira
    int i; //Index que determina que numero de plataforma se persigue actualmente 

    // Start is called before the first frame update
    void Start()
    {
        //Setear la posicion inicial de la plataforma en uno de los puntos
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMove();
    }

    void PlatformMove()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; //Aumentar en uno el index, cambia de objetivo
            if (i == points.Length) i = 0;
        }

        //Movimiento: Siempre despues de la deteccion
        //Mueve la plataforma al punto de array que coincide con el valor de i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
