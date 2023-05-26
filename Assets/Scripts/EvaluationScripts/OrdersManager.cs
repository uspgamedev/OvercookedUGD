using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersManager : MonoBehaviour
{

    public event EventHandler ReceivedOrder;
    public static OrdersManager Instance { get; private set;}

    [SerializeField] private RecipeListSO availableOrders;

    [SerializeField] private Image score;

    private float maxScore;
    private float currentScore;

    private float timer = 4f;
    private int maxRecipes = 5;

    public List<OrderSO> orderList;

    private void Awake(){
        Instance = this;

        orderList = new List<OrderSO>();
    }

    private void Start(){
        FinalTable.Instance.deliveredOrder += FinalTable_DeliveredOrder;
        maxScore = 20;
        currentScore = 0;
    }

    private void FinalTable_DeliveredOrder(object sender, FinalTable.DeliveredOrderEventArgs e){
        Debug.Log("Entregue");
        orderList.Remove(orderList[e.index]);
        currentScore = currentScore + e.points;
        score.fillAmount = currentScore / maxScore;
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
