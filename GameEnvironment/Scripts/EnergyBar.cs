using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.maxValue = 100;
        float totalBoost = PlayerPrefs.GetFloat("TotalBoost", 0f);
        slider.value = totalBoost;
    }
}