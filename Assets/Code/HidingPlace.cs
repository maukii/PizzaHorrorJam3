using Unity.Cinemachine;
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject interactor);
}

public class HidingPlace : MonoBehaviour, IInteractable
{
    [SerializeField] CinemachineCamera hidingCamera;

    bool isPlayerHidden = false;


    public void Interact(GameObject interactor)
    {
        if (!isPlayerHidden)
        {
            HidePlayer(interactor);
        }
        else
        {
            RevealPlayer(interactor);
        }
    }

    void RevealPlayer(GameObject interactor)
    {
        if (interactor.TryGetComponent(out PlayerController playerController))
            playerController.SetHiding(false);

        hidingCamera.gameObject.SetActive(false);
        isPlayerHidden = false;
    }

    void HidePlayer(GameObject interactor)
    {
        if (interactor.TryGetComponent(out PlayerController playerController))
            playerController.SetHiding(true);

        hidingCamera.gameObject.SetActive(true);
        isPlayerHidden = true;
    }
}
