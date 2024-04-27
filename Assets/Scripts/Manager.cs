using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private int currentEnemyHand;
    private int currentPlayerHand;
    public string result { get; private set; }
    public bool shootSession { get; private set; }
    private bool resultCalculated;
    private bool recieveInput = true;
    public bool gameOver { get; private set; }
    private static Manager _instance;

    public static Manager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        shootSession = false;
        gameOver = false;
    }
    //public void GetPlayerPlay(int hand)
    //{
    //    currentPlayerHand = hand;
    //}
    public void GetEnemyPlay(int hand)
    {
        currentEnemyHand = hand;
    }
    string ReturnResult(int playerPlay, int enemyPlay)
    {
        if (playerPlay == enemyPlay)
        {
            return "Draw";
        }
        else if ((playerPlay == 0 && enemyPlay == 2) || (playerPlay == 1 && enemyPlay == 0) || (playerPlay == 2 && enemyPlay == 1))
        {
            return "Win";
        }
        else
        {
            return "Loss";
        }
    }

    public void OnConfirmClick(int playerhand)
    {
        currentPlayerHand = playerhand;
        if(!recieveInput || gameOver)
        {
            return;
        }
        CalculateResult();
    }    

    public void StartShootingSession()
    {
        shootSession = true;
        recieveInput = false;
        resultCalculated = false;
        //StopShootingSession();
    }
    public void CalculateResult()
    {
        if (!resultCalculated)
        {
            result = ReturnResult(currentPlayerHand, currentEnemyHand);
            resultCalculated = true;
            //result = "Win";
            Debug.Log("Result = " + result + " PlayerHand = " + currentPlayerHand + " EnemyHand = " + currentEnemyHand);
        }
    }

    public void StopShootingSession()
    {
        Debug.Log("Shooting Stopped");
        resultCalculated = false;
        shootSession = false;
        recieveInput = true;
    }

    public void GameOver(string gameOverText)
    {
        gameOver = true;
    }
}