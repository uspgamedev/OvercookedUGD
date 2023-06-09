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

    public event EventHandler wrongOrder;

    public class DeliveredOrderEventArgs : EventArgs{
        //public int index;

        public OrderSO aux;
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
                //index = orderedIngredients.IndexOf(tableTuples[0].ingredient),
                aux = order.thisOrder,
                points = order.points
            });
            //order.gameObject.GetComponent<Transform>().DOPunchPosition(GameEasings.FinalTablePunchVector, GameEasings.FinalTablePunchDuration).OnComplete(()=>
            Destroy(order.gameObject);

            //foodRenderer.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            wrongOrder?.Invoke(this, EventArgs.Empty);
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
