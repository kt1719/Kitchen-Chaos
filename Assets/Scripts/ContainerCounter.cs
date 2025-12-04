using System;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        GameObject instantiatedKitchenObject = Instantiate(kitchenObjectSO.prefab);
        instantiatedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
