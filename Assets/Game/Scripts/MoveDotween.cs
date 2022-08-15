using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveDotween : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public GameObject model;
    Collider col;
    Vector3 addY = Vector3.zero;
    private void Awake()
    {
        col = model.GetComponent<Collider>();
        addY = new Vector3(0, (float)(col.bounds.size.y * 0.5), 0) ;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveDotweenToPoint(GetLerp(target1.position), new Vector3(0, 0, 0), new Vector3(1,1,1));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            MoveDotweenToPoint(GetLerp(target2.position), new Vector3(180, 0, 0), new Vector3(0.5f,0.5f,0.5f));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }

    private Vector3 GetLerp(Vector3 position)
    {
        return Vector3.Lerp(position, Camera.main.transform.position, 0.3f);
    }
    private void MoveDotweenToPoint(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        int timer = 2;
        model.transform.DOMove(position, timer);
        model.transform.DORotate(rotation, timer);
        model.transform.DOScale(scale, timer);
    }

}
