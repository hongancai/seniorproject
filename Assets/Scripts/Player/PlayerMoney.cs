using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
   
    void Start()
    {
        GameDB.Load();
        UpdateMoneyDisplay(); 
    }

    
    void Update()
    {
        UpdateMoneyDisplay();
    }
    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = $"${GameDB.money}";  // 可以根據需求修改顯示格式
        }
    }
}
