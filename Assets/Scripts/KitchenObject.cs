using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("ClearCounter already has a KitchenObject!");
        }

        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetCounterTopPoint();
        transform.localPosition = Vector3.zero;
    }
    
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
