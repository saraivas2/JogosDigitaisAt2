using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class player : MonoBehaviour
{

    private int vel = 4;
    private Vector3 posicao;
    private Rigidbody2D rb;
    [SerializeField] private int jump = 2;
    private int forca = 90;
    private bool move, espada = false;
    [SerializeField] private bool bool_jump, bool_jumpUp = false;
    [SerializeField] private int count = 500;
    private Animator animator;
    private int runPlayerHash = Animator.StringToHash("runPlayer");
    private int attack1Hash = Animator.StringToHash("Attack1");
    private int attack2Hash = Animator.StringToHash("Attack2");
    private int attack3Hash = Animator.StringToHash("Attack3");
    private int deathHash = Animator.StringToHash("death");
    private int starRunHash = Animator.StringToHash("startRun");
    private int jumpUpHash = Animator.StringToHash("jumpUp");
    private int jumpDownHash = Animator.StringToHash("jumpDown");
    [SerializeField] private int vida = 100;
    private float valorUp = 0;
    [SerializeField] private bool damage = false;
    [SerializeField] private GameObject Espada;
    [SerializeField] private GameObject Fire;
    [SerializeField] private Collider2D isAttack;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask EnemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
        Espada = GetComponent<GameObject>();
        Fire = GetComponent<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("fire"))
        {
            if (collision.gameObject.CompareTag("fire"))
            {
                damage = true;
                Destroy(collision.gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jump = 2;
            bool_jump = false;
        }
        
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        bool_jump = true;
    }

    // Update is called once per frame
    void Update()
    {
        count--;
        comando();
        count = count_jump(count);
        float altura = rb.velocity.y;
        if (bool_jump)
        {
            bool_jumpUp = jumpUp(altura);
        }

        if (damage)
        {
            vida = -10;
            damage = false;
            
        }

        animator.SetBool(runPlayerHash, move && !espada && !bool_jump);
        animator.SetBool(attack1Hash, espada && !bool_jump);
        animator.SetBool(jumpUpHash, bool_jump && !espada && bool_jumpUp);
        animator.SetBool(jumpDownHash, bool_jump && !espada && !bool_jumpUp);

    }

    
    private int count_jump(int count) 
    { 
        if (count <= 0)
        {
            count = 500;
            if (jump == 0) 
            { 
                jump = 2; 
            }
            return count;
        }
        return count;
    }


    private bool jumpUp(float altura)
    {
        if (altura > valorUp)
        {
            valorUp = altura;
            return true;
        }
        else
        {
            valorUp = 0;
            return false;
        }
    }
    void comando()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            move = true;
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            move = true;
        }
        else
        {
            move = false;
        }

        if (jump > 0 & jump <=2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump--;
                rb.AddForce(new Vector2(0, 2*forca),ForceMode2D.Force);
                
            }
        }
       

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Collider2D[] isAttack = Physics2D.OverlapCircleAll(Espada.transform.position, radius, EnemyLayer);
            espada = true;

        }
        else
        {
            espada = false;
        }
    }    
}
