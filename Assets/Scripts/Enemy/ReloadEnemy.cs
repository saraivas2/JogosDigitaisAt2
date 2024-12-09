using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    private Transform spawnPoint;
    private GameObject currentEnemy;
    private float respawnDelay = 3f;
    private bool isRespawning = false;
    
    void Start()
    {
        // Carrega o prefab do inimigo
        currentEnemy = GameObject.FindWithTag("enemies");
        if (currentEnemy == null)
        {
            Debug.LogError("GameObject não encontrado ou tag 'enemies' foi encontrado!");
            return;
        }

        // Procura o ponto de spawn
        GameObject Point = GameObject.FindWithTag("point");
        if (Point != null)
        {
            spawnPoint = Point.transform;
        }
        else
        {
            Debug.LogError("Nenhum objeto com a tag 'point' foi encontrado!");
            return;
        }
    }

    void Update()
    {
        if (currentEnemy == null)
        {
            isRespawning=true;
        }
        else
        {
            isRespawning=false;
        }
        
        if (isRespawning) 
        {
            StartCoroutine(RespawnEnemy(currentEnemy));
        } 
    }

    IEnumerator RespawnEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(3f); // Aguarda 3 segundos
        enemy.transform.position = spawnPoint.position; // Move para o ponto de spawn
        enemy.SetActive(true); // Reativa o inimigo
        Debug.Log("Inimigo reativado!");
    }

}
