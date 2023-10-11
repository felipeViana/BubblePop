using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] Bubbles;

    void Start()
    {
        GameObject[] Platforms = new GameObject[] 
        {
            GameObject.Find("PlatformTop"),
            GameObject.Find("PlatformLeft"),
            GameObject.Find("PlatformRight"),
            GameObject.Find("PlatformDown")
        };

        foreach (GameObject platform in Platforms)
        {
            for (int i = 0; i < 6; i++)
            {
                // spawn at position i
                GameObject Bubble = platform.transform.GetChild(i).gameObject;
                Vector3 position = Bubble.transform.position;

                GameObject newBubble = Instantiate(Bubbles[i], position, Quaternion.identity);
                newBubble.transform.parent = Bubble.transform;
            }
        }   
    }
}
