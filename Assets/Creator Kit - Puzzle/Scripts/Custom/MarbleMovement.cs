using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleMovement : MonoBehaviour
{
    private Transform marble;
    private Transform cam;

    [SerializeField]
    private float force = 10.0f;
    [SerializeField]
    private float jumpforce = 500.0f;
    [SerializeField]
    private float jumpCD = 1.0f;
    private float timer = 1.0f;

    void Start()
    {
        marble = GameObject.Find("Marble").transform;
        cam = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //if (Input.GetKey(KeyCode.W))
        //    marble.GetComponent<Rigidbody>().AddForce(cam.forward * force);
        //if (Input.GetKey(KeyCode.S))
        //    marble.GetComponent<Rigidbody>().AddForce(-cam.forward * force);
        //if (Input.GetKey(KeyCode.A))
        //    marble.GetComponent<Rigidbody>().AddForce(-cam.right * force);
        //if (Input.GetKey(KeyCode.D))
        //    marble.GetComponent<Rigidbody>().AddForce(cam.right * force);
        //if (Input.GetKeyDown(KeyCode.Space) && timer > jumpCD)
        //{
        //    marble.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpforce);
        //    timer = 0;
        //}
    }

    public void MarbleMove(int x, int y)
    {
        float speed = Mathf.Sqrt(new Vector2(x,y).magnitude);
        Vector3 direction =
            force * speed * Vector3.Normalize(Quaternion.Euler(0, -Vector3.Angle(Vector3.forward, cam.forward), 0) * new Vector3(x, 0, y));
        marble.GetComponent<Rigidbody>().AddForce
            (new Vector3(direction.x, marble.GetComponent<Rigidbody>().velocity.y, direction.z));
    }

    public void MarbleJump()
    {
        if (timer > jumpCD)
        {
            marble.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpforce);
            timer = 0;
        }
    }
}
