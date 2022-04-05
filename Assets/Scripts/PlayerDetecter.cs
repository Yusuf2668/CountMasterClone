using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    public List<EnemyController> enemyControllersList;
    private void Start()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            enemyControllersList.Add(transform.GetChild(i).GetComponent<EnemyController>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().rush = false;
            for (int i = 0; i < enemyControllersList.Count; i++)
            {
                enemyControllersList[i].attack = true;
            }
        }
    }
}
