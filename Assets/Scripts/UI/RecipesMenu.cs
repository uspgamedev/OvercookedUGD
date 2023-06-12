using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesMenu : MonoBehaviour
{

    public GameObject Page;
    public GameObject Hint;
    private bool pageActive;
    private bool bossActive;

    void Start(){
        pageActive = false;
    }
    public void RecipeBook ()
    {
        if(bossActive){
            Hint.SetActive(!bossActive);
            bossActive = !bossActive;
        }
        Page.SetActive(!pageActive);
        pageActive = !pageActive;
    }

    public void BossRecipe ()
    {
        if(pageActive){
            Page.SetActive(!pageActive);
            pageActive = !pageActive;
        }
        Hint.SetActive(!bossActive);
        bossActive = !bossActive;
    }
}
