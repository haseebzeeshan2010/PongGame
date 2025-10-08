using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskAnimation : MonoBehaviour
{
    [SerializeField] public float duration = 0.5f;
    [SerializeField] public float targetHeight = 6.4f;
    [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private RectTransform m_RectTransform;
    private CanvasGroup m_CanvasGroup;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_CanvasGroup = GetComponent<CanvasGroup>();

        InputStage.TaskComplete += PlayTaskAnimation;
    }

    private void OnDestroy()
    {
        InputStage.TaskComplete -= PlayTaskAnimation;
    }

    //FOR DEBUGGING PURPOSES
    private void OnEnable()
    {
        // PlayTaskAnimation();
    }

    void Update()
    {
    }

    public void PlayTaskAnimation()
    {
        Debug.Log($"Task Animation Played: {m_RectTransform.anchoredPosition.y}");
        StartCoroutine(PlayTaskAnimationCoroutine());
    }

    IEnumerator PlayTaskAnimationCoroutine()
    {
        Vector2 startPos = m_RectTransform.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, 8.6f);
        float elapsed = 0f;
        float initialAlpha = m_CanvasGroup.alpha;
        while (elapsed < duration)
        {
            
            float t = elapsed / duration;
            // Use the curve to adjust the interpolation
            float curveValue = movementCurve.Evaluate(t);
            m_RectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, curveValue);
            elapsed += Time.fixedDeltaTime;

            if (endPos.y > targetHeight+0.01)
            {
                
                m_CanvasGroup.alpha = Mathf.Lerp(initialAlpha, 0f, t);
            }
            else
            {
                // Use a predefined fade threshold (set it to the movement distance where you want alpha to reach 0)
                float fadeThreshold = 9.45f; // Adjust as needed
                float distanceFromTarget = Mathf.Abs(m_RectTransform.anchoredPosition.y - targetHeight);
                m_CanvasGroup.alpha = Mathf.Clamp01(1f - (distanceFromTarget / fadeThreshold));
            }

            yield return null;
        }
        m_RectTransform.anchoredPosition = endPos;
        yield return null;
    }
}
