using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;

    public void SetPower(int power)
    {
        slider.value = power;
    }

    public void setMaxPower(int maxPower)
    {
        slider.maxValue = maxPower;
        slider.value = maxPower;
    }

    public void setMinPower(int minPower)
    {
        slider.minValue = minPower;
        if (slider.value < minPower) slider.value = minPower;
    }
}
