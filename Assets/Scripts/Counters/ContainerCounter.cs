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
        if (player.HasKitchenObject())
        {
            // Player is carrying something, do nothing
            return;
        }
        KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
