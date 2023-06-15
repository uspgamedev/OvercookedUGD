using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cinemachine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }

    public event EventHandler BossBattle;

    public GameObject player;

    public GameObject Camera;

    public Transform BossUI;
    
    public Transform GameOver;

    public Transform NextPhase;

    public GameObject Wall;

    public GameObject RecipesUI;

    public GameObject BossRoom;

    public GameObject PageAlert;

    public GameObject NextPage;

    public bool Paused;

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
        }
        Paused = false;
    }

    private void Start(){
        OrdersManager.Instance.PhaseOneEnd += OrdersManager_PhaseOneEnd;
        OrdersManager.Instance.GameOver += OrdersManager_GameOver;
        OrdersManager.Instance.NextPhase += OrdersManager_NextPhase;
        SecretRecipe.Instance.foundPage += SecretRecipe_FoundPage;
        Wall.GetComponent<BoxCollider2D>().enabled = true;
        Camera.GetComponent<CameraController>().enabled = false;
        BossRoom.SetActive(false);
        Paused = false;
    }

    private void SecretRecipe_FoundPage(object sender, System.EventArgs e){
        StartCoroutine(waitRecipeManager());
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
    }

    IEnumerator waitOrderManager(){
        yield return new WaitForSeconds(3f);
        PhaseTwo.GetComponent<CanvasGroup>().DOFade(1f, 1f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(2f);
        PhaseTwo.GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.OutCubic).OnComplete(() => BossBattle?.Invoke(this, EventArgs.Empty));
        yield return new WaitForSeconds(1f);
        BossUI.gameObject.SetActive(true);
        Camera.GetComponent<CameraController>().enabled = true;
        Paused = false;
        BossRoom.SetActive(true);
        Wall.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator waitRecipeManager(){
        PageAlert.SetActive(true);
        PageAlert.GetComponent<CanvasGroup>().DOFade(1f, 1f).SetEase(Ease.InCubic);
        NextPage.gameObject.SetActive(true);
        RecipesUI.GetComponent<RecipesMenu>().unlocked = true;
        yield return new WaitForSeconds(2f);
        PageAlert.GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.InCubic);
    }

    IEnumerator shakeCamera(){
        yield return new WaitForSeconds(2f);
        Camera.GetComponent<Transform>().DOPunchPosition(GameEasings.CameraShakeVector, GameEasings.CameraShakeDuration);
    }

    private void OrdersManager_GameOver(object sender, OrdersManager.GameOverEventArgs e){
        //Time.timeScale = 0f;
        Paused = true;
        AudioManager.Instance.Pause();
        RecipesUI.SetActive(false);
        GameOver.GetChild(1).GetComponent<Image>().DOFillAmount(e.fillScore / e.totalScore, GameEasings.StarFillDuration).SetEase(GameEasings.StarFillEase);
        GameOver.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You scored " + OrdersManager.Instance.GetScore() + " points";
        GameOver.gameObject.SetActive(true);
        GameOver.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.InCubic);
    }

    private void OrdersManager_NextPhase(object sender, OrdersManager.NextPhaseEventArgs e)
    {
        //Time.timeScale = 0f;
        Paused = true;
        AudioManager.Instance.Pause();
        RecipesUI.SetActive(false);
        NextPhase.GetChild(1).GetComponent<Image>().DOFillAmount(e.fillScore / e.totalScore, GameEasings.StarFillDuration).SetEase(GameEasings.StarFillEase);
        NextPhase.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You scored " + OrdersManager.Instance.GetScore() + " points";
        NextPhase.gameObject.SetActive(true);
        NextPhase.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.InCubic);
    }


}
