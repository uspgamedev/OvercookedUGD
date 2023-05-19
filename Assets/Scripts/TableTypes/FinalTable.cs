using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class FinalTable : TableClass
{

    [SerializeField] public Transform orderList;

    public event EventHandler deliveredOrder;

    public static FinalTable Instance { get; private set;}

    public void Awake(){
        Instance = this;
    }
    
    public override void UseTable() {

        if(tableTuples[0].ingredient == orderList.GetChild(1).GetComponent<OrderRecipeUI>().thisOrder.dishRecipe){
            deliveredOrder?.Invoke(this, EventArgs.Empty);
            orderList.GetChild(1).gameObject.GetComponent<Transform>().DOPunchPosition(new Vector3(-10f, 0f, 0f), 2f);
            //Destroy(orderList.GetChild(1).gameObject);
            ClearTable();
        }
        else{
            ClearTable();
            Debug.Log("Errado");
        }
    }
    
}
