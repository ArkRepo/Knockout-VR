using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


   // public float speedH = 2.0f;
    //public float speedV = 2.0f;
    //private float yaw = 0.0f;
    //private float pitch = 0.0f;
 // Reference to the player GameObject.
 //public GameObject player;

 // The distance between the camera and the player.
 //private Vector3 offset;

 // Start is called before the first frame update.
 void Start()
    {
 // Calculate the initial offset between the camera's position and the player's position.
      //  offset = transform.position - player.transform.position; 
    }

    void Update(){

      //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

 // LateUpdate is called once per frame after all Update functions have been completed.
 void LateUpdate()
    {
 // Maintain the same offset between the camera and player throughout the game.
        //transform.position = player.transform.position + offset;  
    }
}