using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levelPart;
    [SerializeField] private Vector3 nextPartPos;

    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;
    [SerializeField] private Transform player;


    private void Update()
    {
        GeneratedPlatform();
        PartToDelete();
    }

    private void GeneratedPlatform()
    {
        // Testing Spawn Platform

        /*if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Transform part = levelPart[Random.Range(0, levelPart.Length)];
            Vector2 newPostion = new Vector2(nextPartPos.x - part.Find("StartPoint").position.x, 0);
            Transform newPart = Instantiate(part, newPostion, transform.rotation, transform);
            nextPartPos = newPart.Find("EndPoint").position;
        }    */
        
        while (Vector2.Distance(player.transform.position,nextPartPos) < distanceToSpawn)
        {
            Transform part = levelPart[Random.Range(0, levelPart.Length)];

            Vector2 newPostion = new Vector2(nextPartPos.x - part.Find("StartPoint").position.x, 0);

            Transform newPart = Instantiate(part, newPostion, transform.rotation, transform);

            nextPartPos = newPart.Find("EndPoint").position;
        }
    }

    private void PartToDelete()
    {
        if (transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(player.transform.position, partToDelete.transform.position) > distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }
    }
}
