using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHp : MonoBehaviour
{
    public Slider healthSlider;
    
    void Start()
    {
        GameDB.playerHp = 100;
        UpdateHealthSlider();
    }

    public void UpdateHealthSlider()
    {
        healthSlider.value = GameDB.playerHp;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameDB.playerHp = (int) healthSlider.value - 5;
            UpdateHealthSlider();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameDB.playerHp = (int)(healthSlider.value + 5);
            UpdateHealthSlider();
        }
    }
}
