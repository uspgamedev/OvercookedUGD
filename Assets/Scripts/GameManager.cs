using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }

    public event EventHandler BossBattle;

    public GameObject player;

    public GameObject Camera;

    public Transform BossUI;
    
    public Transform GameOver;

    public bool Paused = false;

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
        Paused = true;
        AudioManager.Instance.Pause();
        Camera.GetComponent<Transform>().DOPunchPosition(GameEasings.CameraShakeVector, GameEasings.CameraShakeDuration).OnComplete(() => StartCoroutine(shakeCamera()));
        //treme
        //passa 1 segundo
        //treme
        PhaseTwo.gameObject.SetActive(true);
        StartCoroutine(waitOrderManager());
        OrdersManager.Instance.Clean();
        OrdersUI.Instance.Clean();
        Paused = false;
    }

    IEnumerator waitOrderManager(){
        yield return new WaitForSeconds(3f);
        PhaseTwo.GetComponent<CanvasGroup>().DOFade(1f, 1f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(2f);
        PhaseTwo.GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.OutCubic).OnComplete(() => BossBattle?.Invoke(this, EventArgs.Empty));
        yield return new WaitForSeconds(1f);
        BossUI.gameObject.SetActive(true);
    }

    IEnumerator shakeCamera(){
        yield return new WaitForSeconds(2f);
        Camera.GetComponent<Transform>().DOPunchPosition(GameEasings.CameraShakeVector, GameEasings.CameraShakeDuration);
    }

    private void OrdersManager_GameOver(object sender, System.EventArgs e){
        //Time.timeScale = 0f;
        Paused = true;
        AudioManager.Instance.Pause();
        GameOver.GetChild(1).GetComponent<Image>().DOFillAmount(FinalScore.fillAmount, GameEasings.StarFillDuration).SetEase(GameEasings.StarFillEase);
        GameOver.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You scored " + OrdersManager.Instance.GetScore() + " points";
        GameOver.gameObject.SetActive(true);
        GameOver.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.InCubic);
    }

    
}
