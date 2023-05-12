using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrderRecipeUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform list;
    [SerializeField] private Transform ingredientTemplate;

    [SerializeField] private GameObject manager;

    public OrderSO thisOrder;
    
    private float maxTimer;

    private float currentTimer;

    List<OrderSO> currentList;

    //[SerializeField] private GameObject bar;

    public static OrderRecipeUI Instance { get; private set;}


    private void Awake(){
        Instance = this;
        ingredientTemplate.gameObject.SetActive(false);
        currentList = manager.GetComponent<OrdersManager>().orderList;
    }

    public void Name(OrderSO order){
        recipeName.text = order.dishName;
        thisOrder = order;

        foreach(Transform ingredient in list){
            if(ingredient == ingredientTemplate) continue;
            else Destroy(ingredient.gameObject);
        }

        foreach(TupleSO ingredient in order.dishAux){
            Transform ingredientIcon = Instantiate(ingredientTemplate, list);
            ingredientIcon.gameObject.SetActive(true);
            ingredientIcon.GetComponent<Image>().sprite = ingredient.tupleSprite;
        }
    }

    private void Update(){

        if(currentTimer > 0){
            currentTimer = currentTimer - Time.deltaTime;
            //bar.GetComponent<Image>().fillAmount = currentTimer/maxTimer;
            this.gameObject.transform.GetChild(3).GetComponent<Image>().fillAmount = currentTimer/maxTimer;
        }
        else{
            currentList.Remove(currentList[0]);
            Destroy(this.gameObject);
        }
    }

    public void SetTimer(float t){
        maxTimer = t;
        currentTimer = t;
    }

}
