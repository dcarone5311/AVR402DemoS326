using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        Collected();
    }

    private void Collected()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Item was collected");

        ScoreKeep.instance.score++;
        Destroy(gameObject, 1f);
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;

    }

}
