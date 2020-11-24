using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curveball : MonoBehaviour
{
    ParabolaController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<ParabolaController>();
        Fired();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Fired();
        }
        */
    }

    public void Fired()
    {
        pc.FollowParabola();
    }
}
