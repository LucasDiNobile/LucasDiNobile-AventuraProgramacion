using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaNoche : MonoBehaviour
{
    public GameObject diaNoche;
    public float velocidad;

    // Update is called once per frame
    void Update()
    {
        diaNoche.transform.Rotate(velocidad, 0, 0);
    }
}
