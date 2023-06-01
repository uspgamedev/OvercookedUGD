using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsDisplayUI : MonoBehaviour
{
    public List<Image> imagesList = new List<Image>(); 

    public void SetDisplays(Tuple[] tableTuples)
    {
        for (int i = 1; i < tableTuples.Count(); i++) 
        {
            imagesList[i - 1].sprite = tableTuples[i].sprite;

            if (imagesList[i - 1].sprite)
                imagesList[i - 1].transform.parent.gameObject.SetActive(true);
            else
                imagesList[i - 1].transform.parent.gameObject.SetActive(false);
        }
    }
}
