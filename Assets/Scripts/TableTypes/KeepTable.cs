using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: Unite no UseTable?
public class KeepTable : TableClass
{
    public float sliceTime;
    public State stateTransition;

    public override void UseTable()
    {
        //Três estados: vazio, incompleto e completo
        if (Player.currentTuple == Tuple.None && tableTuples [0] != Tuple.None)
        {
            StartCoroutine(KeepCoroutine());
        }
    }

    IEnumerator KeepCoroutine()
    {
        float timer = 0;
        while (IsCurrent)
        {
            timer += Time.deltaTime;
            if (timer >= sliceTime)
            {
                Tuple.ChangeState(tableTuples [0], stateTransition);
                SubstituteAdd();
                yield break;
            }
            yield return null;
        }
        
    }
}