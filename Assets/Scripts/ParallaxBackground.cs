using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float length;
    private float xPostion;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPostion = transform.position.x;

        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPostion + distanceToMove, transform.position.y);

        if (distanceToMoved > xPostion + length)
        {
            xPostion += length;
        }    
    }
}
