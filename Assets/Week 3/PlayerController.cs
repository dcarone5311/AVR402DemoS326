using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed, runSpeed, gravity, jumpSpeed, turnSpeed;


    CharacterController controller;
    Vector2 input;
    float vertVelo;

    public static int checkPointId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        vertVelo = 0f;


        foreach (GameObject checkpoint in GameObject.FindGameObjectsWithTag("CheckPoint"))
        {
            Debug.Log("ID: " + checkpoint.GetComponent<CheckPoint>().ID);
            if(checkpoint.GetComponent<CheckPoint>().ID == checkPointId)
            {
                transform.position = checkpoint.transform.position;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(checkPointId);

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 velo = (input.x * transform.right + input.y * transform.forward);
        velo = velo.normalized;
        velo *= Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if(controller.isGrounded)
        {
            vertVelo = Input.GetKeyDown(KeyCode.Space) ? jumpSpeed : -5f;
        }
        else
        {
            vertVelo -= gravity * Time.deltaTime;
        }
        velo += vertVelo * Vector3.up;
        controller.Move(velo * Time.deltaTime);

        //Player rotation
        transform.Rotate(Vector3.up * turnSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {

        int levels = SceneManager.sceneCountInBuildSettings;

        if (other.CompareTag("Goal"))
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex+1)%levels);
            

        if(other.CompareTag("OOB"))
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)); //reload scene

        if(other.CompareTag("CheckPoint"))
        {
            int id = other.gameObject.GetComponent<CheckPoint>().ID;
            if(id > checkPointId)
                checkPointId = id;
        }

    }
}
