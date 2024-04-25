using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private int currentEnemyHand;
    private int currentPlayerHand;
    public string result { get; private set; }
    public bool hasWon { get; private set; }
    public bool draw { get; private set; }
    public bool shootSession { get; private set; }
    private bool resultCalculated;
    private bool recieveInput = true;
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
    }
    public void GetPlayerPlay(int hand)
    {
        currentPlayerHand = hand;
    }
    public void GetEnemyPlay(int hand)
    {
        currentEnemyHand = hand;
    }
    string ReturnResult(int playerPlay, int enemyPlay)
    {
        if (playerPlay == enemyPlay)
        {
            draw = true;
            return "Draw";
        }
        else if ((playerPlay == 0 && enemyPlay == 2) || (playerPlay == 1 && enemyPlay == 0) || (playerPlay == 2 && enemyPlay == 1))
        {
            hasWon = true;
            return "Win";
        }
        else
        {
            hasWon = false;
            return "Loss";
        }
    }

    public void OnConfirmClick()
    {
        if(!recieveInput)
        {
            return;
        }
        StartShootingSession();
    }    

    public void StartShootingSession()
    {
        if (!resultCalculated)
        {
            result = ReturnResult(currentPlayerHand, currentEnemyHand);
            Debug.Log("Result = " + result + " PlayerHand = " + currentPlayerHand + " EnemyHand = " + currentEnemyHand);
            resultCalculated = true;
        }
        shootSession = true;
        recieveInput = false;
    }

    public void StopShootingSession()
    {
        resultCalculated = false;
        shootSession = false;
        recieveInput = true;
    }
}