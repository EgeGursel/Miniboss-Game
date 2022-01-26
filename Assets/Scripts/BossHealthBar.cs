using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    Enemy enemy;
    public Slider slider;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    public void SetBossMaxHealth(int bossHealth)
    {
        slider.maxValue = enemy.maxHealth;
        slider.value = enemy.currentHealth;
    }
    public void SetHealth(int bossHealth)
    {
        slider.value = enemy.currentHealth;
        if (slider.value <= 0)
        {
            Destroy(gameObject);
        }
    }
}
