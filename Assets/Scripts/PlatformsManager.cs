using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    [SerializeField] private GameObject PlatformTop;
    [SerializeField] private GameObject PlatformLeft;
    [SerializeField] private GameObject PlatformRight;
    [SerializeField] private GameObject PlatformDown;

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

    public static PlatformsManager Instance {  get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public void CheckMatches()
    {
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
        CheckColorMatch(SpawnDownUp, SpawnTopDown, true);

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

    private void CheckColorMatch(GameObject SpawnPosition1, GameObject SpawnPosition2, bool shouldFail = false)
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

            if (shouldFail)
            {
                GameController.Instance.SetFailed(true);
            }
        }
    }
}
