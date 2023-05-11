using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrderRecipeUI : MonoBehaviour
{

    public event EventHandler timedOut;
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform list;
    [SerializeField] private Transform ingredientTemplate;
    
    private float maxTimer;

    private float currentTimer;

    [SerializeField] private GameObject bar;

    public static OrderRecipeUI Instance { get; private set;}


    private void Awake(){
        Instance = this;
        ingredientTemplate.gameObject.SetActive(false);
    }

    public void Name(OrderSO order){
        recipeName.text = order.dishName;

        foreach(Transform ingredient in list){
            if(ingredient == ingredientTemplate) continue;
            else Destroy(ingredient.gameObject);
        }

        foreach(IngredientSO ingredient in order.dishRecipe){
            Transform ingredientIcon = Instantiate(ingredientTemplate, list);
            ingredientIcon.gameObject.SetActive(true);
            ingredientIcon.GetComponent<Image>().sprite = ingredient.icon;
        }
    }

    private void Update(){
        if(currentTimer > 0){
            currentTimer = currentTimer - Time.deltaTime;
            bar.GetComponent<Image>().fillAmount = currentTimer/maxTimer;
        }
        else{
            timedOut?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }
    }

    public void SetTimer(float t){
        maxTimer = t;
        currentTimer = t;
    }

}
