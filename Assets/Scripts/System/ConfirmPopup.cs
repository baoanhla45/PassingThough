using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmPopup : MonoBehaviour
{
    public Text messageText;
    public Button yesButton;
    public Button noButton;

    private static ConfirmPopup instance;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public static void Show(string message, Action onConfirm)
    {
        instance.gameObject.SetActive(true);
        instance.messageText.text = message;

        instance.yesButton.onClick.RemoveAllListeners();
        instance.yesButton.onClick.AddListener(() => {
            onConfirm?.Invoke();
            instance.gameObject.SetActive(false);
        });

        instance.noButton.onClick.RemoveAllListeners();
        instance.noButton.onClick.AddListener(() => {
            instance.gameObject.SetActive(false);
        });
    }
}
