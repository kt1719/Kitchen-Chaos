using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    // For testing
    [SerializeField] private bool isTesting;
    [SerializeField] private GameObject secondClearCounterObject;
    public void Update()
    {
        if (isTesting && Input.GetKeyDown(KeyCode.T))
        {
            kitchenObject.SetClearCounter(secondClearCounterObject.GetComponent<ClearCounter>());
        }
    }
    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject instantiatedKitchenObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            instantiatedKitchenObject.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log("Counter already has a kitchen object. " + kitchenObjectSO.name);
        }
    }

    public Transform GetCounterTopPoint()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
