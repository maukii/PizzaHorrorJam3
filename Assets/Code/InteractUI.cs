using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactPromptLabel;


    public void SetPrompt(string prompt) => interactPromptLabel.text = prompt;
}
