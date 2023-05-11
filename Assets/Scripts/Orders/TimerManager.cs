using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerManager : MonoBehaviour
{
    
    public static TimerManager Instance { get; private set;}
    public float timer;
    private float MAX = 15f;
    private Image bar;

    public event EventHandler RunOut;

    void Awake(){

        Instance = this;
        bar = GetComponent<Image>();
        timer = MAX;
    }

    
    void Update(){

        timer -= Time.deltaTime;
        bar.fillAmount = timer / MAX;
        if(timer <= 0f){
            OrdersManager.Instance.GetOrderList().RemoveAt(0);
            RunOut?.Invoke(this, EventArgs.Empty);
        }
    }
}
