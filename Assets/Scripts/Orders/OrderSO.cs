using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrderSO", menuName = "ScriptableObjects/OrderSO")]
public class OrderSO : ScriptableObject {
    
    public List<IngredientSO> dishRecipe;

    public string dishName;
}
