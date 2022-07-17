using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRain : MonoBehaviour
{
    [SerializeField] private GameObject rainSprite;
    [SerializeField] private GameObject skySprite;
    [SerializeField] private GameObject rainObg;
    private ParticleSystem rainParticle;

    void Start()
    {
        rainParticle = rainObg.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            skySprite.SetActive(true);
            rainSprite.SetActive(false);
            rainParticle.Stop();
            rainObg.SetActive(false);

        }
    }
}
