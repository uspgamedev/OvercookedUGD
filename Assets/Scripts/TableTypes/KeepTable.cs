using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO-DO: Unite no UseTable?
public class KeepTable : TableClass
{
    public float sliceTime;
    public State stateTransition;

    private TableFillUI FillUI => GetComponent<TableFillUI>();

    public override void UseTable()
    {
        //Tr�s estados: vazio, incompleto e completo
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
                SetFirstSpriteWithCrawler();
                SubstituteAdd();
                PaintTable();
                Player.PaintPlayerTuple();
                FillUI.SetImageActive(false);
                yield break;
            }
            SetUI(timer);
            yield return null;
        }
        FillUI.SetImageActive(false);
    }

    private void SetUI(float fillAmount)
    {
        FillUI.SetImageActive(true);
        FillUI.SetRelativeFill(fillAmount, sliceTime);
    }
}