using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countDownText;

    public static event EventHandler OnCountdownNumberChange;
    public static void ResetStaticData()
    {
        OnCountdownNumberChange = null;
    }

    private Animator animator;
    private int previousCountDownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        int counterDownNumber = Mathf.CeilToInt(KitchenGameManger.Instance.GetCountDownToStartTimer());
        countDownText.text = counterDownNumber.ToString();

        if (previousCountDownNumber != counterDownNumber)
        {
            previousCountDownNumber = counterDownNumber;
            animator.SetTrigger(NUMBER_POPUP);

            OnCountdownNumberChange?.Invoke(this, EventArgs.Empty);
        }
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
