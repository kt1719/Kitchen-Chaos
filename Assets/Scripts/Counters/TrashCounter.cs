using System;
using UnityEngine;

public class TrashCounter : BaseCounter, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectTrashed;
    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;
        KitchenObject kitchenObject = player.GetKitchenObject();
        kitchenObject.DestroySelf();

        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
}
