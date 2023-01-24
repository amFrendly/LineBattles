using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LineControl : MonoBehaviour
{
    [SerializeField] 
    GameObject soldier;

    [SerializeField] 
    Transform soldierHolder;

    [SerializeField] 
    int rows;

    [SerializeField]
    int columns;

    [SerializeField]
    float spacing;

    public Vector3 GetLine()
    {
        float rotation = transform.rotation.eulerAngles.y;
        float x = Mathf.Cos(-rotation / 180 * Mathf.PI);
        float z = Mathf.Sin(-rotation / 180 * Mathf.PI);

        return new Vector3(x, 0, z) * spacing;
    }
    public Vector3 GetLineNormal()
    {
        float rotation = transform.rotation.eulerAngles.y - 90;
        float x = Mathf.Cos(-rotation / 180 * Mathf.PI);
        float z = Mathf.Sin(-rotation / 180 * Mathf.PI);

        return new Vector3(x, 0, z) * spacing;
    }
    public void CreateLine()
    {
        Vector3 line = GetLine();
        Vector3 lineNormal = GetLineNormal();
        Vector3 offset = (-line * (rows - 1) / 2f) + (-lineNormal * (columns - 1) / 2f) + transform.position;

        int index = 0;
        for (int t = 0; t < rows; t++)
        {
            for(int column = 0; column < columns; column++)
            {
                GameObject soldier = Instantiate(this.soldier, soldierHolder);
                soldier.transform.position = (line * t) + (lineNormal * column) + offset;
                soldier.transform.rotation = transform.rotation;
                soldier.GetComponent<SoldierBehaviour>().index = index;
                index++;
            }
        }
    }
    public Vector3 GetPosition(int index)
    {
        Vector3 line = GetLine();
        Vector3 lineNormal = GetLineNormal();
        Vector3 offset = (-line * (rows - 1) / 2f) + (-lineNormal * (columns - 1) / 2f) + transform.position;

        int columnIndex = index % columns;
        int rowIndex = index / columns;
        Vector3 position = (line * rowIndex) + (lineNormal * columnIndex) + offset;
        return position;
    }
    public void DeleteLine()
    {
        for (int i = soldierHolder.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(soldierHolder.GetChild(i).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < rows * columns; i++)
        {
            Gizmos.DrawSphere(GetPosition(i), 0.3f);
        }
    } 
}

[CustomEditor(typeof(LineControl)), CanEditMultipleObjects]
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
        if (GUILayout.Button("Refresh"))
        {
            parent.DeleteLine();
            parent.CreateLine();
        }

        base.OnInspectorGUI();
    }
}
