using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] float magnetForce;

    public GameObject player;
    public List<Rigidbody> playerBodys;

    int spawnCount = 0;
    private void Update()
    {
        RunnerMagnet();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sum"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<DoorController>().otherDoor.GetComponent<DoorController>().number = 0;
            for (int i = 1; i < other.gameObject.GetComponent<DoorController>().number; i++)
            {
                CreateRunner();
            }
        }

        if (other.gameObject.CompareTag("Multiply"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<DoorController>().otherDoor.GetComponent<DoorController>().number = 0;
            spawnCount = (other.gameObject.GetComponent<DoorController>().number - 1) * playerBodys.Count;
            for (int i = 1; i < spawnCount; i++)
            {
                CreateRunner();
            }
        }
    }
    void CreateRunner()
    {
        GameObject ad = Instantiate(player, PlayerSpawnPosition(), transform.rotation);
        ad.transform.SetParent(transform);
        playerBodys.Add(ad.GetComponent<Rigidbody>());
    }
    Vector3 PlayerSpawnPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = pos + transform.position;
        newPos.y = 0f;
        return newPos;
    }

    void RunnerMagnet()
    {
        for (int i = 0; i < playerBodys.Count; i++)
        {
            playerBodys[i].AddForce((transform.position - playerBodys[i].position) * Time.deltaTime * magnetForce);
        }
    }
}
