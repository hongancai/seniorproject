using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
     [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private Button expandButton; // 朝右箭頭按鈕
    [SerializeField] private Button collapseButton; // 朝左箭頭按鈕
    
    [SerializeField] private float expandedPosX = 55f;
    [SerializeField] private float collapsedPosX = -55f;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Ease animationEase = Ease.OutQuad;
    
    private bool isExpanded = false;
    
    private void Awake()
    {
        // 確保一開始面板是收縮狀態
        Vector3 initialPosition = panelRectTransform.anchoredPosition;
        initialPosition.x = collapsedPosX;
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
