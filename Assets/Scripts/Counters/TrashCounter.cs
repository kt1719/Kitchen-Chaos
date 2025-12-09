using UnityEngine;

public class TrashCounter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;
        KitchenObject kitchenObject = player.GetKitchenObject();
        kitchenObject.DestroySelf();
    }
}
