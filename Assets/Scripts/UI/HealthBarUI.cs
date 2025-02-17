using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    Slider healthSlider;
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.value = 1;
    }

    public void UpdateHealth(int health, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }
}