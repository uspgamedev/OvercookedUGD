using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public TableType tableType;
    public TupleSO initialTuple;
    public PlayerController player;
    public int capacity = 1;

    public float spawnRate;

    public float sliceTime;
    public State stateTransition;

    public float boilTime;
    public float overcookTime;
    public State overcookedState;

    public TableClass tableScript;

    private void Awake()
    {
        GenerateTable();
    }

    public void GenerateTable()
    {
        if (tableType == TableType.Common)
        {
            if(!GetComponent<CommonTable>())
                tableScript = gameObject.AddComponent<CommonTable>();
            CopyValues(tableScript);
        }
        else if (tableType == TableType.Spawn)
        {
            if (!GetComponent<SpawnTable>())
                tableScript = gameObject.AddComponent<SpawnTable>();
            CopyValues(tableScript);
        }
        else if (tableType == TableType.Keep)
        {
            if (!GetComponent<KeepTable>())
                tableScript = gameObject.AddComponent<KeepTable>();
            CopyValues(tableScript);
        }
        else if (tableType == TableType.Wait)
        {
            if (!GetComponent<WaitTable>())
                tableScript = gameObject.AddComponent<WaitTable>();
            CopyValues(tableScript);
        }
        else if (tableType == TableType.Final)
        {
            if (!GetComponent<FinalTable>())
                tableScript = gameObject.AddComponent<FinalTable>();
            CopyValues(tableScript);
        }
    }
    
    private void CopyValues(TableClass table)
    {
        table.player = player;
        table.capacity = capacity;

        table.tableTuple = new Tuple[capacity];
        table.tableTuple[0] = new Tuple();
        table.tableTuple[0].ingredient = new Ingredient(new string[] { "" });
        table.tableTuple[0].state = initialTuple.state;

        int length = initialTuple.ingredient.ingredientArray.Length;
        for (int i = 0; i < length; i++)
        {
            table.tableTuple[0] = Tuple.CopyTuple(initialTuple.tuple);
            /*
            table.tableTuple[0].ingredient.ingredientArray[i] = initialTuple.ingredient.ingredientArray[i];
            table.tableTuple[0].ingredient.overcookable = initialTuple.ingredient.overcookable;
            table.tableTuple[0].ingredient.mixeable = initialTuple.ingredient.mixeable;
            table.tableTuple[0].ingredient.sliceable = initialTuple.ingredient.sliceable;
            */
        }

        if(table is SpawnTable)
        {
            SpawnTable spawnTable = (SpawnTable)table;
            spawnTable.spawnRate = spawnRate;
        }
        if (table is KeepTable)
        {
            KeepTable keepTable = (KeepTable)table;
            keepTable.sliceTime = sliceTime;
            keepTable.stateTransition = stateTransition;
        }
        else if(table is WaitTable)
        {
            WaitTable waitTable = (WaitTable)table;
            waitTable.boilTime = boilTime;
            waitTable.overcookTime = overcookTime;
            waitTable.overcookedState = overcookedState;
            waitTable.stateTransition = stateTransition;
        }
    }

}

public enum TableType
{
    Common,
    Spawn,
    Keep,
    Wait,
    Final
}
