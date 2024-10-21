using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

   
    void Update()
    {
        if (GameDB.playerHp <= 0)
        {
            playerDiePnl.SetActive(true);
        }
    }
    
    
}
