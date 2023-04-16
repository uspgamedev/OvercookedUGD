using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: definir a classe como abstrata e fazer as mudanças necessárias 
//TO-DO: fazer os sprites necessários aparecerem a depender do ingrediente
//TO-DO: deixar a troca de ingredientes mais conveniente
public class TableScript : MonoBehaviour
{
    public PlayerCont player;
    public IngredientsScriptable tupleScriptable;
    public Tuple[] tableTuple;
    public int capacity;

    private void Awake()
    {
        tableTuple = new Tuple[capacity];
        Tuple awakeTuple = new Tuple(tupleScriptable.ingredient, tupleScriptable.state);
        tableTuple[0] = awakeTuple;
        InitializeTable();
    }

    public void InitializeTable()
    {
        for(int i = 1; i < capacity; i++)
        {
            //Ingredient ingredient= new Ingredient(new string[] { "" });
            tableTuple[i] = Tuple.None;
        }
    }

    public virtual void UseTable() { }

    public bool isCurrent()
    {
        return (player.CheckTable() && player.GetTable().gameObject == gameObject);
    }

    public int Amount()
    {
        int amount = 0;
        for(int i = 0; i < capacity; i++)
        {
            if (tableTuple[i] != Tuple.None) { amount++; }
        }

        return amount;
    }

    public bool SubstitutionPossible()
    {
        return (player.currentTuple == Tuple.None || tableTuple[0] == Tuple.None);
    }

    public bool AdditionPossible()
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

    public void Unite()
    {
        for(int i = 1; i < capacity; i++)
        {
            if (tableTuple[i] != Tuple.None)
            {
                tableTuple[0].ingredient.ingredientArray = tableTuple[0].ingredient.ingredientArray.Concat(tableTuple[i].ingredient.ingredientArray).ToArray();
                tableTuple[i] = Tuple.None;
            }
        }
    }
}

public class CommonTable : TableScript
{
}

public class FinalTable : TableScript
{
    public List<Tuple> acceptable;
    public List<IngredientsScriptable> acceptableScriptables;

    private void Awake()
    {
        foreach (IngredientsScriptable ingredientsScriptable in acceptableScriptables)
        {
            acceptable.Add(new Tuple(ingredientsScriptable.ingredient, ingredientsScriptable.state));
        }
    }
}

