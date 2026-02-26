using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    

    public float walkSpeed, runSpeed, gravity, jumpSpeed, camSensitivity, turnSpeed;

    public Transform child;

    CharacterController controller;
    Vector2 input;
    float vertVelo;

    Animator animator;

    public static int checkPointId = 0;

    bool isComplete;

    // Start is called before the first frame update
    void Start()
    {
        isComplete = false;
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        vertVelo = 0f;


        foreach (GameObject checkpoint in GameObject.FindGameObjectsWithTag("CheckPoint"))
        {
            Debug.Log("ID: " + checkpoint.GetComponent<CheckPoint>().ID);
            if(checkpoint.GetComponent<CheckPoint>().ID == checkPointId)
            {
                controller.Move (checkpoint.transform.position + Vector3.up);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!isComplete)
        {


            Debug.Log(checkPointId);

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


            if (input == Vector2.zero)
                animator.SetInteger("State", 0);
            else
            {

                animator.SetInteger("State", Input.GetKey(KeyCode.LeftShift) ? 2 : 1);
            }


            Vector3 velo = (input.x * transform.right + input.y * transform.forward);


            if (velo != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velo);
                child.rotation = Quaternion.Slerp(child.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }



            velo = velo.normalized;
            velo *= Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            if (controller.isGrounded)
            {
                vertVelo = -5f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    vertVelo = jumpSpeed;
                    animator.SetTrigger("Jump");
                }

            }
            else
            {
                animator.SetInteger("State", 4);
                vertVelo -= gravity * Time.deltaTime;

                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 1f, Vector3.down, out hit, .5f))
                {
                    animator.SetInteger("State", 0);
                }
            }
            velo += vertVelo * Vector3.up;
            controller.Move(velo * Time.deltaTime);

            //Player rotation
            transform.Rotate(Vector3.up * camSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime);

        }
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Goal"))
        {
            checkPointId = 0;
            StartCoroutine(LevelComplete());
            
        }


        if (other.CompareTag("OOB"))
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)); //reload scene

        if (other.CompareTag("CheckPoint"))
        {
            int id = other.gameObject.GetComponent<CheckPoint>().ID;
            if(id > checkPointId)
                checkPointId = id;
        }

    }


    IEnumerator LevelComplete()
    {
        isComplete = true;

        animator.SetInteger("State", 5);

        yield return new WaitForSeconds(3);

        int levels = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % levels);
        yield return null;
    }
}
