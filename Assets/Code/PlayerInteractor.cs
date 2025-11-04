using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float interactDistance = 2.5f;
    [SerializeField] LayerMask interactableMask = ~0;
    [SerializeField] CinemachineCamera playerCamera;
    [SerializeField] InteractUI interactUI;

    IInteractable currentTarget;


    void Update()
    {
        DetectInteractable();
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) TryInteract();
    }

    void DetectInteractable()
    {
        currentTarget = null;
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.CanInteract)
                {
                    currentTarget = interactable;
                    interactUI.gameObject.SetActive(true);
                    interactUI.SetPrompt(interactable.GetInteractionPrompt());
                    return;
                }
            }
        }

        interactUI.gameObject.SetActive(false);
    }

    public void HideInteractUI()
    {
        interactUI.gameObject.SetActive(false);
    }

    void TryInteract()
    {
        if (currentTarget == null) return;

        currentTarget.Interact(gameObject);
    }
}
