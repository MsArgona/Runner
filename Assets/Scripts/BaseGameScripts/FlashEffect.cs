using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private GameObject spriteToChange;
    [SerializeField] private float flashTime = 0.3f;
    [SerializeField] private int activateCount = 2;
    private int count;
    private Color newColor;
    private Color startColor;

    void Start()
    {
        startColor = spriteToChange.GetComponent<SpriteRenderer>().color;
        newColor = new Color(58f, 58f, 58f, 255f);
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeColor(newColor);
            StartCoroutine(FlashGo());
        }
    }

    //запускаются две сразу!
    private IEnumerator FlashGo()
    {
        yield return new WaitForSeconds(flashTime);
        ChangeColor(startColor);
        count++;

        if (count < activateCount)
            StartCoroutine(FlashGo());

    }

    private void ChangeColor(Color color)
    {
        spriteToChange.GetComponent<SpriteRenderer>().color = color;
    }
}
