using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] Platforms;
    [SerializeField] private GameObject[] Bubbles;

    [SerializeField] private GameObject FailText;
    [SerializeField] private GameObject RestartButton;

    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject ContinueButton;

    [SerializeField] private GameObject LevelText;

    private int levelNumber = 1;

    private List<GameObject> bubblesInPlay = new List<GameObject>(24);

    public static GameController Instance { get; private set; }

    private bool failed = false;
    private bool won = false;

    public bool GetFailed()
    {
        return failed;
    }
    public void SetFailed(bool newValue)
    {
        failed = newValue;

        FailText.SetActive(newValue);
        RestartButton.SetActive(newValue);
    }

    private void SetWon(bool newValue) 
    {
        won = newValue;

        WinText.SetActive(newValue);
        ContinueButton.SetActive(newValue);
    }

    void Start()
    {
        Instance = this;
        SetFailed(false);
        SetWon(false);
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

                bubblesInPlay.Add(newBubble);
            }
        }
    }

    public void RestartLevel()
    {
        SetFailed(false);
        SetWon(false);
        DestroyAllBubbles();
        ResetRotations();
        GenerateLevel();
    }

    public void WinLevel()
    {
        SetWon(true);
    }

    public void AdvanceLevel()
    {
        levelNumber++;

        LevelText.GetComponent<TMP_Text>().text = "Level: " + levelNumber.ToString();

        RestartLevel();
    }

    public void DestroyBubbles(GameObject Bubble1, GameObject Bubble2)
    {
        Destroy(Bubble1);
        Destroy(Bubble2);

        bubblesInPlay.Remove(Bubble1);
        bubblesInPlay.Remove(Bubble2);

        if (bubblesInPlay.Count == 0)
        {
            WinLevel();
        }
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
                    bubblesInPlay.Remove(bubble);
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
