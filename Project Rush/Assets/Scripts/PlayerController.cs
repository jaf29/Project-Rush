using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Variables ---------------------------------
    public float speed = 45f;
    public float jumpForce = 20f;
    public float timeBtw = 1f;
    public bool isGrounded;
    public bool wasHit;
    public int startHealth = 10;
    public int currentHealth;
    public int attackDamage = 1;

    private Rigidbody rgby;
    private Vector3 mainMovement;
    private Vector3 autoMovement;
    private Vector3 battleMovement;
    private float horizontal;
    private float vertical;
    private float hitTimer;
    //Variables ---------------------------------

    //Initialize ---------------------------------
    void Start () {
        rgby = GetComponent<Rigidbody>();
        currentHealth = startHealth;
        wasHit = false;
        GameManager.instance.SetPlayerHealthText();
	}
    //Initialize ---------------------------------

    //Update ---------------------------------
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        mainMovement = new Vector3(horizontal, 0, 0) * speed;
        hitTimer += Time.deltaTime;

        rgby.MovePosition(transform.position + mainMovement);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rgby.AddForce(new Vector3(0, 2.5f, 0) * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (GameManager.instance.gameStart == true)
        {
            SelfMove();
        }
        else if(GameManager.instance.battleStart == true)
        {
            BattleMove();
        }
        else
        {
            StopMove();
        }

        if (currentHealth <= 0)
        {
            GameManager.instance.PlayerDeath();
            currentHealth = startHealth;
            GameManager.instance.SetPlayerHealthText();
        }
    }
    //Update ---------------------------------

    //Collision Checks ---------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Object"))
        {
            wasHit = true;
            if (hitTimer >= timeBtw && wasHit)
            {
                GameManager.instance.Damaged();
                rgby.AddForce(new Vector3(0, 0, -90f), ForceMode.Impulse);
                wasHit = false;
                hitTimer = 0;
            }
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            GameManager.instance.AutoRespawn();
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.instance.PlayerDeath();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.PlayerComplete();
        }

        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Lance") || collision.gameObject.CompareTag("Axe"))
        {
            if (collision.gameObject.CompareTag("Sword"))
            {
                attackDamage = 10;
                collision.gameObject.SetActive(false);
                //GameManager.instance.Points();
                GameManager.instance.SwordSwap();
            }

            if (collision.gameObject.CompareTag("Lance"))
            {
                attackDamage = 5;
                collision.gameObject.SetActive(false);
                //GameManager.instance.Points();
                GameManager.instance.LanceSwap();
            }

            if (collision.gameObject.CompareTag("Axe"))
            {
                attackDamage = 15;
                collision.gameObject.SetActive(false);
                //GameManager.instance.Points();
                GameManager.instance.AxeSwap();
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.BattleDamage();
            GameManager.instance.BattleReset();
        }
    }
    //Collision Checks ---------------------------------

    //Movement Methods ---------------------------------
    private void SelfMove()
    {
        autoMovement = new Vector3(horizontal, 0, 1) * speed * Time.deltaTime;
        rgby.MovePosition(transform.position + autoMovement);
    }

    private void StopMove()
    {
        mainMovement = new Vector3(0, 0, 0) * speed * Time.deltaTime;
        rgby.MovePosition(transform.position + mainMovement);
    }

    private void BattleMove()
    {
        battleMovement = new Vector3(horizontal / 1.5f, 0, vertical / 1.5f) * speed * Time.deltaTime;
        rgby.MovePosition(transform.position + battleMovement);
    }
    //Movement Methods ---------------------------------
}
