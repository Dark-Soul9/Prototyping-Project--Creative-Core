using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandScriptAI : MonoBehaviour
{
    private Animator anim;
    private float shootDelay;
    private Transform pos; //enemy position
    private string tempResult;


    private void Start()
    {
        anim = GetComponent<Animator>();
        shootDelay = 3f;
        pos = GetComponent<Transform>();
    }
    private void Update()
    {
        Manager.Instance.GetEnemyPlay(Random.Range(0, 3));
        ShootingSession();
    }
    IEnumerator Movement()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        anim.SetTrigger("Start");

    }
    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(pos.position, Vector3.back, out hitInfo, 8f))
        {
            Debug.Log("shoot " + hitInfo.transform.name);
        }
        Manager.Instance.StopShootingSession();
    }
    void ShootingSession()
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
            else if (Manager.Instance.result == "Win") //moves only if lost the round and currently not moving.
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    return;
                }
                StartCoroutine(Movement());
            }
            else //shoots only if won the round.
            {
                Manager.Instance.StartShootingSession();
                if (shootDelay > Time.deltaTime)
                {
                    shootDelay -= Time.deltaTime;
                    return;
                }
                Shoot();
                shootDelay = 3f;
            }
        }
    }
    IEnumerator ResetOnDraw()
    {
        yield return new WaitForSeconds(3f);
        Manager.Instance.StopShootingSession();
    }
}