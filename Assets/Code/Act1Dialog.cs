using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Core;
using UnityEngine;

public class Act1Dialog : MonoBehaviour
{
    [SerializeField] CanvasGroup dialogUI;
    [SerializeField] List<TypewriterCore> dialogLines = new List<TypewriterCore>();
    [SerializeField] List<TypewriterCore> noteDialogLines = new List<TypewriterCore>();


    IEnumerator Start()
    {
        yield return FadeDialogUI(0f, 1f);

        foreach (TypewriterCore line in dialogLines)
        {
            line.gameObject.SetActive(true);
            line.StartShowingText();

            while (!line.TextAnimator.allLettersShown) yield return null;

            line.StopShowingText();

            yield return new WaitForSeconds(1.5f);
            line.gameObject.SetActive(false);
        }

        yield return FadeDialogUI(1f, 0f);
    }

    public void OnNoteFound()
    {
        StopAllCoroutines();
        StartCoroutine(NoteDialog());
    }

    IEnumerator NoteDialog()
    {
        yield return FadeDialogUI(0f, 1f);

        foreach (TypewriterCore line in noteDialogLines)
        {
            line.gameObject.SetActive(true);
            line.StartShowingText();

            while (!line.TextAnimator.allLettersShown) yield return null;

            line.StopShowingText();

            yield return new WaitForSeconds(1.5f);
            line.gameObject.SetActive(false);
        }

        yield return FadeDialogUI(1f, 0f);
    }

    IEnumerator FadeDialogUI(float from, float to)
    {
        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            dialogUI.alpha = alpha;
            yield return null;
        }

        dialogUI.alpha = to;      
    }
}
