using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private string color;

    public string GetColor()
    {
        return color;
    }
}
