using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerNew : MonoBehaviour
{
    public float horz, vert;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (player != null)
        transform.LookAt(player);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(player != null)
        {

            transform.position = player.position + vert * Vector3.up - horz * player.forward;
            //has issues when camera rotates vertically, fix later
            transform.LookAt(player);

        }

    }
}
