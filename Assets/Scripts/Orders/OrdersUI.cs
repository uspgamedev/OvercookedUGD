using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    [SerializeField] private Transform list;
    [SerializeField] private Transform orderTemplate;

    private int i = 0;

    private float maxTime = 10f;

    private void Start(){
        OrdersManager.Instance.ReceivedOrder += OrdersManager_ReceivedOrder;
    }

    private void OrdersManager_ReceivedOrder(object sender, System.EventArgs e){
        i++;
        UpdateVisual();
    }

    private void Awake(){
        orderTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual(){

        Transform orderCard = Instantiate(orderTemplate, list);
        orderCard.name = "Order" + i;
        orderCard.gameObject.SetActive(true);
        OrderSO order = OrdersManager.Instance.GetOrderList()[OrdersManager.Instance.GetOrderList().Count - 1];
        OrderRecipeUI orderRUI = orderCard.GetComponent<OrderRecipeUI>();
        orderRUI.SetTimer(maxTime * (OrdersManager.Instance.GetOrderList().Count));
        orderRUI.Name(order);
        
    }
}
