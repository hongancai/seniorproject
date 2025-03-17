using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveWarningSystem : MonoBehaviour
{
    //public AudioClip warningsfx;
    public CanvasGroup warningImageCanvasGroup;
    public Image warningImage;
    
    public float fadeDuration = 0.5f;    
    public float totalDuration = 3f;      
    public float maxAlpha = 1f;           
    public float minAlpha = 0f;  
   
    private void Awake()
    {
        if (warningImageCanvasGroup != null)
        {
            warningImageCanvasGroup.alpha = 0f;
            warningImageCanvasGroup.gameObject.SetActive(false);
        }
    }

    public void ShowWarning()
    {
        StartCoroutine(PlayWarningAnimation());
    }

    private IEnumerator PlayWarningAnimation()
    {
        if (warningImageCanvasGroup == null) yield break;

        warningImageCanvasGroup.gameObject.SetActive(true);
        //GameDB.Audio.PlaySfx(warningsfx);
        float elapsedTime = 0f;
        bool isFadingIn = true;

        while (elapsedTime < totalDuration)
        {
            float targetAlpha = isFadingIn ? maxAlpha : minAlpha;
            
            yield return warningImageCanvasGroup
                .DOFade(targetAlpha, fadeDuration)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion();

            isFadingIn = !isFadingIn;
            elapsedTime += fadeDuration;
        }

        warningImageCanvasGroup.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        warningImageCanvasGroup.gameObject.SetActive(false);
    }

    public void StopWarning()
    {
        StopAllCoroutines();
        if (warningImageCanvasGroup != null)
        {
            warningImageCanvasGroup.DOKill();
            warningImageCanvasGroup.alpha = 0f;
            warningImageCanvasGroup.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (warningImageCanvasGroup != null)
        {
            warningImageCanvasGroup.DOKill();
        }
    }
}

