using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    private AudioSource death;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Trigger()
    {
        anim.SetTrigger("death");
       


    }


    public void Death()
    {

        Destroy(this.gameObject,0.3f);
    }
}
