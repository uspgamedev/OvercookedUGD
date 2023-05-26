using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using DG.Tweening;

public class FinalTable : TableClass
{

    [SerializeField] public Transform orderList;

    public event EventHandler<DeliveredOrderEventArgs> deliveredOrder;
    public class DeliveredOrderEventArgs : EventArgs{
        public int index;
        public int points;
    }

    public static FinalTable Instance { get; private set;}

    public void Awake(){
        Instance = this;
    }

    public override void SubstituteAdd()
    {
        base.SubstituteAdd();

        OrderRecipeUI[] orders = orderList.GetComponentsInChildren<OrderRecipeUI>();
        List<Ingredient> orderedIngredients = orderList.GetChild(1).GetComponent<OrderRecipeUI>().currentList.Select(order => order.dishRecipe).ToList();
        if (orderedIngredients.Contains(tableTuples[0].ingredient))
        {
            OrderRecipeUI order = orders[orderedIngredients.IndexOf(tableTuples[0].ingredient)];
            order.End();
            deliveredOrder?.Invoke(this, new DeliveredOrderEventArgs
            {
                index = orderedIngredients.IndexOf(tableTuples[0].ingredient),
                points = order.points
            });
            order.gameObject.GetComponent<Transform>().DOPunchPosition(GameEasings.FinalTablePunchVector, GameEasings.FinalTablePunchDuration).OnComplete(()=>
            Destroy(order.gameObject));
            
        }
        else
        {
            Debug.Log("Errado");
        }

        PlayFadeAnimation();
    }

    public override void UseTable() {}

    private void PlayFadeAnimation()
    {
        PaintTable();
        Vector3 originalScale = foodRenderer.transform.localScale;
        foodRenderer.transform.DOScale(Vector3.zero, GameEasings.FinalTableFadeDuration).SetEase(GameEasings.FinalTableFadeEase).OnComplete(() =>
        {
            ClearTable();
            foodRenderer.transform.localScale = originalScale;
            PaintTable();
        });
    }

}
