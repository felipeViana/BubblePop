using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log(this.name);
        
        // rotate platform
        var currentRotation = transform.eulerAngles;

        transform.rotation = Quaternion.Euler(new Vector3(currentRotation.x, currentRotation.y + 60f, currentRotation.z));
    }
}
