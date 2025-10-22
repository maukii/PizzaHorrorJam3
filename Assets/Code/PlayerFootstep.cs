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
        if (!Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            Debug.Log("No hit");
            return;
        }
        string surfaceTag = hit.collider.tag;
        Debug.Log("Hit tag: " + surfaceTag);
        FootstepSound? footstepSound = footstepSounds.Find(fs => fs.surfaceTag == surfaceTag);
        if (footstepSound == null) 
        {
            Debug.Log("No sound");
            return;
        }

        footstepAudioSource.PlayOneShot(footstepSound.Value.clip);
    }
}
