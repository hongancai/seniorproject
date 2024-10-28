using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueBtn : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button conBtn;
    void Start()
    {
        conBtn.onClick.AddListener(ConBtn);
    }
    
    void Update()
    {
        
    }
    private void ConBtn()
    {
        pauseMenu.SetActive(false);
        Time.timeScale =  1f;
    }
}
