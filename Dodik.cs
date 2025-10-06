using UnityEngine;

public class Dodik : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 3;
    [SerializeField] private float jump = 0.25f;
    [SerializeField] private int coins = 0;
    private bool isGround = false;

    private float lastH = 1f;
    private float curh = 1f;

    private Rigidbody2D rgb;
    private SpriteRenderer sprt;
    private Animator ani;
    private Transform tran;

    public static Dodik Instance { get; set; }

    public enum States
    {
        idle,
        jumpup,
        jumpdown,
        run
    }
    private States St
    {
        get { return (States)ani.GetInteger("stats"); }
        set { ani.SetInteger("stats", (int)value); }
    }
    private void Awake()
    {
        Instance = this;
        tran = GetComponent<Transform>();
        rgb = GetComponent<Rigidbody2D>();
        sprt = GetComponentInChildren<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        lastH = curh;
        curh = tran.position.y;
        Checking();
    }
    private void Update()
    {
        if (isGround) St = States.idle;
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButton("Jump") && isGround)
            Jump();
    }
    private void Run()
    {
        if (isGround) St = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprt.flipX = dir.x < 0.0f;
    }
    private void Jump()
    {
        rgb.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }
    private void Checking()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGround = coll.Length > 1;

        if (!isGround)
        {
            if (curh < lastH) St = States.jumpdown;
            else St = States.jumpup;
        }
    }
    public override void GetDam()
    {
        lives--;
    }
    public void CoinTake()
    {
        coins++;
    }
}
