using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTable : TableClass
{
    private Table table;
    public float spawnRate;

    private void Awake()
    {
        table = gameObject.GetComponent<Table>();
    }

    private void Update()
    {
        if (tableTuple[0] == Tuple.None)
        {
            StartCoroutine(InitializeCoroutine());
        }
    }

    IEnumerator InitializeCoroutine()
    {
        yield return new WaitForSeconds(spawnRate);
        table.GenerateTable();
    }

    public override void UseTable(){}
}
