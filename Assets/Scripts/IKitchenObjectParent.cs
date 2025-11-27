using UnityEngine;

public interface IKitchenObjectParent
{

    public Transform GetKitchenObjectPoint();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
