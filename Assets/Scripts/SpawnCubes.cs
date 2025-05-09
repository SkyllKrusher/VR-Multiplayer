using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField]
    private Transform cubePrefab;
    [SerializeField]
    private Transform cubeParentTf;
    [SerializeField]
    private Collider spawnAreaLidCollider;

    [SerializeField]
    private int cubesPerBatch;

    private void Start()
    {
        SpawnNextBatch();
    }

    private IEnumerator DropCubes()
    {
        spawnAreaLidCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        spawnAreaLidCollider.enabled = true;
        SpawnNextBatch();
    }

    public void OnSpawnLeverOn()
    {
        StartCoroutine(DropCubes());
    }

    private void SpawnNextBatch()
    {
        for (int i = 0; i < cubesPerBatch; i++)
        {
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        Transform cubeTf = Instantiate(cubePrefab, cubeParentTf);
        cubeTf.localPosition = Vector3.zero;
    }
}
