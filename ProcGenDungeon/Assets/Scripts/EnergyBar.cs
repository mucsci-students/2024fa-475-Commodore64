using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;

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
    public int getEnergy()
    {
        return (int)slider.value;
    }
}
