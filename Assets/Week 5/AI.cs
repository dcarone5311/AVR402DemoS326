using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Transform player;
    public float speed, turnspeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameManager.GM.score;
        if (player != null)
        {
            Vector3 move = player.position - transform.position;
            move = move.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }

    }
}
