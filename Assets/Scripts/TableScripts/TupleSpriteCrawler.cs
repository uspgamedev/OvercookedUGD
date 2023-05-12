using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TupleSpriteCrawler
{
    private const string FOLDER_NAME = "TupleSOs";

    public Sprite GetTupleSprite(Tuple tuple)
    {
        return GetTupleSprite(tuple.ingredient.ingredientArray, tuple.state);
    }
    public Sprite GetTupleSprite(string [] ingredientArray, State state)
    {
        TupleSO[] tupleSOs = GetTupleSOs();
        foreach(TupleSO tupleSO in tupleSOs)
        {
            if (tupleSO.ingredient.ingredientArray.OrderBy(x=>x).SequenceEqual(ingredientArray.OrderBy(x=>x)) && tupleSO.state == state)
            {
                return tupleSO.tupleSprite;
            }
        }
        return GetGenericSprite(state);
    }

    //Incompleto
    private Sprite GetGenericSprite(State state)
    {
        switch (state)
        {
            case State.None: 
                return null;
            default:
                return null;
        }
    }

    private TupleSO[] GetTupleSOs()
    {
        Object[] objectArray = Resources.LoadAll(FOLDER_NAME, typeof(TupleSO));

        TupleSO[] tupleSOs = new TupleSO[objectArray.Length];

        for (int i = 0; i < objectArray.Length; i++)
        {
            TupleSO tupleSO = (TupleSO)objectArray[i];
            tupleSOs[i] = tupleSO;
        }
        return tupleSOs;
    } 
}
