using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpTest : MonoBehaviour
{
    public HpHandler hpHandler;
    public int maxHealth = 3000;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        GameDB.towerHp = currentHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(200);
        }
    }

    public int TakeDamage(int damage)
    {
        int previousHealth = currentHealth;
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        
        UpdateHealthBar();
        return previousHealth - currentHealth;
    }

    private void UpdateHealthBar()
    {
        float hpPercentage = currentHealth;
        Debug.Log(hpPercentage);
        hpHandler.SetHp(hpPercentage);
    }
}
