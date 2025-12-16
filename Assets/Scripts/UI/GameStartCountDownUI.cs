using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        KitchenGameManger.Instance.OnStateChanged += KitchenGameManger_OnStateChanged;
        
        Hide();
    }

    private void KitchenGameManger_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManger.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(KitchenGameManger.Instance.GetCountDownToStartTimer()).ToString();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
