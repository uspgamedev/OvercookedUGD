using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: fazer os sprites necessários aparecerem a depender do ingrediente
//TO-DO: deixar a troca de ingredientes mais conveniente
public abstract class TableClass : MonoBehaviour
{
    public PlayerCont player;
    //public IngredientsScriptable tupleScriptable;
    public Tuple[] tableTuple;
    public int capacity = 1;

    private void Start()
    {
        InitializeTable();
    }

    protected void InitializeTable()
    {
        for (int i = 1; i < capacity; i++)
        {
            //Ingredient ingredient= new Ingredient(new string[] { "" });
            tableTuple[i] = Tuple.None;
        }
    }

    public abstract void UseTable();

    protected bool isCurrent()
    {
        return (player.CheckTable() && player.GetTable().gameObject == gameObject);
    }

    protected int Amount()
    {
        int amount = 0;
        for(int i = 0; i < capacity; i++)
        {
            if (tableTuple[i] != Tuple.None) { amount++; }
        }

        return amount;
    }

    protected bool SubstitutionPossible()
    {
        return (player.currentTuple == Tuple.None || tableTuple[0] == Tuple.None);
    }

    protected bool AdditionPossible()
    {
        return (player.currentTuple != Tuple.None && tableTuple[0] != Tuple.None && Amount() < capacity);
    }

    public void SubstituteAdd()
    {
        if (SubstitutionPossible())
        {
            //Subsituição simples
            Tuple tuple = tableTuple [0];
            tableTuple[0] = player.currentTuple;
            player.currentTuple = tuple;
        }
        else if (AdditionPossible())
        {
            for(int i = 0; i < capacity; i++)
            {
                if (tableTuple[i] == Tuple.None)
                {
                    tableTuple[i] = player.currentTuple;
                    break;
                }
            }

            player.currentTuple = Tuple.None;
        }
    }

    protected void Unite()
    {
        for(int i = 1; i < capacity; i++)
        {
            if (tableTuple[i] != Tuple.None)
            {
                string[] ingredientsCopy = (string[])tableTuple[0].ingredient.ingredientArray.Clone();
                tableTuple[0].ingredient.ingredientArray = ingredientsCopy.Concat(tableTuple[i].ingredient.ingredientArray).ToArray();
                tableTuple[i] = Tuple.None;
            }
        }
    }
}

