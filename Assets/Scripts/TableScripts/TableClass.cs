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
    private TupleSpriteCrawler crawler = new TupleSpriteCrawler();

    public PlayerController Player => GameManager.Instance.player.GetComponent<PlayerController>();
    protected bool IsCurrent { get { return Player.CheckTable() && Player.GetTable().gameObject == gameObject; } }
    protected int Amount { get { return tableTuples.Where(t => t != null).Count(t => t != Tuple.None); } }
    protected bool SubstitutionPossible { get { return Player.currentTuple == Tuple.None || tableTuples[0] == Tuple.None; } }
    protected bool AdditionPossible { get { return Player.currentTuple != Tuple.None && tableTuples[0] != Tuple.None && Amount < capacity; } }


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
        tableTuples[0].sprite = crawler.GetTupleSprite(tableTuples[0]);
        PaintTable();
    }

    public void PaintTable()
    {
        foodRenderer.sprite = tableTuples[0].sprite;
    }

    protected void OrderTuples()
    {
        int loopLength = capacity;
        while (loopLength > 0 && tableTuples[0] == Tuple.None) 
        {
            for (int i = 0; i < tableTuples.Count() - 1; i++)
            {
                tableTuples[i] = tableTuples[i + 1];
            }
            tableTuples[tableTuples.Count() - 1] = Tuple.None;
            loopLength -= 1;
        }
    }

    protected void ClearTable(){
        for(int i = 0; i < capacity; i++){
            tableTuples[i] = Tuple.None;
        }
    }
}

