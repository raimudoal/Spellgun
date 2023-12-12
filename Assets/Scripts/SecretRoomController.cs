using UnityEngine;

public class SecretRoomController : MonoBehaviour
{
    public GameObject coveringLayer;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Ensure the CanvasGroup component is attached to the covering layer
        canvasGroup = coveringLayer.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = coveringLayer.AddComponent<CanvasGroup>();
        }

        // Set the initial alpha to fully visible
        canvasGroup.alpha = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the secret room, start fading out
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, 1f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the secret room, start fading in
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, 1f));
        }
    }

    private System.Collections.IEnumerator FadeCanvasGroup(CanvasGroup group, float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            group.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        group.alpha = end;

        if (end > 0f)
        {
            coveringLayer.SetActive(true);
        }
        else
        {
            coveringLayer.SetActive(false);
        }
    }
}
