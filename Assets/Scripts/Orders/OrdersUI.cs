using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrdersUI : MonoBehaviour
{
    [SerializeField] private Transform list;
    [SerializeField] private Transform orderTemplate;

    public static OrdersUI Instance { get; private set;}

    private int i = 0;

    private float maxTime;

    private void Start(){
        OrdersManager.Instance.ReceivedOrder += OrdersManager_ReceivedOrder;
    }

    private void OrdersManager_ReceivedOrder(object sender, System.EventArgs e){
        i++;
        UpdateVisual();
    }

    private void Awake(){
        orderTemplate.gameObject.SetActive(false);
        Instance = this;
    }

    private void UpdateVisual(){

        Transform orderCard = Instantiate(orderTemplate, list);
        orderCard.name = "Order" + i;
        orderCard.gameObject.SetActive(true);
        OrderSO order = OrdersManager.Instance.GetOrderList()[OrdersManager.Instance.GetOrderList().Count - 1];
        OrderRecipeUI orderRUI = orderCard.GetComponent<OrderRecipeUI>();
        orderRUI.GetComponent<CanvasGroup>().DOFade(1,GameEasings.OrderCardFadeDuration).SetEase(GameEasings.OrderCardFadeEase);
        orderRUI.SetTimer(maxTime * (OrdersManager.Instance.GetOrderList().Count));
        orderRUI.Name(order);
        
    }

    public void Clean(){
        int childs = this.transform.GetChild(0).childCount;
        for(int i = 1; i < childs; i++){
            Destroy(this.transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    public void SetTime(float t){
        maxTime = t;
    }
}
