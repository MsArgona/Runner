using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    [SerializeField]
    private Vector2 directional;
    [SerializeField]
    private float forceMagnitude = 10f;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        other.GetComponent<PlayerController>().ForcedJump(directional, forceMagnitude);
    //    }
    //}
}
