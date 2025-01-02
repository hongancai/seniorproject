using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverpnl;
    public Button restartBtn;
    public Button backMainMenuBtn;
   
    void Start()
    {
        gameoverpnl.SetActive(false);
    }

    
    void Update()
    {
        
    }
}
