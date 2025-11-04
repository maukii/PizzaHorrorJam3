using Unity.Cinemachine;
using UnityEngine;

public interface IInteractable
{
    bool CanInteract { get; }
    void Interact(GameObject interactor);
    string GetInteractionPrompt();
}

public class HidingPlace : MonoBehaviour, IInteractable
{
    [SerializeField] AudioClip hidingSfx;
    [SerializeField] CinemachineCamera hidingCamera;
    [SerializeField] HideSequenceManager hideSequenceManager;
    [SerializeField] PlayerController playerController;

    bool isPlayerHidden = false;
    public bool CanInteract => true;
    

    public void Interact(GameObject interactor)
    {
        if (!isPlayerHidden)
        {
            HidePlayer(interactor);
        }
    }

    void Update()
    {
        if (isPlayerHidden && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && !hideSequenceManager.IsInCutscene())
        {
            RevealPlayer();
        }
    }

    public void RevealPlayer()
    {
        playerController.SetHiding(false);
        hidingCamera.gameObject.SetActive(false);
        isPlayerHidden = false;
        AudioSource.PlayClipAtPoint(hidingSfx, transform.position);
    }

    void HidePlayer(GameObject interactor)
    {
        playerController.SetHiding(true);
        hidingCamera.gameObject.SetActive(true);
        isPlayerHidden = true;
        hideSequenceManager.PlayerEnteredCloset(this);
        AudioSource.PlayClipAtPoint(hidingSfx, transform.position);
    }

    public string GetInteractionPrompt() => "HIDE";
}
