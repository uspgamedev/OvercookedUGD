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

    public int points;

    private bool done;
    
    private float maxTimer;

    private float currentTimer;

    public List<OrderSO> currentList;

    public static OrderRecipeUI Instance { get; private set;}


    private void Awake(){
        Instance = this;
        ingredientTemplate.gameObject.SetActive(false);
        currentList = manager.GetComponent<OrdersManager>().orderList;
        done = false;
    }

    public void Name(OrderSO order){
        recipeName.text = order.dishName;
        thisOrder = order;
        points = order.value;

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
        if(!done){
            if(currentTimer > 0){
                currentTimer = currentTimer - Time.deltaTime;
                this.gameObject.transform.GetChild(3).GetComponent<Image>().fillAmount = currentTimer/maxTimer;
            }
            else{
                currentList.Remove(thisOrder);
                Destroy(this.gameObject);
            }
        }
    }

    public void SetTimer(float t){
        maxTimer = t;
        currentTimer = t;
    }

    public void End(){
        done = true;
    }

}
