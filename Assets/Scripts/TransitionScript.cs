using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadePanel;         // assign in Inspector
    public CanvasGroup canvasGroup; // assign in Inspector
    public float fadeDuration = 1f;

    private static SceneTransition instance;

    void Awake()
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

    public static void FadeToScene(string sceneName)
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.FadeAndSwitchScene(sceneName));
        }
        else
        {
            Debug.LogError("No SceneTransition prefab in scene!");
        }
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        // Fade out (blocks input ON)
        canvasGroup.blocksRaycasts = true;
        yield return StartCoroutine(Fade(1));

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Fade in
        yield return StartCoroutine(Fade(0));
        canvasGroup.blocksRaycasts = false; // unblock input
    }

    private IEnumerator Fade(float targetAlpha)
    {
        Color color = fadePanel.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadePanel.color = color;
            canvasGroup.alpha = color.a;
            yield return null;
        }

        color.a = targetAlpha;
        fadePanel.color = color;
        canvasGroup.alpha = targetAlpha;
    }
}