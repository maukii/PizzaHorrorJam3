using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class EndingSelector : MonoBehaviour
{
    [SerializeField] AudioSource currentSource;
    [SerializeField] PlayableDirector triggerAfterDirector;
    [SerializeField] PlayableDirector goodEndingDirector;
    [SerializeField] PlayableDirector badEndingDirector;
    [SerializeField] AudioSource goodEndingAudioSource;
    [SerializeField] AudioSource badEndingAudioSource;


    void OnEnable() => triggerAfterDirector.stopped += OnTriggerAfterDirectorStopped;

    void OnDisable() => triggerAfterDirector.stopped -= OnTriggerAfterDirectorStopped;

    void OnTriggerAfterDirectorStopped(PlayableDirector director)
    {
        if (GameEventSystem.GetCollectedCount() >= Enum.GetValues(typeof(CollectableType)).Length)
        {
            goodEndingDirector.Play();
            StartCoroutine(CrossfadeRoutine(goodEndingAudioSource, 2f));
        }
        else
        {
            badEndingDirector.Play();
            StartCoroutine(CrossfadeRoutine(badEndingAudioSource, 2f));
        }
    }

    IEnumerator CrossfadeRoutine(AudioSource endingAudioSource, float duration)
    {
        endingAudioSource.volume = 0f;
        endingAudioSource.Play();

        float time = 0f;
        float startVol = currentSource.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            currentSource.volume = Mathf.Lerp(startVol, 0f, t);
            endingAudioSource.volume = Mathf.Lerp(0f, startVol, t);

            yield return null;
        }

        currentSource.Stop();
    }
}
