using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeSOListMax = 4;

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
            if (waitingRecipeSOList.Count < waitingRecipeSOListMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
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
                    return;
                }
                else
                {
                    Debug.Log("Player did not");
                }
            }
        }
        Debug.Log("No recipes Match!");
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
}
