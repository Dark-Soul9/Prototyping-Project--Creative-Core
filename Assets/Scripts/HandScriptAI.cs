using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScriptAI : MonoBehaviour
{
    private Animator anim;
    private bool win;
    private float shootDelay;
    private bool startShooting;
    private Transform pos; //enemy position


    private void Start()
    {
        anim = GetComponent<Animator>();
        win = true ;
        shootDelay = 3f;
        startShooting = true;
        pos = GetComponent<Transform>();
    }
    private void Update()
    {
        if(startShooting) //when rock paper scissors round is done do this.
        {
            if (!win && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) //moves only if lost the round and currently not moving.
            {
                StartCoroutine(Movement());
            }
            else //shoots only if won the round.
            {
                if (shootDelay > Time.deltaTime)
                {
                    shootDelay -= Time.deltaTime;
                    return;
                }
                Shoot();
                shootDelay = 3f;
                startShooting = false;
            }
        }
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
    }
}