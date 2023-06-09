using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrderSO", menuName = "ScriptableObjects/OrderSO")]
public class OrderSO : ScriptableObject {
    
    public Ingredient dishRecipe;
    public List<TupleSO> dishAux;

    public string hint;

    public float expectedTime;
    
    public Sprite me;

    public string dishName;

    public int value;
}
