using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Item : GetCompoment3D
{
    public int id;
    private ListTiles listTiles;
    private Color color;
    private Sequence sequence;


    // dùng để cho phép chọn hay không
    private bool seleted = true;

    public override void Awake()
    {
        base.Awake();
        listTiles = ListTiles.Instance;
        color = renderer.material.color;
        sequence = DOTween.Sequence();
    }
    public virtual void MoveToPointDotween(Vector3 position, Vector3 rotation, Vector3 scale, float time, Action<bool> onComplete = null)
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(position, time))
            .Join(transform.DORotate(rotation, time).SetRelative())
            .Join(transform.DOScale(scale, time))
            .OnComplete(()=> {
                onComplete(true);
            }
        );
    }

    private void OnMouseOver()
    {
        if (seleted)
        {
            renderer.material.color = Color.red;
        }
    }
    private void OnMouseDown()
    {
        if (seleted)
        {
            listTiles.AddItem(this);
        }
    }
    private void OnMouseExit()
    {
        renderer.material.color = color;
    }
    public void DestroyItem(Vector3 pos)
    {
        Vector3 distance = transform.position - pos;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + distance * 0.2f, Constrain.ID_time))
            .Append(transform.DOMove(pos, Constrain.ID_time))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(Constrain.TAG_DONTCONTROLITEM))
        {
            seleted = false;
        }
    }
}
