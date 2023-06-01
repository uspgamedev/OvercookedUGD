using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }

    public event EventHandler BossBattle;

    public GameObject player;
    
    public Transform GameOver;

    public Transform PhaseTwo;

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
        PhaseTwo.gameObject.SetActive(true);
        BossBattle?.Invoke(this, EventArgs.Empty);

    }

    private void OrdersManager_GameOver(object sender, System.EventArgs e){
        Time.timeScale = 0f;
        AudioManager.Instance.Pause();
        GameOver.GetChild(1).GetComponent<Image>().fillAmount = FinalScore.fillAmount;
        GameOver.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You scored " + (FinalScore.fillAmount * 10) + " points";
        GameOver.gameObject.SetActive(true);
    }

    
}
