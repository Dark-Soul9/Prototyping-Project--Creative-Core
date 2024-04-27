using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandScriptAI : MonoBehaviour
{
    private Animator anim;
    private Transform pos; //enemy position
    public int handCount;
    private HandScript handScript;
    public int bluff, dodge;
    public bool move;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
        handScript = GameObject.Find("Player").GetComponent<HandScript>();
        handCount = 2;
    }
    private void Update()
    {
        if(Manager.Instance.gameOver)
        {
            return;
        }
        Manager.Instance.GetEnemyPlay(Random.Range(0, 3));
        if(Manager.Instance.result == "Draw")
        {
            StartCoroutine(ResetOnDraw());
        }
    }
    //IEnumerator Movement()
    //{
    //    yield return new WaitForSeconds(Random.Range(0f, 1f));
    //    anim.SetTrigger("Start");
    //}
    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(pos.position, Vector3.back, out hitInfo, 8f))
        {
            Debug.Log("shoot " + hitInfo.transform.name);
            if(!handScript.dodge && bluff == 0 || (handScript.dodge && bluff == 1))
            {
                handScript.handCount--;
            }
            if(handScript.handCount < 1)
            {
                Manager.Instance.GameOver("Game Over");
            }
        }
        Manager.Instance.StopShootingSession();
    }
    public void ShootingSession()
    {
        if (Manager.Instance.shootSession) //when rock paper scissors round is done do this.
        {
            if(Manager.Instance.result == null)
            {
                return;
            }
            if(Manager.Instance.result == "Draw")
            {
                return;
            }
            else if (Manager.Instance.result == "Win") //moves only if lost the round and currently not moving.
            {
                if (dodge == 0)
                {
                    return;
                }
                anim.SetTrigger("Start");
            }
            else //shoots only if won the round.
            {
                Shoot();
            }
        }
    }
    IEnumerator ResetOnDraw()
    {
        yield return new WaitForSeconds(3f);
        Manager.Instance.StopShootingSession();
    }
    public void SetDifficulty(int difficulty)
    {
        switch(difficulty)
        {
            case 0:
                handCount = 2;
                break;
            case 1:
                handCount = 3;
                break;
            case 2:
                handCount = 5;
                break;
            default:
                break;
        }
    }
}