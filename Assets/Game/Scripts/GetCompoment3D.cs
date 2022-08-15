using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCompoment3D : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public Renderer renderer;
    public Collider col;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }
}