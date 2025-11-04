using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void OnTryAgainClicked()
    {
        SceceSwitcher.Instance.SwitchScene("Act2");
    }
}
