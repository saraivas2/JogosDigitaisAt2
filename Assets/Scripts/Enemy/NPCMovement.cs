using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject EnemyFire;
    [SerializeField] private GameObject Espada;
    private float vel = 2f;
    [SerializeField] private bool move,time = false;
    [SerializeField] private float vida = 100f;
    private Animator animator;
    private Rigidbody2D rb;
    private int walkEHash = Animator.StringToHash("walkE");
    private int dieEHash = Animator.StringToHash("dieE");
    private int jumpEHash = Animator.StringToHash("jumpE");
    private int attackEHash = Animator.StringToHash("attackE");
    private int idleEHash = Animator.StringToHash("idle");
    private int count = 500;
    private int jumpForce = 15;
    private int jump = 2;
    [SerializeField] private bool canAttack = false;
    private float attackCooldown = 1.5f;
    private float moveRange = 8f;
    [SerializeField] private float attackTimer = 0f;
    [SerializeField] private bool jumpBool = false;
    [SerializeField] private float distance;
    [SerializeField] private bool damage = false;
    [SerializeField] private float timeNew = 0.5f;


    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        Fire = GetComponent<GameObject>();
        Espada = GetComponent<GameObject>();
    }
    private void Update()
    {
        if (jump == 0)
        {
            count--;
            count = count_jump(count);
        }
        if (jumpBool & !canAttack)
        {
            JumpForce();
            MoveNPC();
        }

        if (damage)
        {
            vida = -10;
            damage = false;
        }

        distance = CalcDistance();

        if (distance > attackCooldown & distance < moveRange)
        {
            move = true;

            if (move & !jumpBool & !canAttack)
            {
                MoveNPC();
                animator.SetBool(jumpEHash, false);
                animator.SetBool(walkEHash, move);
                animator.SetBool(attackEHash, false);
                animator.SetBool(idleEHash, false);
            }
        }
        else
        {
            move = false;
            animator.SetBool(jumpEHash, false);
            animator.SetBool(walkEHash, move);
            animator.SetBool(attackEHash, false);
            animator.SetBool(idleEHash, true);
        }

        if (distance < attackCooldown)
        {
            canAttack = true;
            if (canAttack)
            {
                AttackNPC();
                time = true;

            }
        }
        else
        {
            canAttack = false;
        }
        if (time)
        {
            timeNew = ObjectFire(timeNew);
        }
        
    }


    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("espada"))
        {
            damage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("floor"))
        {
            jumpBool = true;
            animator.SetBool(jumpEHash, jumpBool);
            animator.SetBool(walkEHash, false);
            animator.SetBool(attackEHash, false);
            animator.SetBool(idleEHash, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jumpBool = false;
            animator.SetBool(jumpEHash, jumpBool);
            animator.SetBool(walkEHash, false);
            animator.SetBool(attackEHash, false);
            animator.SetBool(idleEHash, true);
        }
    }
    private void JumpForce()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
    }

    private int count_jump(int count)
    {
        if (count <= 0)
        {
            count = 500;
            jump = 2;
        }
        return count;
    }


    private void dieNPC(int vida)
    {
        if (vida <= 0)
        {
            animator.SetBool(dieEHash, true);
            animator.SetBool(jumpEHash, false);
            animator.SetBool(walkEHash, false);
            animator.SetBool(attackEHash, false);
            move = false;
            return;
        }
    }
    public void MoveNPC()
    {
        
        if (transform.position.x < Player.transform.position.x)
        {
            Enemy.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Enemy.transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            
        }
        else if (transform.position.x > Player.transform.position.x)
        {
            Enemy.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            Enemy.transform.Translate(new Vector2(vel * Time.deltaTime, 0));
        }
    }
    

    private float CalcDistance()
    {
        distance = Vector2.Distance(Enemy.transform.position, Player.transform.position);
        return distance;
    }
    private void AttackNPC()
    {
        if (canAttack)
        {
            attackTimer = attackTimer - Time.deltaTime;
            if (attackTimer <= 0)
            {
                Instantiate(Fire, EnemyFire.transform.position, EnemyFire.transform.rotation);
                animator.SetBool(jumpEHash, false);
                animator.SetBool(walkEHash, false);
                animator.SetBool(attackEHash, canAttack);
                animator.SetBool(idleEHash, false);
                attackTimer = attackCooldown;
            }
        }
    }


    private float ObjectFire(float timeNew)
    {
        timeNew = timeNew - Time.deltaTime;
        if (timeNew <= 0)
        {
            Destroy(Fire);
            timeNew = 0.5f;
            time=false;
        }
        return timeNew;
    }
}
