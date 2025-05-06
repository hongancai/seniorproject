using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTowerHpHandler : MonoBehaviour
{
    // 血條Quad物件引用
    public GameObject healthBarQuad;

    public GameObject losePnl;


    private float maxHP = 3000f;
    private float currentHP = 3000f;

    private float maintowerDef = 8;


    void Start()
    {
        losePnl.SetActive(false);
        currentHP = maxHP;
        if (healthBarQuad != null)
        {
            UpdateHPBar();
        }
        else
        {
            Debug.LogWarning("未設置血條Quad，請在Inspector中設置healthBarQuad！");
        }
    }


    void Update()
    {
        // 確保主塔位置與旋轉角度固定
        transform.localPosition = new Vector3(500.040009f, 1.06f, 500.790009f);
        transform.eulerAngles = new Vector3(30, 0, 0);
        transform.localScale = new Vector3(1.2f, 2.5f, 1.2f);

        // 測試用，按C鍵減血
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(200);
        }
    }

    public void TakeDamage(float damage)
    {
        // 計算實際傷害，考慮防禦力
        float actualDamage = Mathf.Max(1, damage - maintowerDef);

        currentHP -= actualDamage;
        currentHP = Mathf.Max(0, currentHP); // 確保血量不會小於0

        // 更新血條
        UpdateHPBar();

        // 血量歸零時死亡
        if (currentHP <= 0)
        {
            Die();
        }
    }


    void UpdateHPBar()
    {
        if (healthBarQuad == null) return;

        float hpPercentage = currentHP / maxHP;
        // 在此使用quad的scale來當作血條的變化
        healthBarQuad.transform.localScale = new Vector3(hpPercentage, 1f, 1f);
        // 設定血條的位移，使其從左側減少
        healthBarQuad.transform.localPosition =
            new Vector3((-0.5f + hpPercentage * 0.5f), healthBarQuad.transform.localPosition.y, 0f);
    }

    public void SetHp(float hp)
    {
        currentHP = Mathf.Clamp(hp, 0f, maxHP);
        UpdateHPBar();
    }

    private void Die()
    {
        losePnl.SetActive(true);
        if (losePnl.activeSelf)
        {
            Time.timeScale = 0;
            float currentVolume = GameDB.Audio._bgmAudioSource.volume;
            if (GameDB.Audio._bgmAudioSource != null)
            {
                if (!GameDB.Audio._bgmAudioSource.isPlaying)
                {
                    GameDB.Audio._bgmAudioSource.Play();
                }

                // 保持原有音量，不降低
                GameDB.Audio._bgmAudioSource.volume = currentVolume;
            }
        }
    }
}
