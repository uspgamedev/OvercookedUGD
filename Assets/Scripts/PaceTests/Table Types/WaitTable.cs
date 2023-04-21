using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: Mais condições no UseTable (?)
public class WaitTable : TableClass
{
    public float boilTime;
    public float overcookTime;
    public State stateTransition;
    public State overcookedState;
    private bool coroutineRunning;

    public override void UseTable()
    {
        //Tem que chegar se todos os ingredientes são mixeable... (tlvz fazer um método no TableScript pra esse tipo de coisa)
        if(!coroutineRunning)
            StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        coroutineRunning = true;
        float timer = 0;
        while (timer <= overcookTime + 1)
        {
            //Debug.Log(timer);
            timer += Time.deltaTime;

            if (timer >= boilTime && timer < overcookTime)
            {
                Unite();
                Tuple.ChangeState(tableTuple[0], stateTransition);
                tableTuple[0].ingredient.overcookeable = true;
            }
            else if (timer >= overcookTime)
            {
                Tuple.ChangeState(tableTuple [0], overcookedState);
            }
            yield return null;
        }
        coroutineRunning = false;
    }
}
