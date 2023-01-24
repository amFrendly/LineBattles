using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
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

    [SerializeField]
    float turnSpeed = 1.0f;

    LineControl control;

    Animator animator;

    bool running = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        control = transform.parent.parent.GetChild(0).GetComponent<LineControl>();
    }
    void Update()
    {
        linePosition = control.GetPosition(index);
        if (Vector3.Distance(transform.position, linePosition) < stopWithin)
        {
            TurnTowards(control.transform.forward, turnSpeed);
            animator.SetBool("isRunning", false);
            return;
        }
        animator.SetBool("isRunning", true);;

        Vector3 direction = linePosition - transform.position;
        direction.Normalize();
        TurnTowards(direction, turnSpeed);

        transform.position += direction * speed * Time.deltaTime;
    }

    void TurnTowards(Vector3 targetRotation, float turnSpeed)
    {
        Quaternion rotationGoal = Quaternion.LookRotation(targetRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, turnSpeed);
    }
}
