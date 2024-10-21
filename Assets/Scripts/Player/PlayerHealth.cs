using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject playerDiePnl;
    public Button rebirthBtn;
    void Start()
    {
        playerDiePnl.SetActive(false);
        GameDB.playerHp = 10;
        UpdateHealthSlider();
        rebirthBtn.onClick.AddListener(OnRebirth);
    }
    
    void Update()
    {
        if (GameDB.playerHp <= 0)
        {
            Die();
        }
    }

    public void UpdateHealthSlider()
    {
        healthSlider.value = GameDB.playerHp;
    }

    public void Die()
    {
        playerDiePnl.SetActive(true);
    }
    
    private void OnRebirth()
    {
        GameDB.playerHp = 10;           // 重置玩家血量
        playerDiePnl.SetActive(false);   // 隱藏死亡面板
        UpdateHealthSlider(); 
        SceneManager.LoadScene("上");
    }
    
}
