using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            Destroy(orderList.GetChild(1).gameObject);
            ClearTable();
        }
        else{
            ClearTable();
            Debug.Log("Errado");
        }
    }
    
}
