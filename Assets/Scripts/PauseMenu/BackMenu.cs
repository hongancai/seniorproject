using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMenu : MonoBehaviour
{
    public Button btnBackMainMenu;
    void Start()
    {
        btnBackMainMenu.onClick.AddListener(BackMainMenu);
    }
    void Update()
    {
        
    }
    private void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
