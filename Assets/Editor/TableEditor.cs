using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Table))]
public class TableEditor : Editor
{
    SerializedProperty tableTyple;
    SerializedProperty initialTuple;
    SerializedProperty capacity;

    SerializedProperty spawnRate;

    SerializedProperty sliceTime;
    SerializedProperty stateTransition;

    SerializedProperty boilTime;
    SerializedProperty overcookTime;
    SerializedProperty overcookedState;

    SerializedProperty foodRenderer;

    SerializedProperty orderList;

    private void OnEnable()
    {
        tableTyple = serializedObject.FindProperty("tableType");
        initialTuple = serializedObject.FindProperty("initialTuple");
        capacity = serializedObject.FindProperty("capacity");
        spawnRate = serializedObject.FindProperty("spawnRate");
        sliceTime = serializedObject.FindProperty("sliceTime");
        stateTransition = serializedObject.FindProperty("stateTransition");
        boilTime = serializedObject.FindProperty("boilTime");
        overcookTime = serializedObject.FindProperty("overcookTime");
        overcookedState = serializedObject.FindProperty("overcookedState");
        foodRenderer = serializedObject.FindProperty("foodRenderer");
        orderList = serializedObject.FindProperty("orderList");

    }

    public override void OnInspectorGUI()
    {
        Table _table = (Table)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(tableTyple);
        EditorGUILayout.PropertyField(initialTuple);
        EditorGUILayout.PropertyField(capacity);
        EditorGUILayout.PropertyField(foodRenderer);


        if (_table.tableType == TableType.Spawn)
        {
            EditorGUILayout.PropertyField(spawnRate);
        }
        if(_table.tableType == TableType.Keep)
        {
            EditorGUILayout.PropertyField(sliceTime);
            EditorGUILayout.PropertyField(stateTransition);
        }
        if(_table.tableType == TableType.Wait)
        {
            EditorGUILayout.PropertyField(boilTime);
            EditorGUILayout.PropertyField(overcookTime);
            EditorGUILayout.PropertyField(stateTransition);
            EditorGUILayout.PropertyField(overcookedState);
        }
        if(_table.tableType == TableType.Final){
            EditorGUILayout.PropertyField(orderList);
        }


        serializedObject.ApplyModifiedProperties();
    }

}
