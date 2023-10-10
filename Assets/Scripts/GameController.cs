using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] Bubbles;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlatformTop = GameObject.Find("PlatformTop");
        for (int i = 0; i < 6; i++)
        {
            // spawn at position i
            GameObject BubblePosition = PlatformTop.transform.GetChild(i).gameObject;
            Vector3 position = BubblePosition.transform.position;

            GameObject newBubble = Instantiate(Bubbles[i], position, Quaternion.identity);
            newBubble.transform.parent = PlatformTop.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
