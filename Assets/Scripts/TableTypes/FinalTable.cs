using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class FinalTable : TableClass
{

    [SerializeField] public Transform orderList;

    public event EventHandler<DeliveredOrderEventArgs> deliveredOrder;
    public class DeliveredOrderEventArgs : EventArgs{
        public int index;
    }

    private int i;

    public static FinalTable Instance { get; private set;}

    public void Awake(){
        Instance = this;
    }
    
    public override void UseTable() {

        OrderRecipeUI[] orders = orderList.GetComponentsInChildren<OrderRecipeUI>();
        bool delivered = false;
        i = 0;
        foreach(OrderRecipeUI order in orders){
            if(tableTuples[0].ingredient == order.thisOrder.dishRecipe){
                deliveredOrder?.Invoke(this, new DeliveredOrderEventArgs { index = i });
                order.gameObject.GetComponent<Transform>().DOPunchPosition(new Vector3(-10f, 0f, 0f), 2f);
                Destroy(order.gameObject);
                ClearTable();
                delivered = true;
                break;
            }
            i++;
        }
        if(!delivered){
            ClearTable();
            Debug.Log("Errado");
        }
        /*if(tableTuples[0].ingredient == orderList.GetChild(1).GetComponent<OrderRecipeUI>().thisOrder.dishRecipe){
            deliveredOrder?.Invoke(this, EventArgs.Empty);
            orderList.GetChild(1).gameObject.GetComponent<Transform>().DOPunchPosition(new Vector3(-10f, 0f, 0f), 2f);
            Destroy(orderList.GetChild(1).gameObject);
            ClearTable();
        }
        else{
            ClearTable();
            Debug.Log("Errado");
        }*/
    }
    
}
