using System;
using UnityEditor.SettingsManagement;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static event EventHandler OnPlayFootstepFX;
    [SerializeField] private Player player;

    private float footSteptimer;
    private float footStepTimerMax = .1f;

    private void Update()
    {
        footSteptimer -= Time.deltaTime;

        if (footSteptimer < 0f)
        {
            footSteptimer = footStepTimerMax;
            if (player.IsWalking())
            {
                OnPlayFootstepFX?.Invoke(this, EventArgs.Empty);   
            }
        }
    }
}
