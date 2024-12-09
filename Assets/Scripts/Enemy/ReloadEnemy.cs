using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    private Transform spawnPoint;
    private GameObject currentEnemy;
    private NPCMovement enemyScript; // Referência ao script do inimigo
    private float respawnDelay = 3f;
    private bool isRespawning = false;

    void Start()
    {
        // Localiza o inimigo na cena
        currentEnemy = GameObject.FindWithTag("enemies");
        if (currentEnemy == null)
        {
            Debug.LogError("GameObject com a tag 'enemies' não foi encontrado!");
            return;
        }

        // Obtenha o script do inimigo
        enemyScript = currentEnemy.GetComponent<NPCMovement>();
        if (enemyScript == null)
        {
            Debug.LogError("O script NPCMovement não foi encontrado no inimigo!");
            return;
        }

        // Localiza o ponto de spawn
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
        // Verifica se o inimigo está desativado e inicia o respawn
        if (!currentEnemy.activeSelf && !isRespawning)
        {
            isRespawning = true;
            StartCoroutine(RespawnEnemy());
        }
    }

    IEnumerator RespawnEnemy()
    {
        yield return new WaitForSeconds(respawnDelay); // Aguarda o tempo de respawn

        // Reinicializa as variáveis do inimigo
        if (enemyScript != null)
        {
            enemyScript.isDead = false;
            enemyScript.timeDeath = 1.5f;
            enemyScript.vida = 100;
        }

        // Move o inimigo para o ponto de spawn
        currentEnemy.transform.position = spawnPoint.position;

        // Reativa o inimigo
        currentEnemy.SetActive(true);
        Debug.Log("Inimigo reativado!");

        isRespawning = false; // Permite novos respawns no futuro
    }
}

