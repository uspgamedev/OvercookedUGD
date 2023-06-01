using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }

    public GameObject player;
    
    public Transform GameOver;

    [SerializeField] private Image FinalScore;

    [Header("Food sprites")]
    public Sprite genericFood;

    private void Awake()
    {
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

    private void Start(){
        OrdersManager.Instance.PhaseOneEnd += OrdersManager_PhaseOneEnd;
        OrdersManager.Instance.GameOver += OrdersManager_GameOver;
    }

    private void OrdersManager_PhaseOneEnd(object sender, System.EventArgs e){
        Time.timeScale = 0f;
        AudioManager.Instance.Pause();
    }

    private void OrdersManager_GameOver(object sender, System.EventArgs e){
        Time.timeScale = 0f;
        AudioManager.Instance.Pause();
        GameOver.GetChild(0).GetComponent<Image>().fillAmount = FinalScore.fillAmount;
        GameOver.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You scored " + (FinalScore.fillAmount * 10) + " points";
        GameOver.gameObject.SetActive(true);
    }

    
}
