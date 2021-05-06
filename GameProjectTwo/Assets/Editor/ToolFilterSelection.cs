using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;

// Change the Tag and/or the layer of the selected GameObjects.

class ToolFilterSelection : EditorWindow
{
    string tagText = "Tag : ";
    string selectedTag = "";
    string layerText = "Layer : ";
    LayerMask selectedLayer = 0;





    [MenuItem("RTools/Filter Selection")]
    static void Init()
    {
        EditorWindow window = GetWindow<ToolFilterSelection>();
        window.position = new Rect(0, 0, 272, 215);
        window.Show();
    }
    public void OnEnable()
    {
        var root = this.rootVisualElement;
        root.style.paddingTop = new StyleLength(10f);
        root.style.paddingBottom = new StyleLength(10f);
        root.style.paddingLeft = new StyleLength(10f);
        root.style.paddingRight = new StyleLength(10f);

    }

    void OnGUI()
    {
        //Knapp SelectNone
        if (GUI.Button(new Rect(10, 10, position.width / 2 - 20, 17), "Select None"))
        {
            Selection.objects = null;
        }
        //Knapp SelectAll
        if (GUI.Button(new Rect(position.width / 2 + 10, 10, position.width / 2 - 20, 17), "Select All"))
        {
            Selection.objects = FindObjectsOfType<GameObject>(); 
        }


        EditorGUIUtility.labelWidth = CalculateLabelWidth(tagText);
        
        //Rullgardin
        selectedTag = EditorGUI.TagField(
            new Rect(10, 50, position.width - 20, 20),
           // tagText + selectedTag,
           tagText,
            selectedTag);

        //Knapp
        if (GUI.Button(new Rect(position.width - 110, 75, 100, 17), "Filter By Tag"))
        {
            if (selectedTag.Length > 0)
                Selection.objects = FindGameObjectsWithTag(selectedTag, Selection.gameObjects);
        }

        EditorGUIUtility.labelWidth = CalculateLabelWidth(layerText);

        //Rullgardin
        selectedLayer = EditorGUI.LayerField(
            new Rect(10, 120, position.width - 20, 20),
            //(layerText + LayerMask.LayerToName(selectedLayer)),
            layerText,
            selectedLayer);

        //Knapp
        if (GUI.Button(new Rect(position.width - 110, 145, 100, 17), "Filter By Layer"))
        {
            Selection.objects = FindGameObjectsWithLayer(selectedLayer, Selection.gameObjects);
        }

        ResetLabelWidth();


        //Knapp
        if (GUI.Button(new Rect(10, 175, position.width - 20, 30), "SerchBy Tag & Layer"))
        {
            Selection.objects = FindGameObjectsWithLayer(selectedLayer, Selection.gameObjects);
            Selection.objects = FindGameObjectsWithTag(selectedTag, Selection.gameObjects);
        }

    }
    void OnInspectorUpdate()
    {
        Repaint();
    }

    GameObject[] FindGameObjectsWithLayer(LayerMask layermask, GameObject[] selected)
    {
        GameObject[] allT = selected;
        if (selected.Length < 1)
        {
            allT = FindObjectsOfType<GameObject>();
        }

        List<GameObject> goList = new List<GameObject>();

        for (var i = 0; i < allT.Length; i++)
        {
            LayerMask o = allT[i].layer;
            if ((layermask.value == o.value))
            {
                goList.Add(allT[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }


    GameObject[] FindGameObjectsWithTag(string searchTag, GameObject[] selected)
    {
        GameObject[] allT = selected;
        if (selected.Length < 1)
        {
            return GameObject.FindGameObjectsWithTag(searchTag);
        }

        List<GameObject> goList = new List<GameObject>();

        for (var i = 0; i < allT.Length; i++)
        {
            if (allT[i].tag == searchTag)
            {
                goList.Add(allT[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }




    public static float CalculateLabelWidth(GUIContent label, float padding = 0f)
    {
        float labelWidth = GUI.skin.label.CalcSize(label).x + padding;
        return labelWidth;
    }
    
    public static float CalculateLabelWidth(string txt, float padding = 0f)
    {
        return CalculateLabelWidth(new GUIContent(txt), padding);
    }

    /// <summary>
    /// Set at construction and used by <see cref="ResetLabelWidth"/> to reset any 
    /// changes made to the <see cref="EditorGUIUtility.labelWidth"/> (by eg. <see cref="CalculateLabelWidth(GUIContent, float)"/>).
    /// </summary>
    private static readonly float originalLabelWidth = EditorGUIUtility.labelWidth;

    /// <summary>
    /// Reset the <see cref="EditorGUIUtility.labelWidth"/> to the default value.
    /// </summary>
    public static void ResetLabelWidth()
    {
        EditorGUIUtility.labelWidth = originalLabelWidth;
    }
}
