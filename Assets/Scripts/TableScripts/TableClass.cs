using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: fazer os sprites necess�rios aparecerem a depender do ingrediente
//TO-DO: deixar a troca de ingredientes mais conveniente

//Essa classe abstrata define as informa��es gerais das t�buas e alguns m�todos b�sicos de intera��o. Implementa��es concretas podem ser encontradas na pasta TableTypes
public abstract class TableClass : MonoBehaviour
{
    public Tuple[] tableTuples;
    public int capacity = 1;

    public SpriteRenderer foodRenderer;

    //Crawler para procurar os sprites de misturas dentro da pasta Resources\TupleSOs
    private TupleSpriteCrawler _crawler = new TupleSpriteCrawler();

    public PlayerController Player => GameManager.Instance.player.GetComponent<PlayerController>();
    protected bool IsCurrent => (Player.CheckTable() && Player.GetTable().gameObject == gameObject);
    protected int Amount => tableTuples.Where(t => t != null).Count(t => t != Tuple.None);
    protected bool SubstitutionPossible => (Player.currentTuple == Tuple.None || tableTuples[0] == Tuple.None);
    protected bool AdditionPossible => (Player.currentTuple != Tuple.None && tableTuples[0] != Tuple.None && Amount < capacity);
    private IngredientsDisplayUI DisplayUI
    {
        get
        {
            IngredientsDisplayUI manyUI = GetComponent<IngredientsDisplayUI>();
            if (manyUI == null || manyUI.imagesList.Count != capacity - 1)
                throw new System.Exception("Table UI images count not equal to capacity: IngredientsDisplayUI component might be missing or not correctly setup.");
            return manyUI;
        }
    }

    public abstract void UseTable();

    public virtual void SubstituteAdd()
    {
        if (SubstitutionPossible)
        {
            //Subsitui��o simples
            Tuple tuple = tableTuples[0];
            tableTuples[0] = Player.currentTuple;
            Player.currentTuple = tuple;
        }
        else if (AdditionPossible)
        {
            //Adi��o a uma t�bua que pode conter v�rias tuples
            Tuple firstNonNull = tableTuples.FirstOrDefault(t => t == Tuple.None);
            int index = System.Array.IndexOf(tableTuples, firstNonNull);
            tableTuples[index] = Player.currentTuple;

            Player.currentTuple = Tuple.None;
        }
        OrderTuples();
        PaintTable();
    }

    protected void Unite()
    {
        //Une todas as tuples numa mistura �nica
        for (int i = 1; i < capacity; i++)
        {
            if (tableTuples[i] != Tuple.None)
            {
                tableTuples[0].ingredient = Ingredient.ConcatIngredients(tableTuples[0].ingredient, tableTuples[i].ingredient);
                tableTuples[i] = Tuple.None;
            }
        }
        //Crawler procura pelo sprite
    }

    protected void SetFirstSpriteWithCrawler()
    {
        tableTuples[0].sprite = _crawler.GetTupleSprite(tableTuples[0]);
    }

    public void PaintTable()
    {
        foodRenderer.sprite = tableTuples[0].sprite;
        DisplayUI.SetDisplays(tableTuples);
    }

    protected void OrderTuples()
    {
        /*
        int loopLength = capacity;
        while (loopLength > 0 && tableTuples[0] == Tuple.None)
        {
            for (int i = 0; i < tableTuples.Count() - 1; i++)
            {
                tableTuples[i] = tableTuples[i + 1];
            }
            tableTuples[tableTuples.Count() - 1] = Tuple.None;
            loopLength -= 1;
        }*/
        tableTuples = tableTuples.OrderByDescending(t => t != Tuple.None).ToArray();
    }

    protected void ClearTable()
    {
        for (int i = 0; i < capacity; i++)
        {
            tableTuples[i] = Tuple.None;
        }
    }
}

