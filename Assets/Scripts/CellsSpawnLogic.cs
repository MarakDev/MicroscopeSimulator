using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellsSpawnLogic : MonoBehaviour
{
    [SerializeField] private Transform cellStorage;
    [SerializeField] private int maxNumberCells;


    [ContextMenu("GenerateCellsField")]
    public void GenerateCellsField()
    {
        float width = transform.localScale.x / 2;
        float heigth = transform.localScale.z / 2;

        float spawnPosX = transform.localPosition.x;
        float spawnPosZ = transform.localPosition.z;

        int maxNumberCellsRange = Random.Range(maxNumberCells - 25, maxNumberCells + 25);

        GameManager.instance.maxNCell = maxNumberCellsRange;

        for (int i = 0; i < maxNumberCellsRange; i++)
        {
            Vector3 cellPosition = new Vector3(spawnPosX + Random.Range(-width, width), 0, spawnPosZ + Random.Range(-heigth, heigth));

            Instantiate(Resources.Load("Prefabs/Cell"), cellPosition, Quaternion.Euler(0,Random.Range(0,360),0), cellStorage);
        }
    }

    [ContextMenu("ClearCellsField")]
    public void ClearCellsField()
    {
        foreach (Transform cell in cellStorage)
        {
            Destroy(cell.gameObject);
        }
    }
}
