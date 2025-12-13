using System;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            // There is a kitchen object
            if (!player.HasKitchenObject())
            {
                // Player is not carrying anything, give them the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                // Player is carrying something, do nothing
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
