using System.Collections;
using UnityEngine;

public class ChurchShake : MonoBehaviour
{
    [SerializeField] float positionIntensity = 0.3f;
    [SerializeField] float rotationIntensity = 5f;
    [SerializeField] float frequency = 20f;
    [SerializeField] float duration = 2f;

    Vector3 originalPos;
    Quaternion originalRot;
    Coroutine shakeRoutine;


    void Start()
    {
        shakeRoutine = StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;

        float elapsed = 0f;
        float seed = Random.value * 100f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed * frequency;
            float falloff = Mathf.Clamp01(1f - (elapsed / duration));

            float offsetX = (Mathf.PerlinNoise(seed, t) - 0.5f) * 2f * positionIntensity * falloff;
            float offsetY = (Mathf.PerlinNoise(seed + 1f, t) - 0.5f) * 2f * positionIntensity * falloff;
            float offsetZ = (Mathf.PerlinNoise(seed + 2f, t) - 0.5f) * 2f * positionIntensity * falloff;

            float rotX = (Mathf.PerlinNoise(seed + 3f, t) - 0.5f) * 2f * rotationIntensity * falloff;
            float rotY = (Mathf.PerlinNoise(seed + 4f, t) - 0.5f) * 2f * rotationIntensity * falloff;
            float rotZ = (Mathf.PerlinNoise(seed + 5f, t) - 0.5f) * 2f * rotationIntensity * falloff;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, offsetZ);
            transform.localRotation = originalRot * Quaternion.Euler(rotX, rotY, rotZ);

            yield return null;
        }

        transform.localPosition = originalPos;
        transform.localRotation = originalRot;
        shakeRoutine = null;
    }
}
