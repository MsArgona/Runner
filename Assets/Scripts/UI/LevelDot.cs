using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDot : MonoBehaviour
{
    [SerializeField] private Sprite dotIsActive;
    [SerializeField] private Sprite dotIsNotActive;

    private SpriteRenderer spriteRenderer;

    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;

            if (_active)
                spriteRenderer.sprite = dotIsActive;
            else
                spriteRenderer.sprite = dotIsNotActive;
        }
    }

    private bool _active;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
