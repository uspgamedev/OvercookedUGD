using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTable : TableClass
{
    private Table ThisTable => GetComponent<Table>();

    public float boilTime;
    public float overcookTime;
    public State stateTransition;
    public State overcookedState;
    private bool _coroutineRunning;

    //Second fill must be the overcook one
    private TableFillUI[] FillUIs => GetComponents<TableFillUI>();

    public override void UseTable()
    {
        //Tem que chegar se todos os ingredientes s�o mixeable... (tlvz fazer um m�todo no TableScript pra esse tipo de coisa)
        //Tamb�m, deve-se decidir se a t�bua s� pode ser usada quando cheia ou n�o
        if(!_coroutineRunning)
            StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        _coroutineRunning = true;
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
                FillUIs[1].SetImageActive(false);
            }

            if (tableTuples[0] == Tuple.None)
            {
                ThisTable.GenerateTable();

                FillUIs[0].SetImageActive(false);
                FillUIs[1].SetImageActive(false);
                break;
            }
            SetFirstSpriteWithCrawler();
            PaintTable();
            SetUI(timer);

            yield return null;
        }
        _coroutineRunning = false;
    }

    private void SetUI(float timer)
    {
        if (FillUIs.Length != 2)
            throw new System.Exception("Wait table UI has wrong count of fillers.");
        

        if (timer < boilTime)
        {
            FillUIs[0].SetImageActive(true);
            FillUIs[0].SetRelativeFill(timer, boilTime);
        }
        else if (timer < overcookTime)
        {
            FillUIs[0].SetImageActive(false);
            FillUIs[1].SetImageActive(true);
            FillUIs[1].SetRelativeFill(timer - boilTime, overcookTime - boilTime);
        }
    }
}
