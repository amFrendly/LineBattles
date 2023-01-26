using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float mouseMoveSensitivity = 1;

    [SerializeField]
    Transform lineCommander;

    [SerializeField]
    new Camera camera;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = Input.mouseScrollDelta;
        if (mouseDelta.y != 0)
        {
            transform.position += new Vector3(0, mouseDelta.y + mouseDelta.x, 0);
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            lineCommander.position = GetMousePosition();

        }

        DragMove();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos += new Vector3(0, 0, transform.position.y);
        return camera.ScreenToWorldPoint(mousePos);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GetMousePosition(), 1);
    }


    Vector3 startDrag;
    Vector3 dragChange;
    Vector3 startPos;
    private void DragMove()
    {
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            startPos = new Vector3(transform.position.x, 0, transform.position.z);
            startDrag = new Vector3(mousePos.x, 0, mousePos.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            dragChange = new Vector3(mousePos.x, 0, mousePos.y);
            Vector3 diference = (startDrag - dragChange);
            Vector3 keepY = new Vector3(0, transform.position.y, 0);
            transform.position = startPos + keepY + diference * mouseMoveSensitivity;
        }
    }
}
