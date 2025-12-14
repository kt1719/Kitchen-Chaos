using System;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                // Player is carrying plate kitchen object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject) 
                    && plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                }
                else
                {
                    // Player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject counterPlateKitchenObject) 
                        && counterPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        } 
        else
        {
            // There is no kitchen object
            if (player.HasKitchenObject())
            {
                // Drop the kitchen object on the counter top
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // If the player has no kitchen object, do nothing
            }
        }
    }
}
