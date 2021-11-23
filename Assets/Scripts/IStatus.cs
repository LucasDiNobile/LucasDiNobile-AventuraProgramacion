using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IStatus : MonoBehaviour
{
    public float health, hunger, thirst;
    public float healthTick, hungerTick, thirstTick;
    public float healthMax, hungerMax, thirstMax;

    public Slider healthUI;
    public Slider hungerUI;
    public Slider thirstUI;

    

    private void Update()
    {
        healthUI.value = health;
        thirstUI.value = thirst;
        hungerUI.value = hunger;

        if (health < healthMax) health += Time.deltaTime / healthTick;
        if (hunger > 0) hunger -= Time.deltaTime / hungerTick;
        if (thirst > 0) thirst -= Time.deltaTime / thirstTick;

        if(hunger <= 50 && health > healthMax) health -= Time.deltaTime / healthTick;

        //hunger = Mathf.Clamp(hunger += Time.deltaTime / hungerTick, 0, hungerMax);
    }
}

