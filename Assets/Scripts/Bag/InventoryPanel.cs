using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public Button expandButton; // 朝右箭頭按鈕
    public Button collapseButton; // 朝左箭頭按鈕

    public float expandedPosX = 55f;
    public float collapsedPosX = -58f;
    public float animationDuration = 0.5f;
    public Ease animationEase = Ease.OutQuad;
    
    public bool startExpanded = true;
    private bool isExpanded;

    private void Awake()
    {
        // 根據設定決定初始狀態
        isExpanded = startExpanded;
        
        // 設置初始位置
        Vector3 initialPosition = panelRectTransform.anchoredPosition;
        initialPosition.x = isExpanded ? expandedPosX : collapsedPosX;
        panelRectTransform.anchoredPosition = initialPosition;
        
        // 設置按鈕事件監聽
        expandButton.onClick.AddListener(ExpandPanel);
        collapseButton.onClick.AddListener(CollapsePanel);
        
        // 初始時設置按鈕啟用狀態
        UpdateButtonsState();
    }
    
    public void ExpandPanel()
    {
        if (isExpanded) return;
        
        // 使用DoTween移動面板到展開位置
        panelRectTransform.DOAnchorPosX(expandedPosX, animationDuration)
            .SetEase(animationEase)
            .OnComplete(() => {
                isExpanded = true;
                UpdateButtonsState();
            });
    }
    
    public void CollapsePanel()
    {
        if (!isExpanded) return;
        
        // 使用DoTween移動面板到收縮位置
        panelRectTransform.DOAnchorPosX(collapsedPosX, animationDuration)
            .SetEase(animationEase)
            .OnComplete(() => {
                isExpanded = false;
                UpdateButtonsState();
            });
    }
    
    private void UpdateButtonsState()
    {
        // 根據當前狀態啟用/禁用按鈕
        expandButton.gameObject.SetActive(!isExpanded);
        collapseButton.gameObject.SetActive(isExpanded);
    }
    
    private void OnDestroy()
    {
        // 清除事件監聽，避免記憶體洩漏
        expandButton.onClick.RemoveListener(ExpandPanel);
        collapseButton.onClick.RemoveListener(CollapsePanel);
        
        // 確保DoTween動畫被正確終止
        panelRectTransform.DOKill();
    }
}