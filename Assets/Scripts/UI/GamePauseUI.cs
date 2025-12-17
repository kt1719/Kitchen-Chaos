using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManger.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        KitchenGameManger.Instance.OnGamePaused += KitchenGameManger_OnGamePaused;
        KitchenGameManger.Instance.OnGameUnpaused += KitchenGameManger_OnGameUnpaused;

        Hide();
    }

    private void KitchenGameManger_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void KitchenGameManger_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
