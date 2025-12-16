using System;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManger : MonoBehaviour
{
    public static KitchenGameManger Instance { get; private set; }

    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                WaitingToStartHandler();
                break;
            case State.CountDownToStart:
                CountDownToStartHandler();
                break;
            case State.GamePlaying:
                GamePlayingHandler();
                break;
            case State.GameOver:
                break;
        }
    }

    private void WaitingToStartHandler()
    {
        TimeHandler(ref waitingToStartTimer);
    }
    private void CountDownToStartHandler()
    {
        TimeHandler(ref countDownToStartTimer);
    }
    private void GamePlayingHandler()
    {
        TimeHandler(ref gamePlayingTimer);
    }

    private void TimeHandler(ref float timer)
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            state += 1; // Update to next state
            if (state == State.GamePlaying)
            {
                gamePlayingTimer = gamePlayingTimerMax;
            }
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
        return;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingtimerNormalized()
    {
        return 1f - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
