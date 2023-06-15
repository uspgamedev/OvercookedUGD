using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public enum GameState
{
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

    public event EventHandler<NextPhaseEventArgs> NextPhase;

    public event EventHandler<BossOrderedEventArgs> BossOrdered;

    public class BossOrderedEventArgs : EventArgs
    {
        public int number;

        public OrderSO newOrder;

    }

    public class GameOverEventArgs : EventArgs
    {
        public float fillScore;
        public float totalScore;

    }
    public class NextPhaseEventArgs : EventArgs
    {
        public float fillScore;
        public float totalScore;

    }

    public static OrdersManager Instance { get; private set; }

    [SerializeField] private RecipeListSO availableOrders;

    [SerializeField] private RecipeListSO bossOrders;

    [SerializeField] private Image score;

    private float passingScore;
    private float currentScore;
    private float bossScore;

    public GameState gameState;

    public GameObject Hint;

    private bool over;

    private float possiblePoints;

    private float timer;
    private float cooldown;
    private int maxRecipes;

    public List<OrderSO> orderList;

    private void Awake()
    {
        Instance = this;

        orderList = new List<OrderSO>();
    }

    private void Start()
    {
        FinalTable.Instance.deliveredOrder += FinalTable_DeliveredOrder;
        FinalTable.Instance.wrongOrder += FinalTable_WrongOrder;
        GameManager.Instance.BossBattle += GameManager_BossBattle;
        currentScore = 0;
        bossScore = 0;
        possiblePoints = 0;
        passingScore = 4;
        timer = 3f;
        cooldown = 10f;
        over = false;
        maxRecipes = 5;
        OrdersUI.Instance.SetTime(10f);
        gameState = GameState.PLAYING;
    }

    private void FinalTable_DeliveredOrder(object sender, FinalTable.DeliveredOrderEventArgs e)
    {
        Debug.Log("Entregue");
        orderList.Remove(orderList[e.index]);
        StartCoroutine(Review());
        if (gameState == GameState.PLAYING) currentScore = currentScore + e.points;
        else bossScore = bossScore + e.points;
        if (gameState != GameState.BOSS) score.DOFillAmount(currentScore / passingScore, GameEasings.StarFillDuration).SetEase(GameEasings.StarFillEase);
        if (currentScore >= passingScore && gameState == GameState.PLAYING)
        {
            PhaseOneEnd?.Invoke(this, EventArgs.Empty);
            //Debug.Log("antes de mudar estado pra ganhou é: " + gameState);
            gameState = GameState.WIN;
        }
        else if (gameState == GameState.BOSS)
        {
            currentScore = currentScore + 10;
            NextPhase?.Invoke(this, new NextPhaseEventArgs
            {
                fillScore = currentScore,
                totalScore = passingScore
            });
            gameState = GameState.GAMEOVER;
        }
        else
        {
            if (orderList.Count == 0 && over)
            {
                gameState = GameState.OVER;
            }
        }
        /*else if(bossScore >= passingScore && gameState == GameState.BOSS){
            currentScore = currentScore + bossScore;
            GameOver?.Invoke(this, new GameOverEventArgs{
                fillScore = currentScore,
                totalScore = passingScore
            });
            gameState = GameState.GAMEOVER;
        }*/
    }

    IEnumerator Review()
    {
        face.SetActive(false);
        face.SetActive(true);
        yield return new WaitForSeconds(1f);
        face.SetActive(false);
    }

    private void FinalTable_WrongOrder(object sender, EventArgs e)
    {
        mainCamera.DOPunchPosition(GameEasings.FinalTablePunchVector, GameEasings.FinalTablePunchDuration);
        Debug.Log("Errado");
    }

    private void GameManager_BossBattle(object sender, EventArgs e)
    {

        OrderSO order = bossOrders.possibleOrders[UnityEngine.Random.Range(0, bossOrders.possibleOrders.Count)];
        orderList.Add(order);
        OrdersUI.Instance.SetTime(25f);
        BossOrdered?.Invoke(this, new BossOrderedEventArgs
        {
            number = 1,
            newOrder = order
        });
        Hint.GetComponent<TextMeshProUGUI>().text = order.hint;
        timer = 25f;
        gameState = GameState.BOSS;
    }


    void Update()
    {
        if (possiblePoints >= 2 * passingScore) over = true;
        timer -= Time.deltaTime;
        if (orderList.Count == 0 && over && gameState == GameState.PLAYING) gameState = GameState.OVER;
        if (timer <= 0f && gameState == GameState.PLAYING)
        {
            if (!over && orderList.Count < maxRecipes)
            {
                OrderSO order = availableOrders.possibleOrders[UnityEngine.Random.Range(0, availableOrders.possibleOrders.Count)];

                orderList.Add(order);
                ReceivedOrder?.Invoke(this, EventArgs.Empty);
                possiblePoints = possiblePoints + order.value;
            }
            else if (orderList.Count == 0)
            {
                gameState = GameState.OVER;
            }

            timer = cooldown;
        }
        else if (orderList.Count == 0 && gameState == GameState.BOSS)
        {
            GameOver?.Invoke(this, new GameOverEventArgs
            {
                fillScore = currentScore,
                totalScore = passingScore
            });
            gameState = GameState.GAMEOVER;
        }

        if (over && orderList.Count == 0 && gameState == GameState.OVER)
        {
            //Debug.Log("antes de ver se a lista ta vazia é: " + gameState);
            GameOver?.Invoke(this, new GameOverEventArgs
            {
                fillScore = currentScore,
                totalScore = passingScore
            });
            //Debug.Log("antes de mudar estado pra perdeu é: " + gameState);
            gameState = GameState.GAMEOVER;
        }
    }

    public void Clean()
    {
        orderList.Clear();
    }

    public List<OrderSO> GetOrderList()
    {
        return orderList;
    }


    public float GetScore()
    {
        //if(currentScore >= passingScore) return passingScore;
        return currentScore;
    }
}
