using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1Bgm : MonoBehaviour
{
    public AudioClip s1bgm;
    
    void Start()
    {
        GameDB.Audio.PlayBgm(s1bgm);  
    }

    
    void Update()
    {
        
    }
}
