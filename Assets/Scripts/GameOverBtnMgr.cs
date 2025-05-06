using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBtnMgr : MonoBehaviour
{
    public Button backMenuBtn;
    public Button restartBtn;
    void Start()
    {
        backMenuBtn.onClick.AddListener(BackMenuOnClick);
        restartBtn.onClick.AddListener(RestartBtnOnClick);
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
}
