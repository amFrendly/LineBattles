using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Drawing;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LineControl : MonoBehaviour
{
    [SerializeField] GameObject soldier;
    [SerializeField] float spacing;
    [SerializeField] float rotation;

    public int zAmount;
    public int xAmount;

    public void DeleteLine()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    public void CreateLine()
    {
        Vector3 offset = TopLeft();
        Vector3 soldierPos = offset;
        for (int x = 0; x < zAmount; x++)
        {
            for (int z = 0; z < xAmount; z++)
            {
                GameObject newSoldier = Instantiate(soldier, transform);
                newSoldier.transform.position = soldierPos;
                soldierPos.x += spacing;
            }
            soldierPos.x = offset.x;
            soldierPos.z += spacing;
        }
    }
    public void RefreshLine()
    {
        DeleteLine();
        CreateLine();
    }

    public Vector3 GetPos(int index)
    {
        int indexZ = index % zAmount;
        int indexX = (index / zAmount);
        float xRotation = Mathf.Cos(transform.rotation.ToEuler().y);
        float zRotation = Mathf.Sin(transform.rotation.ToEuler().y);
        return TopLeft() + new Vector3(spacing * indexX, 0, spacing * indexZ);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //Gizmos.DrawSphere(GetPos(i), 1);
        }
        Vector3 topLeft = TopLeft();
        Gizmos.DrawSphere(topLeft, 1);

        Gizmos.DrawSphere(transform.position, 1);

        float radius = spacing;
        float degreeStep = 360f / (float)10;
        for (float degree = 0; Math.Round(degree, 2) <= 360; degree += degreeStep)
        {
            double x = transform.position.x + radius * Math.Cos(degree * Math.PI / 180);
            double y = transform.position.z + radius * Math.Sin(degree * Math.PI / 180);

            //Gizmos.DrawSphere(new Vector3((float)x, 1.5f, (float)y), 0.2f);
        }
    }

    public Vector3 TopLeft()
    {
        Vector3 position = (new Vector3(((xAmount -1) / 2f) * spacing, 0, (zAmount -1) / 2f) * spacing);

        float radius = spacing;
        float rotation = gameObject.transform.rotation.eulerAngles.y;
        
        double x = gameObject.transform.position.x + radius * Math.Cos(rotation / 180 * MathF.PI);
        double z = gameObject.transform.position.z + radius * Math.Sin(rotation / 180 * MathF.PI);
        return transform.position - position;
        return new Vector3((float)x, 1.5f, (float)z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[CustomEditor(typeof(LineControl))]
public class LineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LineControl parent = (LineControl)target;
        if (GUILayout.Button("Create Line"))
        {
            parent.CreateLine();
        }
        if (GUILayout.Button("Delete Line"))
        {
            parent.DeleteLine();
        }
        if (GUILayout.Button("Refresh Line"))
        {
            parent.RefreshLine();
        }

        base.OnInspectorGUI();
    }
}
