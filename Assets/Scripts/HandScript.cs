using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private Animator anim;
    private bool win;
    private bool startShooting;
    private Transform pos; //player position
    private void Start()
    {
        anim = GetComponent<Animator>();
        win = true;
        startShooting = true;
        pos = GetComponent<Transform>();
    }
    private void Update()
    {
        if (startShooting) //when rock paper scissors round is done do this.
        {
            if (!win && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) //moves only if lost the round and currently not moving.
            {
                if(Input.GetKeyDown(KeyCode.W))
                {
                    anim.SetTrigger("Start");
                }
            }
            else //shoots only if won the round.
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                    startShooting = false;
                }
            }
        }
    }
    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(pos.position, Vector3.forward, out hitInfo, 8f))
        {
            Debug.Log("shoot " + hitInfo.transform.name);
        }
    }
}