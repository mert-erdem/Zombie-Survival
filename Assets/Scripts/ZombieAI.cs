using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    //This will be the zombie's speed. Adjust as necessary.
    private float speed = 2.0f;

    public int health;

    ZombieAnim anim;

    private bool alive = true;

    void Start()
    {
        health = 100;

        //At the start of the game, the zombies will find the gameobject called wayPoint.
        player = GameObject.Find("Player");

        anim = this.transform.GetComponent<ZombieAnim>();
    }

    void FixedUpdate()
    {
        if(alive==true)
        {
            Tracker();
            Walk();

            if ((playerPos - this.transform.position).magnitude <= 3)//this zombie is very close to player
            {
                Attack();
            }
        }
        Dead();
    }

    void Tracker()
    {
        playerPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);

        //Here, the zombie's will follow the waypoint.
        this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed * Time.deltaTime);
        this.transform.LookAt(playerPos);
    }

    void Attack()
    {
        PlayerController.HEALTH -= 1;
        anim.AttackAnim();       
    }

    
    void Walk()
    {
        if ((playerPos - this.transform.position).magnitude >= 3)
        {
            anim.WalkAnim();
        }
    }

    void Dead()
    {
        if(health<=0)
        {
            anim.DeadAnim();
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        alive = false;
        this.transform.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(4f);

        ZombieGenerator.aliveZombies--;
        Destroy(gameObject);
    }

    
}
