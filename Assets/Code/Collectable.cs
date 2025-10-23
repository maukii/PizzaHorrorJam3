using UnityEngine;

public enum CollectableType
{
    Collectable_1,
    Collectable_2,
    Collectable_3,
    Collectable_4,
}

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableType collectableType;
    [SerializeField] AudioClip collectSound;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float hoverAmplitude = 0.25f;
    [SerializeField] float hoverFrequency = 2f;

    bool _collected = false;
    Vector3 _startPos;


    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        float newY = _startPos.y + Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_collected) return;
        if (!other.CompareTag("Player")) return;

        _collected = true;
        OnCollected(other.gameObject);
    }

    protected virtual void OnCollected(GameObject collector)
    {
        if (collectSound)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        GameEventSystem.RegisterCollectable(collectableType);
        Destroy(gameObject);
    }
}
