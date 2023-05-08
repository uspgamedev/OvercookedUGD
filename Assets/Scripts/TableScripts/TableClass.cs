using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: fazer os sprites necessários aparecerem a depender do ingrediente
//TO-DO: deixar a troca de ingredientes mais conveniente

//Essa classe abstrata define as informações gerais das tábuas e alguns métodos básicos de interação. Implementações concretas podem ser encontradas na pasta TableTypes
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

    public void SubstituteAdd()
    {
        if (SubstitutionPossible)
        {
            //Subsituição simples
            Tuple tuple = tableTuples[0];
            tableTuples[0] = Player.currentTuple;
            Player.currentTuple = tuple;
        }
        else if (AdditionPossible)
        {
            //Adição a uma tábua que pode conter várias tuples
            Tuple firstNonNull = tableTuples.FirstOrDefault(t => t == Tuple.None);
            int index = System.Array.IndexOf(tableTuples, firstNonNull);
            tableTuples[index] = Player.currentTuple;

            Player.currentTuple = Tuple.None;
        }
        PaintTable();
    }

    protected void Unite()
    {
        //Une todas as tuples numa mistura única
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
}

