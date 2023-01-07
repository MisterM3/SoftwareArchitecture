using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorButtonVisualisePath : MonoBehaviour
{


    static EnemyPathManager pathManager;


    [MenuItem("ResearchTree/Update")]
    static void UpdateVisuals()
    {

        pathManager = GameObject.FindObjectOfType<EnemyPathManager>();

        pathManager.PathVisual();
    }
}
