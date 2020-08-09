﻿using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private Sprite healthy, destroyed;

    private SpriteRenderer spriteRenderer;

    private bool shielded = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector2 GetPosition() => transform.position;

    public void BlowUp()
    {
        if(shielded)
        {
            shielded = false;
            return;
        }

        spriteRenderer.sprite = destroyed;
        // do more stuff
        BaseManager.Inst.BaseDestroyed(this);
    }

    public void Restore()
    {
        spriteRenderer.sprite = healthy;
        // maybe restore particle effect
    }
}