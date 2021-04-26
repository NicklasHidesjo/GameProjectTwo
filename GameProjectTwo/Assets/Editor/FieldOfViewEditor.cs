
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    { 
        FieldOfView fov = (FieldOfView)target;
        if (fov.NPC == null) // this is to remove the error when not playing in the editor.
        { 
            return; 
        }

        float viewRadius = fov.NPC.Stats.SightLenght;
        int FoV = fov.NPC.FOV;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, viewRadius);
        Vector3 viewAngleA = DirFromAngle(-FoV / 2, false);
        Vector3 viewAngleB = DirFromAngle(FoV / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * viewRadius);

        Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += fov.transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}