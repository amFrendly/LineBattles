using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierBehaviour : MonoBehaviour
{
    [NonSerialized]
    public Vector3 linePosition;

    [SerializeField]
    float stopWithin;

    [SerializeField]
    public int index;

    [SerializeField]
    float speed;

    LineControl control;

    private void Start()
    {
        control = transform.parent.parent.GetChild(0).GetComponent<LineControl>();
    }
    void Update()
    {
        linePosition = control.GetPosition(index);
        if (Vector3.Distance(transform.position, linePosition) < stopWithin)
        {
            return;
        }

        Vector3 direction = linePosition - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
}
