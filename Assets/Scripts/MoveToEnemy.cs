using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnemy : MonoBehaviour
{
    [SerializeField] PlayerType playerType;
    PlayerController playerController;

    GameObject[] enemies;
    GameObject closestEnemy;

    float distanceToClosestEnemy;
    float distanceToEnemy;
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("MainPlayer").gameObject.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (!playerController.rush)
        {
            FindClosestEnemy();
            try
            {
                transform.LookAt(closestEnemy.transform.position);
            }
            catch
            {
                playerController.rush = false;
            }

            transform.Translate(Vector3.forward * playerType.runSpeed * Time.deltaTime / 3);
        }
    }

    void FindClosestEnemy()
    {
        distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = null;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            distanceToEnemy = (enemies[i].transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = enemies[i].transform.gameObject;
            }
        }
        if (closestEnemy == null)
        {
            playerController.rush = false;
        }
    }
}
