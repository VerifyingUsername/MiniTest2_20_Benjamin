using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 14.0f;
    float jump = 5.0f;
    float gravityModifier = 2.0f;
    float absZ;

    bool isOnMoveCube = false;
    bool isOnStartCube = true;
    bool isOnCube = true;

    int col = 0;
    Rigidbody playerRb;
    Vector3 startPos;

    public GameObject movingCube;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {               
        float horizontalInput = Input.GetAxis("Horizontal");
       
        transform.Translate(Vector3.forward * Time.deltaTime * horizontalInput * speed);

        if(Input.GetKey(KeyCode.Space) && !isOnMoveCube && isOnStartCube && isOnCube)
        {
            playerRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            isOnCube = false;
        }

        if (Input.GetKey(KeyCode.Space) && isOnMoveCube && !isOnStartCube && isOnCube)
        {
            playerRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            isOnCube = false;
        }

        if (isOnMoveCube && isOnCube)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, movingCube.transform.position.z - absZ);
        }

        if (isOnStartCube && isOnCube)
        {
            transform.position = startPos;
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("MovingCube"))
        {
            isOnMoveCube = true;
            isOnStartCube = false;
            isOnCube = true;

            float playerZ = transform.position.z;
            float cubeZ = movingCube.transform.position.z;

            absZ = cubeZ - playerZ;
            //transform.position = new Vector3(transform.position.x, transform.position.y, movingCube.transform.position.z - absZ);

        }

        if (collision.gameObject.CompareTag("StartCube"))
        {
            isOnMoveCube = false;
            isOnStartCube = true;
            isOnCube = true;

            startPos = transform.position;
        }
    }

    
}
