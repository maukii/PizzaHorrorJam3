using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class HideSequenceManager : MonoBehaviour
{
    public event System.Action OnHidePhaseSuccess;

    [SerializeField] PlayableDirector successCutscene;
    [SerializeField] PlayableDirector failCutscene;
    [SerializeField] float hideTimeWindow = 5f;
    
    bool playerHid = false;
    bool hidePhaseActive = false;
    HidingPlace activeHidingPlace;
    
    public bool IsInCutscene()
    {
        return successCutscene.state == PlayState.Playing || failCutscene.state == PlayState.Playing;
    }

    public void BeginHidePhase()
    {
        hidePhaseActive = true;
        playerHid = false;

        StartCoroutine(HideCountdown());
    }

    IEnumerator HideCountdown()
    {
        float timer = hideTimeWindow;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (playerHid) yield break;

            yield return null;
        }

        OnPlayerCaught();
    }

    public void PlayerEnteredCloset(HidingPlace hidingPlace)
    {
        if (!hidePhaseActive) return;
        
        playerHid = true;
        activeHidingPlace = hidingPlace;

        successCutscene.stopped += OnSuccessCutsceneEnd;
        successCutscene.Play();

        hidePhaseActive = false;
    }

    void OnSuccessCutsceneEnd(PlayableDirector director)
    {
        director.stopped -= OnSuccessCutsceneEnd;
        if (activeHidingPlace != null)
        {
            activeHidingPlace.RevealPlayer();
            activeHidingPlace = null;
        }
        OnHidePhaseSuccess?.Invoke();
    }

    void OnPlayerCaught()
    {
        hidePhaseActive = false;
        failCutscene.Play();
    }
}
