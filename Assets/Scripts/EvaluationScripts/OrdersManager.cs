using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrdersManager : MonoBehaviour
{

    public event EventHandler ReceivedOrder;

    public event EventHandler PhaseOneEnd;

    public event EventHandler GameOver;
    public static OrdersManager Instance { get; private set;}

    [SerializeField] private RecipeListSO availableOrders;

    [SerializeField] private Image score;

    public float passingScore;
    private float currentScore;

    private bool over;

    private float possiblePoints;

    private float timer;
    private int maxRecipes = 5;

    public List<OrderSO> orderList;

    private void Awake(){
        Instance = this;

        orderList = new List<OrderSO>();
    }

    private void Start(){
        FinalTable.Instance.deliveredOrder += FinalTable_DeliveredOrder;
        currentScore = 0;
        possiblePoints = 0;
        timer = 3f;
        over = false;
    }

    private void FinalTable_DeliveredOrder(object sender, FinalTable.DeliveredOrderEventArgs e){
        Debug.Log("Entregue");
        orderList.Remove(orderList[e.index]);
        timer = 5f;
        currentScore = currentScore + e.points;
        score.DOFillAmount(currentScore / passingScore, GameEasings.StarFillDuration).SetEase(GameEasings.StarFillEase);
        if(currentScore >= passingScore){
            PhaseOneEnd?.Invoke(this, EventArgs.Empty);
        }
    }


    void Update(){
        if(possiblePoints >= 2 * passingScore) over = true;
        timer -= Time.deltaTime;
        if(timer <= 0f){

            if(!over && orderList.Count < maxRecipes){
                OrderSO order = availableOrders.possibleOrders[UnityEngine.Random.Range(0, availableOrders.possibleOrders.Count)];

                orderList.Add(order);
                ReceivedOrder?.Invoke(this, EventArgs.Empty);
                possiblePoints = possiblePoints + order.value;
            }

            timer = 5f;
        }

        if(over){
            if(orderList.Count == 0){
                GameOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public List<OrderSO> GetOrderList(){
        return orderList;
    }

}
