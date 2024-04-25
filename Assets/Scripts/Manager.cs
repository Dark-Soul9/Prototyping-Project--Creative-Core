using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private int currentEnemyHand;
    private int currentPlayerHand;
    private string result;
    public bool hasWon { get; private set; }
    public bool draw { get; private set; }
    public bool gameRunning { get; private set; }
    public bool moving { get; private set; }
    public bool shootingActive { get; private set; }
    public bool canShoot { get; private set; }
    public bool recieveInput = true;
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
    public void GetHandPlay(int hand, string type)
    {
        if (type == "Player")
        {
            currentPlayerHand = hand;
        }
        else if (type == "Enemy")
        {
            currentEnemyHand = hand;
        }
    }
    string ReturnResult(int playerPlay, int enemyPlay)
    {
        if (playerPlay == enemyPlay)
        {
            draw = true;
            StartCoroutine(ResetAfterDraw());
            return "Draw";
        }
        else if ((playerPlay == 0 && enemyPlay == 2) || (playerPlay == 1 && enemyPlay == 0) || (playerPlay == 2 && enemyPlay == 1))
        {
            hasWon = true;
            canShoot = true;
            return "Win";
        }
        else
        {
            hasWon = false;
            canShoot = false;
            return "Loss";
        }
    }
    public void DisplayResult()
    {
        result = ReturnResult(currentPlayerHand, currentEnemyHand);
        Debug.Log("Result = " + result + " PlayerHand = " + currentPlayerHand + " EnemyHand = " + currentEnemyHand);
    }

    public void StartNew()
    {
        gameRunning = true;
        moving = false;
        hasWon = false;
        draw = false;
        shootingActive = false;
        canShoot = false;
        recieveInput = true;
    }
    
    public void StartShootingSequence()
    {
        recieveInput = false;
        gameRunning = false;
        shootingActive = true;
        moving = true;
    }
    public void StopShootingSequence()
    {
        StartNew();
    }
    IEnumerator ResetAfterDraw()
    {
        yield return new WaitForSeconds(2f);
        StartNew();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && recieveInput)
        {
            DisplayResult();
            StartShootingSequence();
        }
    }

}