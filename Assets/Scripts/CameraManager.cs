using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject PlatformEdge;

    void Update()
    {
        AdjustCameraFOV();
    }

    private void AdjustCameraFOV()
    {
        Vector3 position = PlatformEdge.transform.position;

        Vector3 viewPosition = this.GetComponent<Camera>().WorldToViewportPoint(position);

        if (viewPosition.x < 0.05) this.gameObject.GetComponent<Camera>().fieldOfView += 0.1f;
        else if (viewPosition.x > 0.15) this.gameObject.GetComponent<Camera>().fieldOfView -= 0.1f;

    }
}
