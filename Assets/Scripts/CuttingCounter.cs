using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress; 
    
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
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                // Drop the kitchen object on the counter top
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
            }
            else
            {
                // If the player has no kitchen object, do nothing
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            }
        }
        else
        {
            // There is no kitchen object, do nothing
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
