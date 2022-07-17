using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    [SerializeField] private float speed = 8f; //как у игрока
    [SerializeField] Vector2 direction;
    new private Rigidbody2D rigidbody;
    private bool canFly = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canFly)
            Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFly = true;
        }
    }
}
