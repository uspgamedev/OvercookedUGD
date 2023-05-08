using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scriptables", menuName ="TupleSO")]
public class TupleSO : ScriptableObject
{
    public Ingredient ingredient;
    public State state;
    public Sprite tupleSprite;

    public Tuple tuple => new Tuple(ingredient, state, tupleSprite);
}
