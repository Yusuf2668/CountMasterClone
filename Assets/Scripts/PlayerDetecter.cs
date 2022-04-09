using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    public List<GameObject> enemyControllersList;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyControllersList.Add(transform.GetChild(i).transform.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainPlayer"))
        {
            other.gameObject.GetComponent<PlayerController>().rush = false;
            for (int i = 0; i < enemyControllersList.Count; i++)
            {
                enemyControllersList[i].GetComponent<EnemyController>().attack = true; // for enemy action
                enemyControllersList[i].tag = "Enemy";
            }
        }
    }
}
