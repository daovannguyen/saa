using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCompoment2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;
    public Collider2D col;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
}
