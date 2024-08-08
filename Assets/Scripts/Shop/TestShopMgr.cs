using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShopMgr : MonoBehaviour
{
    public GameObject ItemPrefab;
    
    public GameObject ContentPool;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(ItemPrefab);
            temp.transform.parent = ContentPool.transform;
            //temp.transform.localPosition = Vector3.zero;
            temp.transform.localEulerAngles = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            ItemHandler handler = temp.GetComponent<ItemHandler>();
            int _rndIndex = Random.Range(0,GameDB.res.itemList.Count);
            handler.SetImage(GameDB.res.itemList[_rndIndex]);

            int temp_I = i;
            temp.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    OnItemClick(temp_I);
                }
                );
        }
    }
    
    void Update()
    {
        
    }

    public void OnItemClick(int itemId)
    {
        Debug.Log(itemId);
    }
}
