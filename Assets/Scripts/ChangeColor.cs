using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public SpriteRenderer sr;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.player.GetComponent<SpriteRenderer>().color = color;

        /*if (col.tag == "Player")
        {
            sr.color = color;
        }*/
    }

}
