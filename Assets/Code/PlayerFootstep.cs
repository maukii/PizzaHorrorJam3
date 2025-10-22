using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FootstepSound
{
    public string surfaceTag;
    public AudioClip clip;
}

public class PlayerFootstep : MonoBehaviour
{
    [SerializeField] List<FootstepSound> footstepSounds = new List<FootstepSound>();

    AudioSource footstepAudioSource;


    void Awake() => footstepAudioSource = GetComponent<AudioSource>();

    public void PlayStepSound()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, out RaycastHit hit, 2f)) return;
        
        string surfaceTag = hit.collider.tag;
        FootstepSound? footstepSound = footstepSounds.Find(fs => fs.surfaceTag == surfaceTag);
        if (footstepSound == null) return;

        // TODO::
        // Random volume/pitch
        footstepAudioSource.PlayOneShot(footstepSound.Value.clip);
    }
}
