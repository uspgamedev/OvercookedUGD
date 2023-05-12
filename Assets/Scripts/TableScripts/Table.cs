using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Essa classe � respons�vel por inicializar as t�buas. Ela serve de intermedi�ria entre TableClass e TableEditor, que possibilita um custom inspector pras t�buas
//no inspector.
public class Table : MonoBehaviour
{
    public TableType tableType;
    public TupleSO initialTuple;
    public int capacity = 1;

    public float spawnRate;

    [SerializeField] private Transform orderList;

    [SerializeField] SpriteRenderer foodRenderer;

    public float sliceTime;
    public State stateTransition;

    public float boilTime;
    public float overcookTime;
    public State overcookedState;

    public TableClass tableScript;

    private void Awake()
    {
        //Inicializa a t�bua
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

        tableScript.foodRenderer = foodRenderer;
        tableScript.PaintTable();
    }
    
    private void CopyValues(TableClass table)
    {
        //Inicializa a t�bua a partir do TupleSO fornecido
        table.capacity = capacity;

        table.tableTuples = new Tuple[capacity];
        table.tableTuples[0] = Tuple.CopyTuple(initialTuple.tuple);

        table.tableTuples = table.tableTuples.Select(t => t ?? Tuple.None).ToArray();

        if (table is SpawnTable)
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
        else if(table is FinalTable){
            FinalTable finalTable = (FinalTable)table;
            finalTable.orderList = orderList;
        }
    }
}

//Sempre que adicionar um tipo de t�bua, adicione aqui (n�o muito escalon�vel).
public enum TableType
{
    Common,
    Spawn,
    Keep,
    Wait,
    Final
}
