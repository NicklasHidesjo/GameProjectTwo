
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    { 
        FieldOfView fow = (FieldOfView)target;
        if (fow.NPC == null) // this is to remove the error when not playing in the editor.
        { 
            return; 
        }

        float viewRadius = fow.NPC.Stats.SightLenght;
        int FoW = fow.NPC.FOW;

        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, viewRadius);
        Vector3 viewAngleA = DirFromAngle(-FoW / 2, false);
        Vector3 viewAngleB = DirFromAngle(FoW / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * viewRadius);

        Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += fow.transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}