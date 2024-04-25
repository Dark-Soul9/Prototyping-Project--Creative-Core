using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private Animator anim;
    private Transform pos; //player position
    private int playerInput;
    private void Start()
    {
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
    }
    private void Update()
    {
        Manager.Instance.GetPlayerPlay(playerInput);
        shootingSession();
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
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        return;
                    }
                    anim.SetTrigger("Start");
                }
            }
            else //shoots only if won the round.
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    Manager.Instance.StartShootingSession();
                    Shoot();
                }
            }
        }
    }
    IEnumerator ResetOnDraw()
    {
        yield return new WaitForSeconds(3f);
        Manager.Instance.StopShootingSession();
    }
    
}