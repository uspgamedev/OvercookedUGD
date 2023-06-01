using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{

    public static BossManager Instance { get; private set; }

    public Transform BossUI;

    void Awake(){
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start(){
        GameManager.Instance.BossBattle += GameManager_BossBattle;
    }

    private void GameManager_BossBattle(object sender, EventArgs e){
        BossUI.gameObject.SetActive(true);
    }
}
