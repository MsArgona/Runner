using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player characteristics")]
    [SerializeField] float startSpeed = 5.0f;
    [SerializeField] float JumpForce = 6.0f;
    [SerializeField] Vector2 direction;  //понадобится, если нужно будет поменять направление движения перса
    //public ParticleSystem dyingEffect;
    [SerializeField]  float dyingTime = 0.7f;
    private float currSpeed;

    [Header("Effects")]
    [SerializeField] SpriteRenderer[] baseSprites;
    [SerializeField] ParticleSystem[] baseParticles;
    [SerializeField] ParticleSystem[] deadEffectParticles;
    [SerializeField] GameObject objWithRunEffects;

    public float CurrSpeed
    {
        get { return currSpeed; }
        set { currSpeed = value; }
    }

    [Header("Ground")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.2f;
    private bool isGrounded = false;

    new private Rigidbody2D rigidbody;

    private GameManager gameManager;

    private Vector3 startPos = new Vector3(0, 0, 0);

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        CurrSpeed = startSpeed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currSpeed = startSpeed;
    }

    void Update()
    {
        GroundCheck();
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    public void ChangePos(Transform newPos)
    {
        transform.position = newPos.position;
    }

    private void Move()
    {
        transform.Translate(direction * currSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (isGrounded)
        {

        }
    }

    public void Dying()
    {
        //поэтому сделай не через частицы, а анимацию спрайтов! Так можно будет менять состояния и ставить тригеры
        //if (baseParticles.Length > 0)
        //{
        //    foreach (var curEffect in baseParticles)
        //    {
        //        curEffect.Pause();  //частицы замирают
        //    }
        //}

        if (objWithRunEffects != null)
        {
            objWithRunEffects.SetActive(false);
        }

        if (baseSprites.Length > 0)
        {
            foreach (var curSprite in baseSprites)
            {
                curSprite.enabled = false;
            }          
        }

        if (deadEffectParticles.Length > 0)
        {
            foreach (var curEffect in deadEffectParticles)
            {
               curEffect.Play();
            }
        }
        CurrSpeed = 0f;
        StartCoroutine(DyingCo());
       // this.gameObject.transform.position = startPos;
       // gameManager.Replay();
    }

    private IEnumerator DyingCo()
    {
        yield return new WaitForSeconds(dyingTime);
        gameManager.PlayerIsDead();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
