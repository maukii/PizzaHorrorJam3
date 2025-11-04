using UnityEngine;
using UnityEngine.Playables;

public class EndingCutsceneTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    
    bool playing = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playing)
        {
            playing = true;
            director.Play();
            Destroy(gameObject);
        }
    }
}
