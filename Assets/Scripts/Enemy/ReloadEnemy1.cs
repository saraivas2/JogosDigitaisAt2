using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadEnemy1 : MonoBehaviour
{
    private Transform spawnPoint;
    private GameObject currentEnemy;
    private NPCMovement1 enemyScript; // Referência ao script do inimigo
    private float respawnDelay = 3f;
    private bool isRespawning = false;

    void Start()
    {
        // Localiza o inimigo na cena
        currentEnemy = GameObject.FindWithTag("enemy2");
        if (currentEnemy == null)
        {
            Debug.LogError("GameObject com a tag 'enemies' não foi encontrado!");
            return;
        }

        // Obtenha o script do inimigo
        enemyScript = GameObject.Find("Enemy2").GetComponent<NPCMovement1>();
        if (enemyScript == null)
        {
            Debug.LogError("O script NPCMovement não foi encontrado no inimigo!");
            return;
        }

        // Localiza o ponto de spawn
        GameObject Point = GameObject.FindWithTag("point2");
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

