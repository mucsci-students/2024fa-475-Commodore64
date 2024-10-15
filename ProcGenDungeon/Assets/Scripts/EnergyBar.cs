using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;

    private float timeUntilRegen;

    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }

    public void SetEnergy(int energy)
    {
        slider.value = energy;
    }

    public void decreaceEnergy(int value)
    {
        slider.value -= value;
        if (slider.value < 0)
        {
            slider.value = 0;
        }
    }
    public void increaceEnergy(int value)
    {
        slider.value += value;
        if (slider.value > 100)
        {
            slider.value = 100;
        }
    }

    void Update()
    {
        timeUntilRegen -= Time.deltaTime;

        if (timeUntilRegen <= 0)
        {
            increaceEnergy(5);
            SetTimeUntilRegen();
        }
    }

    public int getEnergy()
    {
        return (int)slider.value;
    }

    private void SetTimeUntilRegen()
    {
        timeUntilRegen = 3f;
    }
}
