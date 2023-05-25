using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{

    public event EventHandler ReceivedOrder;
    public static OrdersManager Instance { get; private set;}

    [SerializeField] private RecipeListSO availableOrders;

    private float timer = 4f;
    private int maxRecipes = 5;

    public List<OrderSO> orderList;

    private void Awake(){
        Instance = this;

        orderList = new List<OrderSO>();
    }

    private void Start(){
        FinalTable.Instance.deliveredOrder += FinalTable_DeliveredOrder;
    }

    private void FinalTable_DeliveredOrder(object sender, FinalTable.DeliveredOrderEventArgs e){
        Debug.Log("Entregue");
        orderList.Remove(orderList[e.index]);
    }


    void Update(){

        timer -= Time.deltaTime;
        if(timer <= 0f){
            timer = 5f;

            if(orderList.Count < maxRecipes){
                OrderSO order = availableOrders.possibleOrders[UnityEngine.Random.Range(0, availableOrders.possibleOrders.Count)];

                orderList.Add(order);
                Debug.Log(order);
                ReceivedOrder?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public List<OrderSO> GetOrderList(){
        return orderList;
    }

}
