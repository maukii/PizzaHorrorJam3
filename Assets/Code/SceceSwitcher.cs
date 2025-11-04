using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceceSwitcher : MonoBehaviour
{
    public static SceceSwitcher Instance { get; private set; }

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 1f;

    bool isSwitching = false;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void SwitchScene(string sceneName)
    {
        SwitchScene(sceneName, false);
    }

    public void SwitchScene(string sceneName, bool useWhiteFade = false)
    {
        fadeImage.color = useWhiteFade ? Color.white : Color.black;

        if (!isSwitching)
            StartCoroutine(SwitchSceneRoutine(sceneName));
    }

    IEnumerator SwitchSceneRoutine(string sceneName)
    {
        isSwitching = true;
        canvasGroup.blocksRaycasts = true;

        yield return Fade(1f);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone)
            yield return null;

        yield return Fade(0f);

        canvasGroup.blocksRaycasts = false;
        isSwitching = false;
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}