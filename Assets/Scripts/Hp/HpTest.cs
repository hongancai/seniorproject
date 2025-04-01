using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpTest : MonoBehaviour
{
    public HpHandler hpHandler;
    public int maxHealth = 3000;
    public int currentHealth;
    
    public GameObject losePnl;
    public Button backMenuBtn;
    public Button restartBtn;
    void Start()
    {
        losePnl.SetActive(false);
        backMenuBtn.onClick.AddListener(BackMenuOnClick);
        restartBtn.onClick.AddListener(RestartBtnOnClick);
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
        if (currentHealth <= 0)
        {
            losePnl.SetActive(true);
        }
    }
    private void BackMenuOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void RestartBtnOnClick()
    {
        GameDB.ResetAll();
        SceneManager.LoadScene("S1");
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
