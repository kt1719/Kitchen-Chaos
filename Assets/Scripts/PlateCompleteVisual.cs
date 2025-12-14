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
            }
        }

        List<KitchenObjectSO> kitchenObjectSOlist = plateKitchenObject.GetKitchenObjectSOList();
        foreach (KitchenObjectSO kitchenObjectSO in kitchenObjectSOlist)
        {
            // Calculate the height of the visual object
            float height = plateCompleteVisualObjectHelper.CalculateHeight(kitchenObjectSO, kitchenObjectSOlist);
            
            // Get the gameObject reference
            GameObject kitchenObjectSOGameObject = getKitchenObjectGameObjectReference(kitchenObjectSO);
            kitchenObjectSOGameObject.gameObject.transform.localPosition = new Vector3(
                kitchenObjectSOGameObject.gameObject.transform.localPosition.x, 
                height, 
                kitchenObjectSOGameObject.gameObject.transform.localPosition.z
            );
        }
    }

    private GameObject getKitchenObjectGameObjectReference(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == kitchenObjectSO)
                return kitchenObjectSO_GameObject.gameObject; 
        }
        Debug.LogError("KitchenObjectSO GameObject reference not found for: " + kitchenObjectSO.name);
        return null;
    }
}
