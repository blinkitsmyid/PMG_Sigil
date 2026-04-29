using UnityEngine;
using UnityEngine.UI;

public class ClickZone : MonoBehaviour
{
    public int numberValue; // 1-5

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameController.Instance.OnNumberPressed(numberValue);
    }
}