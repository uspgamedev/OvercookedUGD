using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scriptables", menuName ="Scriptable Objects/Tuple Scriptable")]
public class IngredientsScriptable : ScriptableObject
{
    public Ingredient ingredient;
    public State state;
}
