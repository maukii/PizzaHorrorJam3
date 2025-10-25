using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float interactDistance = 2.5f;
    [SerializeField] LayerMask interactableMask = ~0;
    [SerializeField] CinemachineCamera playerCamera;
    [SerializeField] GameObject interactUI;

    IInteractable currentTarget;


    void Update()
    {
        DetectInteractable();
        if (Input.GetKeyDown(KeyCode.E)) TryInteract();
    }

    void DetectInteractable()
    {
        currentTarget = null;
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                currentTarget = interactable;
                interactUI.SetActive(true);
                Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green);
                return;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
        interactUI.SetActive(false);
    }

    public void HideInteractUI()
    {
        interactUI.SetActive(false);
    }

    void TryInteract()
    {
        if (currentTarget == null) return;

        currentTarget.Interact(gameObject);
    }
}
