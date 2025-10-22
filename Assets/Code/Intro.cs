using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] AudioSource typeSoundSource;
    [SerializeField] float typeDelayPerLetter = 0.05f;
    [SerializeField] Button fullscreenContinueButton;
    [SerializeField] TextMeshProUGUI sentenceLabel;
    [SerializeField] string[] sentencesBeforeGraphic;
    [SerializeField] CanvasGroup graphicCanvasGroup;
    [SerializeField] float graphicFadeDuration = 1f;
    [SerializeField] string[] sentencesAfterGraphic;
    [SerializeField] string sceneToLoad = "Act1";

    bool continueClicked;


    void Awake()
    {
        sentenceLabel.text = "";
        fullscreenContinueButton.gameObject.SetActive(false);
        fullscreenContinueButton.onClick.AddListener(() => continueClicked = true);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        foreach (string sentence in sentencesBeforeGraphic)
        {
            yield return StartCoroutine(TypeSentence(sentence));
            fullscreenContinueButton.gameObject.SetActive(true);

            yield return new WaitUntil(() => continueClicked);

            continueClicked = false;
            fullscreenContinueButton.gameObject.SetActive(false);
        }

        sentenceLabel.text = "";
        yield return FadeInGraphic();

        foreach (string sentence in sentencesAfterGraphic)
        {
            yield return StartCoroutine(TypeSentence(sentence));
            fullscreenContinueButton.gameObject.SetActive(true);

            yield return new WaitUntil(() => continueClicked);

            continueClicked = false;
            fullscreenContinueButton.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceLabel.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceLabel.text += letter;
            typeSoundSource.Play();
            yield return new WaitForSeconds(typeDelayPerLetter);
        }
    }

    IEnumerator FadeInGraphic()
    {
        float elapsed = 0f;
        while (elapsed < graphicFadeDuration)
        {
            elapsed += Time.deltaTime;
            graphicCanvasGroup.alpha = Mathf.Clamp01(elapsed / graphicFadeDuration);
            yield return null;
        }
        graphicCanvasGroup.alpha = 1f;
    }
}
