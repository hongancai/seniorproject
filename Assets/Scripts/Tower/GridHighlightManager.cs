using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridHighlightManager : MonoBehaviour
{
    public Material validPlacementMaterial; // 綠色材質 (可放置)
    
    private List<GameObject> roadObjects = new List<GameObject>();
    private List<Material> originalMaterials = new List<Material>();
    private bool isHighlighting = false;
    
    void Start()
    {
        // 找出所有帶有RoadTag的物體
        RoadTag[] roadTags = FindObjectsOfType<RoadTag>();
        foreach (RoadTag tag in roadTags)
        {
            GameObject roadObj = tag.gameObject;
            roadObjects.Add(roadObj);
            
            // 儲存原始材質，以便之後恢復
            Renderer renderer = roadObj.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterials.Add(renderer.material);
            }
            else
            {
                originalMaterials.Add(null);
            }
        }
    }
    
    // 顯示所有可放置區域的高亮
    public void ShowAllValidAreas()
    {
        if (isHighlighting) return;
        
        isHighlighting = true;
        
        for (int i = 0; i < roadObjects.Count; i++)
        {
            Renderer renderer = roadObjects[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
                renderer.material = validPlacementMaterial;
            }
        }
    }
    
    // 隱藏所有高亮
    public void HideAllHighlights()
    {
        if (!isHighlighting) return;
        
        isHighlighting = false;
        
        for (int i = 0; i < roadObjects.Count; i++)
        {
            Renderer renderer = roadObjects[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }
}