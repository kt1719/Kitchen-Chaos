using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct  KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects;
    [SerializeField] private PlateCompleteVisualObjectHelper plateCompleteVisualObjectHelper;

    private List<KitchenObjectSO_GameObject> currentActiveIngredients;

    private void Awake()
    {
        currentActiveIngredients = new List<KitchenObjectSO_GameObject>();
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }

        plateCompleteVisualObjectHelper.RecordKitchenObjectBaseHeights(kitchenObjectSO_GameObjects);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.PlateIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
                currentActiveIngredients.Add(kitchenObjectSO_GameObject);
            }
        }

        foreach (KitchenObjectSO_GameObject ingredient in currentActiveIngredients)
        {
            float height = plateCompleteVisualObjectHelper.CalculateHeight(ingredient.kitchenObjectSO, currentActiveIngredients.ConvertAll(i => i.kitchenObjectSO));
            ingredient.gameObject.transform.localPosition = 
            new Vector3(
                ingredient.gameObject.transform.localPosition.x, 
                height, 
                ingredient.gameObject.transform.localPosition.z
            );
        }
    }
}
