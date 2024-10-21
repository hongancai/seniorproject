using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TestShopMgr : MonoBehaviour
{
    public GameObject ItemPrefab;

    public GameObject ContentPool;

    private IList<ItemData> list;

    void Awake()
    {
        string json = PlayerPrefs.GetString("data");
    }

    public IList<ItemData> GetData()
    {
        //fromDB
        list = new List<ItemData>();
        IList<ItemData> result = new List<ItemData>();
        //result.Add(new ItemData() { Id = 0, Name = "高粱酒", SpriteId = 0 });
        //result.Add(new ItemData() {Id = 1, Name = "閩式燒餅" , SpriteId = 1 });
        //result.Add(new ItemData() {Id = 2, Name = "貢糖" , SpriteId = 2 });
        //result.Add(new ItemData() {Id = 3, Name = "牛肉乾" , SpriteId = 3 });
        //result.Add(new ItemData() {Id = 4, Name = "金門麵線" , SpriteId = 4 });
        

        return result;
    }

    void Start()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = Instantiate(ItemPrefab);
            temp.transform.parent = ContentPool.transform;
            temp.transform.localScale = Vector3.one;

            ItemHandler handler = temp.GetComponent<ItemHandler>();
            //int _rndIndex = Random.Range(0,GameDB.res.itemList.Count);
            // handler.SetImage(GameDB.res.itemList[_rndIndex]);
            handler.SetItem(list[i]);
        }
    }

    void Update()
    {
        
    }
}