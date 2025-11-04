using System.Collections;
using UnityEngine;

public class AudioCrossfade : MonoBehaviour
{
    [SerializeField] AudioSource sourceA;
    [SerializeField] AudioSource sourceB;
    [SerializeField] float defaultFadeDuration = 1.5f;

    AudioSource currentSource;
    AudioSource nextSource;
    Coroutine fadeCoroutine;


    void Awake()
    {
        currentSource = sourceA;
        nextSource = sourceB;
    }

    public void Crossfade()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(CrossfadeRoutine(defaultFadeDuration));
    }

    IEnumerator CrossfadeRoutine(float duration)
    {
        nextSource.volume = 0f;
        nextSource.Play();

        float time = 0f;
        float startVol = currentSource.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            currentSource.volume = Mathf.Lerp(startVol, 0f, t);
            nextSource.volume = Mathf.Lerp(0f, startVol, t);

            yield return null;
        }

        currentSource.Stop();
        nextSource.volume = startVol;

        (currentSource, nextSource) = (nextSource, currentSource);
    }
}
