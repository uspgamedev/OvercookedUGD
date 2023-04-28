using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderRecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform list;
    [SerializeField] private Transform ingredientTemplate;


    private void Awake(){
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

}
