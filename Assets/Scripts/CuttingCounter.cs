using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player player)
    {
        
        if (HasKitchenObject())
        {
            // There is a kitchen object
            Debug.Log("Counter has kitchen object");
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            // There is a kitchen object
            Debug.Log("Cutting Counter has kitchen object");
            GetKitchenObject().DestroySelf();
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
        else
        {
            // There is no kitchen object, do nothing
        }
    }
}
