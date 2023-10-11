using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // don't change the order of these positions!
    // same order as spawnPositions defined in prefab
    private enum platformPositions
    {
        DownLeft,
        UpLeft, 
        Up,
        UpRight,
        DownRight,
        Down
    }

    [SerializeField] private float rotateDuration = 1.0f;
    private bool rotating = false;

    private void OnMouseUpAsButton()
    {
        StartCoroutine(rotateObject(this.gameObject, new Vector3(0, 60, 0), rotateDuration));
    }

    // https://stackoverflow.com/a/37588536/21797975
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
        CheckMatches();

    }

    private void CheckMatches()
    {
        GameObject PlatformTop = GameObject.Find("PlatformTop");
        GameObject PlatformLeft = GameObject.Find("PlatformLeft");
        GameObject PlatformRight = GameObject.Find("PlatformRight");
        GameObject PlatformDown = GameObject.Find("PlatformDown");

        // get platform rotation to determine bubble position
        int offsetTop = ConvertRotationToOffset(PlatformTop.transform.eulerAngles.y);
        int offsetLeft = ConvertRotationToOffset(PlatformLeft.transform.eulerAngles.y);
        int offsetRight = ConvertRotationToOffset(PlatformRight.transform.eulerAngles.y);
        int offsetDown = ConvertRotationToOffset(PlatformDown.transform.eulerAngles.y);

        // compare top with left
        GameObject SpawnTopLeft = PlatformTop.transform.GetChild(GetOffsettedPosition((int)platformPositions.DownLeft, offsetTop)).gameObject;
        GameObject SpawnLeftUp = PlatformLeft.transform.GetChild(GetOffsettedPosition((int)platformPositions.UpRight, offsetLeft)).gameObject;
        CheckColorMatch(SpawnLeftUp, SpawnTopLeft);

        // compare top with down
        GameObject SpawnTopDown = PlatformTop.transform.GetChild(GetOffsettedPosition((int)platformPositions.Down, offsetTop)).gameObject;
        GameObject SpawnDownUp = PlatformDown.transform.GetChild(GetOffsettedPosition((int)platformPositions.Up, offsetDown)).gameObject;
        CheckColorMatch(SpawnDownUp, SpawnTopDown);

        // compare top with right
        GameObject SpawnTopRight = PlatformTop.transform.GetChild(GetOffsettedPosition((int)platformPositions.DownRight, offsetTop)).gameObject;
        GameObject SpawnRightUp = PlatformRight.transform.GetChild(GetOffsettedPosition((int)platformPositions.UpLeft, offsetRight)).gameObject;
        CheckColorMatch(SpawnRightUp, SpawnTopRight);

        // compare down with left
        GameObject SpawnDownLeft = PlatformDown.transform.GetChild(GetOffsettedPosition((int)platformPositions.UpLeft, offsetDown)).gameObject;
        GameObject SpawnLeftDown = PlatformLeft.transform.GetChild(GetOffsettedPosition((int)platformPositions.DownRight, offsetLeft)).gameObject;
        CheckColorMatch(SpawnLeftDown, SpawnDownLeft);

        // compare down with right
        GameObject SpawnDownRight = PlatformDown.transform.GetChild(GetOffsettedPosition((int)platformPositions.UpRight, offsetDown)).gameObject;
        GameObject SpawnRightDown = PlatformRight.transform.GetChild(GetOffsettedPosition((int)platformPositions.DownLeft, offsetRight)).gameObject;
        CheckColorMatch(SpawnRightDown, SpawnDownRight);
    }

    private int ConvertRotationToOffset(float rotation)
    {
        return (int)(rotation / 60.0f);
    }

    private int GetOffsettedPosition(int position, int offset)
    {
        int offsettedPosition = position - offset;

        if (offsettedPosition < 0) offsettedPosition += 6;
        if (offsettedPosition >= 6) offsettedPosition -= 6;

        return offsettedPosition;
    }

    private void CheckColorMatch(GameObject SpawnPosition1, GameObject SpawnPosition2)
    {
        if (SpawnPosition1.transform.childCount == 0 || SpawnPosition2.transform.childCount == 0)
        {
            return;
        }

        GameObject Bubble1 = SpawnPosition1.transform.GetChild(0).gameObject;
        GameObject Bubble2 = SpawnPosition2.transform.GetChild(0).gameObject;

        string color1 = Bubble1.GetComponent<Bubble>().GetColor();
        string color2 = Bubble2.GetComponent<Bubble>().GetColor();

        if (color1 == color2)
        {
            Destroy(Bubble1);
            Destroy(Bubble2);
        }

    }
}
