using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisualObjectHelper : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_Height
    {
        public KitchenObjectSO kitchenObjectSO;
        public float height;
    }

    [Serializable]
    public struct KitchenObjectSO_Rank
    {
        public KitchenObjectSO kitchenObjectSO;
        public int rank;
    }

    private Dictionary<KitchenObjectSO, float> kitchenObjectSO_HeightDict;
    private Dictionary<KitchenObjectSO, int> kitchenObjectSO_RankDict;
    private Dictionary<KitchenObjectSO, float> kitchenObjectSO_BaseHeightDict;

    [SerializeField] private List<KitchenObjectSO_Height> kitchenObjectSO_OffsetHeights;
    [SerializeField] private List<KitchenObjectSO_Rank> kitchenObjectSO_Ranks; // Lower rank means it appears first (at the bottom)

    private void Awake()
    {
        kitchenObjectSO_HeightDict = new Dictionary<KitchenObjectSO, float>();
        kitchenObjectSO_RankDict = new Dictionary<KitchenObjectSO, int>();
        kitchenObjectSO_BaseHeightDict = new Dictionary<KitchenObjectSO, float>();
    }
    private void Start()
    {
        // Convert to hashMap for faster lookup
        foreach (KitchenObjectSO_Height kitchenObjectSO_Height in kitchenObjectSO_OffsetHeights)
        {
            if (kitchenObjectSO_Height.kitchenObjectSO == null)
            {
                Debug.LogError("KitchenObjectSO is null in KitchenObjectSO_Height list.");
                continue;   
            }
            kitchenObjectSO_HeightDict[kitchenObjectSO_Height.kitchenObjectSO] = kitchenObjectSO_Height.height;
        }

        foreach (KitchenObjectSO_Rank kitchenObjectSO_Rank in kitchenObjectSO_Ranks)
        {
            if (kitchenObjectSO_Rank.kitchenObjectSO == null)
            {
                Debug.LogError("KitchenObjectSO is null in KitchenObjectSO_Rank list.");
                continue;   
            }
            kitchenObjectSO_RankDict[kitchenObjectSO_Rank.kitchenObjectSO] = kitchenObjectSO_Rank.rank;
        }
    }

    public float CalculateHeight(KitchenObjectSO kitchenObjectSO, List<KitchenObjectSO> currentActiveIngredients)
    {
        if (!kitchenObjectSO_BaseHeightDict.ContainsKey(kitchenObjectSO))
        {
            Debug.LogError("KitchenObjectSO base height not recorded: " + kitchenObjectSO.name);
        }
        
        float heightOffset = 0f;
        int currentRank = kitchenObjectSO_RankDict[kitchenObjectSO];
        foreach (KitchenObjectSO activeIngredient in currentActiveIngredients)
        {
            if (activeIngredient == kitchenObjectSO)
            {
                continue;
            }

            if (kitchenObjectSO_HeightDict.TryGetValue(activeIngredient, out float heightTransform) &&
                kitchenObjectSO_RankDict.TryGetValue(activeIngredient, out int rank))
            {
                if (rank > currentRank)
                {
                    continue; // Only consider ingredients with lower or equal rank
                }
                heightOffset += heightTransform;
            }
            else
            {
                Debug.LogError("KitchenObjectSO not found in height or rank dictionary: " + activeIngredient.name);
            }
        }
        return kitchenObjectSO_BaseHeightDict[kitchenObjectSO] + heightOffset;
    }

    public void RecordKitchenObjectBaseHeights(List<PlateCompleteVisual.KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects)
    {
        foreach (PlateCompleteVisual.KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjects)
        {
            kitchenObjectSO_BaseHeightDict.Add(
                kitchenObjectSO_GameObject.kitchenObjectSO, 
                kitchenObjectSO_GameObject.gameObject.transform.localPosition.y
            );
        }
    }
}
