using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{

    private float timer = 0.5f;

    void Update()
    {
        timer -= Time.deltaTime;
        checkTimer();
    }

    void checkTimer(){
        if(timer <= 0){
            Destroy(gameObject);
        }
    }
}
