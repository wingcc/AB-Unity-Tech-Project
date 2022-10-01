using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataManager))]
public class CustomDataManager : Editor
{
    bool  Parsing=false, Generate = false;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        DataManager dataManager =(DataManager)target;
        GUILayout.Space(5);
        if (GUILayout.Button("Generate Json"))
        {
            Generate = true;
            Parsing = false;
            dataManager.GenerateJsonFile();
          
        }
        if (GUILayout.Button("Get Json and Parsing the data "))
        {
            Generate = false;
            Parsing = true;
            dataManager.StartCoroutine(dataManager.GetJsonTextFromUrl());
        }


        if (dataManager.IsDataGinirated&& Generate)
        {
            GUILayout.Space(5);
            EditorGUILayout.HelpBox("[MainNode] is  Generate To Json File!", MessageType.Info);
        }

        if (dataManager.isDataLoaded&& Parsing)
        {
            GUILayout.Space(5);
            EditorGUILayout.HelpBox("[MainNode]  DATA  Is loaded successfully from Server", MessageType.Info);
        }
        else if (!dataManager.isDataLoaded && Parsing)
        {
            GUILayout.Space(5);
            EditorGUILayout.HelpBox("[MainNode] Your Url Is Empty Or Errer in parsing See the console for more details  ", MessageType.Error);
        }
        
     
    }
}
