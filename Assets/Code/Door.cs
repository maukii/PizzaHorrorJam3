using System;
using UnityEngine;
using UnityEngine.Playables;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] AudioCrossfade audioCrossfade;
    [SerializeField] PlayableDirector director;
    [SerializeField] HideSequenceManager hideSequenceManager;

    bool firstInteraction = false;
    bool hideSequenceCompleted = false;
    bool secondInteraction = false;
    public bool CanInteract => !firstInteraction || (firstInteraction && hideSequenceCompleted && !secondInteraction);


    void OnEnable() => hideSequenceManager.OnHidePhaseSuccess += OnHideCompleted;

    void OnDisable() => hideSequenceManager.OnHidePhaseSuccess -= OnHideCompleted;

    void OnHideCompleted() => hideSequenceCompleted = true;

    public void Interact(GameObject interactor)
    {
        if (!firstInteraction)
        {
            audioCrossfade.Crossfade();
            director.stopped += OnCutsceneEnd;
            director.Play();
            firstInteraction = true;
            return;
        }

        if (!secondInteraction && hideSequenceCompleted)
        {
            SceceSwitcher.Instance.SwitchScene("Act3");
            secondInteraction = true;
        }
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        director.stopped -= OnCutsceneEnd;
        hideSequenceManager.BeginHidePhase();
    }

    public string GetInteractionPrompt()
    {
        if (!firstInteraction) return "OPEN DOOR";
        
        return "ENTER BASEMENT";
    }
}
