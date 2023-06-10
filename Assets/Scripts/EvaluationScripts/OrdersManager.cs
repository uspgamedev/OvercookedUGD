using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum GameState{
    WIN, GAMEOVER, PLAYING, BOSS, OVER
}

public class OrdersManager : MonoBehaviour
{

    public event EventHandler ReceivedOrder;

    public event EventHandler PhaseOneEnd;
    

    //temporario
    public Transform mainCamera;
    public GameObject face;

    public event EventHandler<GameOverEventArgs> GameOver;

    public class GameOverEventArgs : EventArgs{
        public float fillScore;
        public float totalScore;

    }

    public static OrdersManager Instance { get; private set;}

    [SerializeField] private RecipeListSO availableOrders;

    [SerializeField] private Image score;

    private float passingScore;
    private float currentScore;
    private float bossScore;

    public GameState gameState;

    private bool over;

    private float possiblePoints;

    private float timer;
    private float cooldown;
    private int maxRecipes;

    public List<OrderSO> orderList;

    private void Awake(){
        Instance = this;

        orderList = new List<OrderSO>();
    }

    private void Start(){
        FinalTable.Instance.deliveredOrder += FinalTable_DeliveredOrder;
        FinalTable.Instance.wrongOrder += FinalTable_WrongOrder;
        GameManager.Instance.BossBattle += GameManager_BossBattle;
        currentScore = 0;
        bossScore = 0;
        possiblePoints = 0;
        passingScore = 10;
        timer = 3f;
        cooldown = 5f;
        over = false;
        maxRecipes = 5;
        OrdersUI.Instance.SetTime(10f);
        gameState = GameState.PLAYING;
    }

    private void FinalTable_DeliveredOrder(object sender, FinalTable.DeliveredOrderEventArgs e){
        Debug.Log("Entregue");
        orderList.Remove(orderList[e.index]);
        timer = cooldown;
        StartCoroutine(Review());
        if(gameState == GameState.PLAYING) currentScore = currentScore + e.points;
        else bossScore = bossScore + e.points;
        if(gameState != GameState.BOSS) score.DOFillAmount(currentScore / passingScore, GameEasings.StarFillDuration).SetEase(GameEasings.StarFillEase);
        if(currentScore >= passingScore && gameState == GameState.PLAYING){
            PhaseOneEnd?.Invoke(this, EventArgs.Empty);
            //Debug.Log("antes de mudar estado pra ganhou é: " + gameState);
            gameState = GameState.WIN;
        }
        else if(bossScore >= passingScore && gameState == GameState.BOSS){
            currentScore = currentScore + bossScore;
            GameOver?.Invoke(this, new GameOverEventArgs{
                fillScore = currentScore,
                totalScore = passingScore
            });
            gameState = GameState.GAMEOVER;
        }
        if(orderList.Count == 0 && !over){
            gameState = GameState.OVER;
        }
    }

    IEnumerator Review(){
        face.SetActive(false);
        face.SetActive(true);
        yield return new WaitForSeconds(1f);
        face.SetActive(false);
    }

    private void FinalTable_WrongOrder(object sender, EventArgs e){
        mainCamera.DOPunchPosition(GameEasings.FinalTablePunchVector, GameEasings.FinalTablePunchDuration);
        Debug.Log("Errado");
    }

    private void GameManager_BossBattle(object sender, EventArgs e){
        possiblePoints = 0;
        timer = 2f;
        cooldown = 3f;
        passingScore = 10;
        over = false;
        OrdersUI.Instance.SetTime(5f);
        gameState = GameState.BOSS;
    }


    void Update(){
        if(possiblePoints >= 2 * passingScore) over = true;
        timer -= Time.deltaTime;
        if(timer <= 0f && (gameState == GameState.PLAYING || gameState == GameState.BOSS)){

            if(!over && orderList.Count < maxRecipes){
                OrderSO order = availableOrders.possibleOrders[UnityEngine.Random.Range(0, availableOrders.possibleOrders.Count)];

                orderList.Add(order);
                ReceivedOrder?.Invoke(this, EventArgs.Empty);
                possiblePoints = possiblePoints + order.value;
            }

            timer = cooldown;
        }

        if(over && gameState == GameState.OVER){
            //Debug.Log("antes de ver se a lista ta vazia é: " + gameState);
            currentScore = currentScore + bossScore;
            GameOver?.Invoke(this, new GameOverEventArgs{
                fillScore = currentScore,
                totalScore = passingScore
            });
            //Debug.Log("antes de mudar estado pra perdeu é: " + gameState);
            gameState = GameState.GAMEOVER;
        }
    }

    public void Clean(){
        orderList.Clear();
    }

    public List<OrderSO> GetOrderList(){
        return orderList;
    }

    public float GetScore(){
        //if(currentScore >= passingScore) return passingScore;
        return currentScore;
    }
}
