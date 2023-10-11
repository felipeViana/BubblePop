using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] Platforms;
    [SerializeField] private GameObject[] Bubbles;

    [SerializeField] private GameObject FailText;
    [SerializeField] private GameObject RestartButton;

    public static GameController Instance { get; private set; }

    private bool failed = false;

    public bool GetFailed()
    {
        return failed;
    }
    public void SetFailed(bool newValue)
    {
        failed = newValue;

        if (failed)
        {
            FailText.SetActive(true);
            RestartButton.SetActive(true);
        }
        else
        {
            FailText.SetActive(false);
            RestartButton?.SetActive(false);
        }
    }

    void Start()
    {
        Instance = this;
        SetFailed(false);
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        foreach (GameObject platform in Platforms)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject SpawnPosition = platform.transform.GetChild(i).gameObject;
                Vector3 position = SpawnPosition.transform.position;

                GameObject newBubble = Instantiate(Bubbles[i], position, Quaternion.identity);
                newBubble.transform.parent = SpawnPosition.transform;
            }
        }
    }

    public void RestartLevel()
    {
        SetFailed(false);
        DestroyAllBubbles();
        ResetRotations();
        GenerateLevel();
    }

    private void DestroyAllBubbles()
    {
        foreach (GameObject platform in Platforms)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject SpawnPosition = platform.transform.GetChild(i).gameObject;
                
                for (int j = 0; j < SpawnPosition.transform.childCount; j++)
                {
                    GameObject bubble = SpawnPosition.transform.GetChild(j).gameObject;
                    Destroy(bubble);
                }
            }

        }
    }

    private void ResetRotations()
    {
        foreach(GameObject platform in Platforms)
        {
            platform.transform.eulerAngles = new Vector3(270, 0, 0);
        }
    }
}
