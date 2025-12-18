using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeSOListMax = 4;
    private int successFulRecipesAmount = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Start()
    {
        StartCoroutine(UpdateWaitingRecipeList());
    }

    private IEnumerator UpdateWaitingRecipeList()
    {
        while (true)
        {
            if (KitchenGameManger.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeSOListMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
            yield return new WaitForSeconds(spawnRecipeTimerMax);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSOList)
        {
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) // Optimization check
            {
                // Has the same number of ingredients
                if (RecipeSOContainsAllIngredients(waitingRecipeSO, plateKitchenObject.GetKitchenObjectSOList()))
                {
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    successFulRecipesAmount++;
                    return;
                }
                else
                {
                    Debug.Log("Player did not");
                }
            }
        }
        Debug.Log("No recipes Match!");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    private bool RecipeSOContainsAllIngredients(RecipeSO recipeSO, List<KitchenObjectSO> kitchenObjectSOList)
    {
        foreach (KitchenObjectSO kitchenObjectSO in kitchenObjectSOList)
        {
            if (!recipeSO.kitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return  false;
            }
        }
        return true;
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successFulRecipesAmount;
    }
}
