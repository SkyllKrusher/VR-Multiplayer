using System.Collections;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class SpawnCubes : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform cubeParentTf;
    [SerializeField]
    private Collider spawnAreaLidCollider;

    [SerializeField]
    private int cubesPerBatch;

    private string interactableCubePrefabName = "Grab Interactable";

    public override void OnCreatedRoom()
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
        return;
        Transform cubeTf = PhotonNetwork.Instantiate(interactableCubePrefabName, cubeParentTf.position, quaternion.identity).transform;
        cubeTf.parent = cubeParentTf;
        cubeTf.localPosition = Vector3.zero;
    }
}
