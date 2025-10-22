using UnityEngine;

public class SoundOnCharacter : MonoBehaviour
{
    [SerializeField] AudioSource source;


    void Awake() => GetComponent<Febucci.UI.Core.TypewriterCore>()?.onCharacterVisible.AddListener(OnCharacter);

    void OnCharacter(char character) => source.Play();
}
