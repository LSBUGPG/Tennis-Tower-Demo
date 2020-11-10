using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    public void Remove()
    {
        transform.position = new Vector3(999, 999, 999);
    }

    public void Reset()
    {
        transform.position = startPosition;
    }

}
