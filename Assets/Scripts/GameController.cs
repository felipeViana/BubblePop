using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

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
    private string bubblesAsString;

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
        PlayerPrefs.DeleteAll();
        Instance = this;
        SetFailed(false);
        SetWon(false);
        LoadData();

        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            LoadLevel();
        }
        else
        {
            GenerateLevel();
        }
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            levelNumber = PlayerPrefs.GetInt("LevelNumber");
            UpdateLevelText();

            bubblesAsString = PlayerPrefs.GetString("Bubbles");
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("LevelNumber", levelNumber);
        PlayerPrefs.SetString("Bubbles", bubblesAsString);
    }

    private void UpdateLevelText()
    {
        LevelText.GetComponent<TMP_Text>().text = "Level: " + levelNumber.ToString();
    }
    private void LoadLevel()
    {
        for (int platformIndex = 0; platformIndex < Platforms.Length; platformIndex++)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject SpawnPosition = Platforms[platformIndex].transform.GetChild(i).gameObject;
                Vector3 position = SpawnPosition.transform.position;

                GameObject newBubble = Instantiate(Bubbles[Int32.Parse(bubblesAsString.ElementAt(i + platformIndex * 6).ToString())], position, Quaternion.identity);
                newBubble.transform.parent = SpawnPosition.transform;

                bubblesInPlay.Add(newBubble);
            }
        }
    }

    private List<int> GenerateIndexList()
    {
        var indexList = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, 6);
            } while (indexList.Contains(randomIndex));

            indexList.Add(randomIndex);
        }

        return indexList;
    }

    private void GenerateLevel()
    {
        bubblesAsString = "";
        bubblesInPlay.Clear();

        List<int> indexList = GenerateIndexList();
        foreach (GameObject platform in Platforms)
        {

            for (int i = 0; i < 6; i++)
            {
                GameObject SpawnPosition = platform.transform.GetChild(i).gameObject;
                Vector3 position = SpawnPosition.transform.position;

                GameObject newBubble = Instantiate(Bubbles[indexList.ElementAt(i)], position, Quaternion.identity);
                newBubble.transform.parent = SpawnPosition.transform;

                bubblesInPlay.Add(newBubble);
                bubblesAsString += indexList.ElementAt(i);
            }
        }

        SaveData();
    }

    public void RestartLevel()
    {
        ResetConditions();
        LoadLevel();
    }

    public void WinLevel()
    {
        SetWon(true);
    }

    private void ResetConditions()
    {
        SetFailed(false);
        SetWon(false);
        DestroyAllBubbles();
        ResetRotations();
    }

    public void AdvanceLevel()
    {
        levelNumber++;
        UpdateLevelText();

        ResetConditions();
        GenerateLevel();
    }

    public void DestroyBubbles(GameObject Bubble1, GameObject Bubble2)
    {
        Bubble1.GetComponent<Bubble>().StartDying();
        Bubble2.GetComponent<Bubble>().StartDying();

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

        bubblesAsString = null;
    }

    private void ResetRotations()
    {
        foreach(GameObject platform in Platforms)
        {
            platform.transform.eulerAngles = new Vector3(270, 0, 0);
        }
    }
}
