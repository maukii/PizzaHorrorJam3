using UnityEngine;

public class EnterChurchTrigger : MonoBehaviour
{
    [SerializeField] AudioClip enterChurchSfx;
    [SerializeField] string sceneToLoad = "Act2";


    void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(enterChurchSfx, transform.position);
        SceceSwitcher.Instance.SwitchScene(sceneToLoad);
    }
}
