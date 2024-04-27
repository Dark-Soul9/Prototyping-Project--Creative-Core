using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private Animator anim;
    private Transform pos; //player position
    private int playerInput;
    public int handCount;
    private HandScriptAI handScriptAI;
    private bool bluff;
    public bool dodge;
    private bool shootingDone;
    private void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
        handScriptAI = GameObject.Find("Enemy").GetComponent<HandScriptAI>();
        handCount = 2;
    }
    private void Update()
    {
        if (Manager.Instance.gameOver)
        {
            return;
        }
        //Manager.Instance.GetPlayerPlay(playerInput);
        //shootingSession();
    }
    public void GetPlayerInput(int input)
    {
        playerInput = input;
    }
    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(pos.position, Vector3.forward, out hitInfo, 8f))
        {
            Debug.Log("shoot " + hitInfo.transform.name);
            if (handScriptAI.dodge == 0 || (handScriptAI.dodge == 1 && bluff))
            {
                handScriptAI.handCount--;
            }
            if (handScriptAI.handCount < 1)
            {
                Manager.Instance.GameOver("Congatulations! You Won");
            }
        }
        Manager.Instance.StopShootingSession();
    }
    void shootingSession()
    {
        if (Manager.Instance.shootSession) //when rock paper scissors round is done do this.
        {
            if(Manager.Instance.result == null)
            {
                return;
            }
            if(Manager.Instance.result == "Draw")
            {
                StartCoroutine(ResetOnDraw());
                return;
            }
            else if (Manager.Instance.result == "Loss") //moves only if lost the round and currently not moving.
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || !dodge)
                {
                    return;
                }
                anim.SetTrigger("Start");
            }
            //else //shoots only if won the round.
            //{
            //    if(!shootingDone)
            //    {
            //        if (bluff)
            //        {
            //            //Manager.Instance.StopShootingSession();
            //            return;
            //        }
            //        Shoot();
            //        bluff = true;
            //        shootingDone = true;
            //    }
            //}
        }
    }
    IEnumerator ResetOnDraw()
    {
        yield return new WaitForSeconds(3f);
        Manager.Instance.StopShootingSession();
    }
    public void Bluff(bool shoot)
    {
        Manager.Instance.StartShootingSession();
        if(!shoot)
        {
            bluff = true;
            Shoot();
        }
        else
        {
            bluff = false;
            Shoot();
        }
        handScriptAI.move = true;
        handScriptAI.dodge = Random.Range(0, 2);
        shootingSession();
        handScriptAI.ShootingSession();
        //Manager.Instance.StopShootingSession();
    }
    public void Dodge(bool move)
    {
        Manager.Instance.StartShootingSession();
        if(move)
        {
            dodge = true;
        }
        else
        {
            dodge = false;
        }
        handScriptAI.bluff = Random.Range(0, 2);
        shootingSession();
        handScriptAI.ShootingSession();
        //Manager.Instance.StopShootingSession();
    }
    
}