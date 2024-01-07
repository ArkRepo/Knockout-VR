using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

   [SerializeField] private Image HealthbarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        HealthbarSprite.fillAmount = currentHealth/ maxHealth;

    }
  
}
