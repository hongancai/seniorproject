using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    public Text ItemtText;
    public Image ItemImage;

    private bool _isDirty = false;
    private ItemData _data;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (_isDirty)
        {
            ItemtText.text = _data.Name;
            ItemImage.sprite = GameDB.res.itemList[_data.SpriteId];
            _isDirty = false;
        }
    }

    public void SetImage(Sprite sprite)
    {
        ItemImage.sprite = sprite;
    }
    public void SetText(string text)
    {
        ItemtText.text = text;
    }

    public void SetItem(ItemData data)
    {
        _data = data;
        _isDirty = true; //用Update更新
    }
}
