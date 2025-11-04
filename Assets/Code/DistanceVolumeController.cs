using UnityEngine;

public class DistanceVolumeController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minDistance = 1f;
    [SerializeField] float maxDistance = 100f;

    AudioSource audioSource;


    void Awake() => audioSource = GetComponent<AudioSource>();

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float normalized = Mathf.InverseLerp(maxDistance, minDistance, distance);
        audioSource.volume = normalized;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
#endif
}
