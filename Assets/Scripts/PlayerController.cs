using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, skiping,jumping,falling,hurt}
    private State state = State.idle;
    private Collider2D coll;
    private AudioSource footsteep;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] Text carrots;
    [SerializeField] private static int carrotCount = 0;

    [SerializeField]private int maxHealth = 100;
    [SerializeField]private int currentHealth;
    [SerializeField] private Health healthBar;








    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        footsteep = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        VelocityState();
        anim.SetInteger("state", (int)state);
        carrots.text = carrotCount.ToString();



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            FindObjectOfType<AudioMangaer>().Play("carrot");

            Destroy(collision.gameObject);
            carrotCount++;
            carrots.text = carrotCount.ToString();

        }
        else if (collision.tag == "PowerUp")
        {
            FindObjectOfType<AudioMangaer>().Play("star");

            Destroy(collision.gameObject);
            jumpForce = 19f;
            speed = 10f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
        else if (collision.tag == "PowerDown")
        {
            FindObjectOfType<AudioMangaer>().Play("mushroom");

            Destroy(collision.gameObject);
            jumpForce = 12f;
            speed = 6f;
            GetComponent<SpriteRenderer>().color = Color.blue;
            StartCoroutine(ResetPower());
        }
        else if (collision.tag == "Fall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            carrotCount = 0;


        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.Trigger();
                enemy.Death();
                FindObjectOfType<AudioMangaer>().Play("enemydeath");
                Jump();
            }
            else
            {
                state = State.hurt;
                FindObjectOfType<AudioMangaer>().Play("hurt");


                currentHealth -= 30;
                healthBar.SetHealth(currentHealth);
                if (currentHealth <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    carrotCount = 0;

                }
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);

                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);

                }
            }
        }

    }

    private void Movement()
    {
        float hDirection = Input.GetAxisRaw("Horizontal");


        if (hDirection < 0)
        {

            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (hDirection > 0)
        {

            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            FindObjectOfType<AudioMangaer>().Play("jump");

            Jump();
        }
    }
    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }

        }

        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }

        }

        else if (state == State.hurt)
        {

            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }




        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            state = State.skiping;

        }
        else
        {
            state = State.idle;
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;

    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(10);
        jumpForce = 16f;
        speed = 6f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

 
    private void Footsteep()
    {
        footsteep.Play();
    }
    public void setCount()
    {
        carrotCount = 0;
    }
}
