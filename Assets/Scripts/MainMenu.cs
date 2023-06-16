using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviour
{


    public event EventHandler Unpause;

    public static MainMenu Instance {get; private set;}

    void Awake(){
        if(Instance != null && Instance != this){
            //Destroy(this.gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void OptionsClose(){
        Unpause?.Invoke(this, EventArgs.Empty);
    }
}
