using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private string color;
    [SerializeField] private float timeToDie;

    private bool dying = false;
    private float timeDying = 0;

    private void Update()
    {
        if (dying)
        {
            timeDying += Time.deltaTime;
            gameObject.transform.localScale += Vector3.one * 0.00001f;

            if (timeDying > timeToDie )
            {
                timeDying = 0;
                dying = false;
                Destroy(gameObject);
            }
        }
    }
    public string GetColor()
    {
        return color;
    }

    public void StartDying()
    {
        dying = true;
    }
}
