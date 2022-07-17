using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public bool hideEffect = false;
    public bool showEffect = false;
    public float speed = 1f;

    public int scoreValue = 0;
    public ParticleSystem[] otherEffect;
    public ParticleSystem[] destroyEffects;
    public AudioClip audioClip;
    //public AnimationClip animClip;
    public float dyingTime = 1.6f;

    private SpriteRenderer sprite;
    private Animator animator;
    private GameManager gameManager;

    private bool isTouched = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //if (gameManager.isPlayerDied)
        //{
        //    Restart();
        //    Debug.Log("You died!");
        //    gameManager.isPlayerDied = false;
        //}

        if (isTouched)
        {
            if (hideEffect && sprite != null)
            {
                var color = sprite.color;
                color.a -= speed * Time.deltaTime;
                color.a = Mathf.Clamp(color.a, 0, 1);
                sprite.color = color;
            }
            if (showEffect && sprite != null)
            {
                var color = sprite.color;
                color.a += speed * Time.deltaTime;
                color.a = Mathf.Clamp(color.a, 0, 1);
                sprite.color = color;
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (this.CompareTag("Star"))
            {
                gameManager.Score += scoreValue;
            }

            isTouched = true;

            if (animator != null)
            {
                //animator.Play("TouchEffect");
                animator.enabled = true;
            }
            if (audioClip != null)
            {

            }
            if (otherEffect.Length != 0)
            {
                foreach (ParticleSystem currEff in otherEffect)
                {
                    currEff.Play();
                }
            }
            if (destroyEffects.Length != 0)
            {
                foreach (var destroyEffect in destroyEffects)
                {
                    destroyEffect.Play();
                }
            }

            //if (sprite != null)
            // {
            //     HideSpriteCo(hideSpriteTime);
            // }

            Die(dyingTime);
        }
    }

    private void Restart()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);

        isTouched = false;

        if (animator != null)
        {
            //animator.Play("TouchEffect");
            animator.enabled = false;
        }
        if (audioClip != null)
        {

        }
        if (otherEffect.Length != 0)
        {
            foreach (ParticleSystem currEff in otherEffect)
            {
                currEff.Stop();
            }
        }
        if (destroyEffects.Length != 0)
        {
            foreach (var destroyEffect in destroyEffects)
            {
                destroyEffect.Stop();
            }
        }
    }

    //private IEnumerable HideSpriteCo(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    sprite.enabled = false;
    //}

    //не все объекты уничтожаются после касания
    protected void Die(float dyingTime)
    {
        Destroy(gameObject, dyingTime);
    }
}
