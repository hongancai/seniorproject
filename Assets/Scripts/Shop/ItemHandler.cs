using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public Image ItemImage;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetImage(Sprite sprite)
    {
        ItemImage.sprite = sprite;
    }
}
