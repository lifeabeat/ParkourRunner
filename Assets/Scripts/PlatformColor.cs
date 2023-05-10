using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer theSR;
    [SerializeField] private SpriteRenderer headerSR;

    private void Start()
    {
        // Change level to Parent (Level Part)
        headerSR.transform.parent = transform.parent;
        headerSR.transform.localScale = new Vector3(theSR.bounds.size.x, .2f);
        headerSR.transform.position = new Vector2(transform.position.x, theSR.bounds.max.y - .1f); // -.1f is distance from Player feet to Header    

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            headerSR.color = GameManager.Instance.platformColor;
        }    
    }
}
