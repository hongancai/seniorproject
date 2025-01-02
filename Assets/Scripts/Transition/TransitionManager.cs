using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    public CanvasGroup transitionCanvasGroup; // UI 的 CanvasGroup 控制透明度
    public float transitionDuration = 2f;    // 淡入淡出的持續時間

    private static TransitionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadSceneWithTransition(string targetScene, string fromScene = "")
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.Transition(targetScene, fromScene));
        }
    }

    private IEnumerator Transition(string targetScene, string fromScene)
    {
        // 淡出
        transitionCanvasGroup.DOFade(1f, transitionDuration).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(transitionDuration);

        // 更新玩家位置
        if (targetScene == "S1" && !string.IsNullOrEmpty(fromScene))
        {
            GameDB.UpdatePlayerPosition(fromScene);
        }

        // 加載目標場景
        SceneManager.LoadScene(targetScene);

        // 等待場景加載完成
        yield return null;

        // 設置玩家位置
        if (targetScene == "S1")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = GameDB.playerPosition;
            }
        }

        // 淡入
        transitionCanvasGroup.DOFade(0f, transitionDuration).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(transitionDuration);
    }
}
