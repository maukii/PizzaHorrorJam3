using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EndingHandler : MonoBehaviour
{
    [SerializeField] CinemachineCamera targetCamera;
    [SerializeField] float shakeAmplitude = 2.0f;
    [SerializeField] float shakeFrequency = 2.0f;
    [SerializeField] Volume postProcessVolume;
    [SerializeField] float blurIntensity = 0.6f;
    [SerializeField] float blurFadeSpeed = 1.5f;

    CinemachineBasicMultiChannelPerlin perlin;
    DepthOfField dof;


    void Awake()
    {
        perlin = targetCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        postProcessVolume.profile.TryGet(out dof);
    }

    public void ShakeCamera()
    {
        perlin.AmplitudeGain = shakeAmplitude;
        perlin.FrequencyGain = shakeFrequency;

        StopAllCoroutines();
        StartCoroutine(EnableBlur(true));
    }

    IEnumerator EnableBlur(bool enable)
    {
        float targetAperture = enable ? blurIntensity : 0f;
        float start = 0f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * blurFadeSpeed;
            dof.gaussianEnd.value = Mathf.Lerp(start, targetAperture, t);
            yield return null;
        }
    }

    public void ShowBadEnding()
    {
        SceceSwitcher.Instance.SwitchScene("BadEndingScene");
    }

    public void ShowGoodEnding()
    {
        Debug.Log("Switching to good ending scene");
        SceceSwitcher.Instance.SwitchScene("GoodEndingScene", true);
    }
}
