using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTable : TableClass
{
    private Table table => GetComponent<Table>();

    public float boilTime;
    public float overcookTime;
    public State stateTransition;
    public State overcookedState;
    private bool coroutineRunning;

    public override void UseTable()
    {
        //Tem que chegar se todos os ingredientes s�o mixeable... (tlvz fazer um m�todo no TableScript pra esse tipo de coisa)
        //Tamb�m, deve-se decidir se a t�bua s� pode ser usada quando cheia ou n�o
        if(!coroutineRunning)
            StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        coroutineRunning = true;
        float timer = 0;
        while (timer <= overcookTime + 1)
        {
            timer += Time.deltaTime;

            if (timer >= boilTime && timer < overcookTime)
            {
                Unite();
                Tuple.ChangeState(tableTuples[0], stateTransition);
            }
            else if (timer >= overcookTime)
            {
                Tuple.ChangeState(tableTuples [0], overcookedState);
            }

            if (tableTuples[0] == Tuple.None)
            {
                table.GenerateTable();
                break;
            }
            yield return null;
        }
        coroutineRunning = false;
    }
}
