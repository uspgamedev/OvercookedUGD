using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTable : TableClass
{
    private Table table => GetComponent<Table>();
    public float spawnRate;

    private void Update()
    {
        if (tableTuples[0] == Tuple.None)
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
