using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnim : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = this.transform.GetComponent<Animator>();//"this" is important
    }

    public  void AttackAnim()
    {
        anim.SetBool("attack", true);
    }


    public  void IdleAnim()
    {
        anim.SetBool("idle", true);
        anim.SetBool("attack", false);
    }

    public  void WalkAnim()
    {
        
        anim.SetBool("attack", false);
    }

    public  void DeadAnim()
    {
        anim.SetBool("isDead", true);
    }
}
