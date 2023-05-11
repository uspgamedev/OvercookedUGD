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

    private float maxTime = 15f;

    private List<OrderSO> orderList;

    private void Awake(){
        Instance = this;

        orderList = new List<OrderSO>();
    }

    private void Start(){
        OrderRecipeUI.Instance.timedOut += OrderRecipeUI_TimedOut;
    }

    private void OrderRecipeUI_TimedOut(object sender, System.EventArgs e){
        Debug.Log("Morri");
        /*Debug.Log(orderList[0]);
        Debug.Log(orderList);*/
        orderList.Remove(orderList[0]);
        //Debug.Log(orderList);
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
