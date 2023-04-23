using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    [SerializeField] private Transform list;
    [SerializeField] private Transform orderTemplate;

    private void Start(){
        OrdersManager.Instance.ReceivedOrder += OrdersManager_ReceivedOrder;

        UpdateVisual();
    }

    private void OrdersManager_ReceivedOrder(object sender, System.EventArgs e){
        UpdateVisual();
    }

    private void Awake(){
        orderTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual(){
        foreach (Transform order in list){
            if(order == orderTemplate) continue;
            else Destroy(order.gameObject);
        }

        foreach(OrderSO order in OrdersManager.Instance.GetOrderList()){
            Transform orderCard = Instantiate(orderTemplate, list);
            orderCard.gameObject.SetActive(true);
            orderCard.GetComponent<OrderRecipeUI>().Name(order);
        }
    }
}
