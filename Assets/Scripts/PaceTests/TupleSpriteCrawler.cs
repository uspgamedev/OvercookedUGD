using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TupleSpriteCrawler : MonoBehaviour
{
    private const string FOLDER_NAME = "TupleSOs";

    public static Sprite GetTupleSprite(string [] ingredientArray, State state)
    {
        TupleSO[] tupleSOs = GetTupleSOs();
        foreach(TupleSO tupleSO in tupleSOs)
        {
            if (tupleSO.ingredient.ingredientArray.SequenceEqual(ingredientArray) && tupleSO.state == state)
            {
                return tupleSO.tupleSprite;
            }
        }
        return GetGenericSprite(state);
    }

    //Incompleto
    private static Sprite GetGenericSprite(State state)
    {
        switch (state)
        {
            case State.None: 
                return null;
            default:
                return null;
        }
    }

    private static TupleSO[] GetTupleSOs()
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
