using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        PlayerSounds.ResetStaticData();
        GameStartCountDownUI.ResetStaticData();
    }
}
