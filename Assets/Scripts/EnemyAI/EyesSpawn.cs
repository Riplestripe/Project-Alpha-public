using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesSpawn : MonoBehaviour
{
    [Header("Референс то спавнить")]
    public GameObject spawnPrefab;
    public bool canSeePlayer;
    public bool isSpawned;
    [Header("Референс игрока")]
    public GameObject player;



    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canSeePlayer = GetComponent<FieldOfView>().canSeePlayer;
        if (canSeePlayer && !isSpawned) eyesEnemy();
    }



    private void eyesEnemy()
     {
    isSpawned = true;
     Instantiate(spawnPrefab, player.transform.position - player.transform.forward * 10f, transform.rotation);
     }
}
