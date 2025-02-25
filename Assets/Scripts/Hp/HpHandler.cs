using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHandler : MonoBehaviour
{  public float maxHP = 100f;
    public float currentHP = 100f;
    void Start()
    {
    }

    void Update()
    {
        UpdateHPBar();
    }
    void UpdateHPBar()
    {
        float hpPercentage = currentHP / maxHP;
        // 在此使用 quad 的 scale 來當作血條的變化
        transform.localScale = new Vector3(hpPercentage, 1f, 1f);
        // 設定血條的位移
        transform.localPosition = new Vector3((-0.5f + hpPercentage * 0.5f), transform.localPosition.y, 0f);
    }
    public void SetHp(float hpPercentage)
    {
        currentHP = Mathf.Clamp(hpPercentage, 0f, maxHP);
    }
}
