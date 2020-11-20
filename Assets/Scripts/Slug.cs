using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float runLength = 5f;
    private Collider2D coll;

    // Start is called before the first frame update

    private bool facingLeft = true;
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();

    }

    void Update()
    {
        Move();

    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1, 1);
            }
            if (transform.position.x > leftCap)
            {
                if (coll.IsTouchingLayers())
                {
                    rb.velocity = new Vector2(-runLength, rb.velocity.y);

                }

            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {

                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (coll.IsTouchingLayers())
                {
                    rb.velocity = new Vector2(runLength, rb.velocity.y);

                }


            }
            else
            {
                facingLeft = true;
            }

        }
    }
}

    // Update is called once per frame
    
