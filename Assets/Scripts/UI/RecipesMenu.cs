using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesMenu : MonoBehaviour
{

    public GameObject Page;
    public GameObject Hint;
    public GameObject nextPage;
    public GameObject nextButton;
    private bool pageActive;
    private bool bossActive;

    private bool secondOpen;

    public bool unlocked;

    void Start(){
        pageActive = false;
        unlocked = false;
    }
    public void RecipeBook ()
    {
        if(bossActive){
            Hint.SetActive(!bossActive);
            bossActive = !bossActive;
        }
        Page.SetActive(!pageActive);
        nextPage.SetActive(false);
        secondOpen = false;
        if(unlocked) nextButton.SetActive(true);
        pageActive = !pageActive;
    }

    public void BossRecipe ()
    {
        if(pageActive){
            Page.SetActive(!pageActive);
            pageActive = !pageActive;
        }
        if(secondOpen){
            nextPage.SetActive(false);
        }
        Hint.SetActive(!bossActive);
        bossActive = !bossActive;
    }

    public void NextPage(){
        nextButton.SetActive(false);
        secondOpen = true;
        nextPage.SetActive(true);
    }
}
