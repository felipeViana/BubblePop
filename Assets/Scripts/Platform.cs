using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float rotateDuration = 1.0f;
    private bool rotating = false;

    private void OnMouseUpAsButton()
    {
        StartCoroutine(rotateObject(this.gameObject, new Vector3(0, 60, 0), rotateDuration));
    }

    private IEnumerator rotateObject(GameObject gameObjectToMove, Vector3 eulerAngles, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Vector3 newRot = gameObjectToMove.transform.eulerAngles + eulerAngles;

        Vector3 currentRot = gameObjectToMove.transform.eulerAngles;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.eulerAngles = Vector3.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotating = false;
    }
}
