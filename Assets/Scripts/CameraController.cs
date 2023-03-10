using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject Player;
    [SerializeField] float cameraHeight = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");


        Vector3 PlayerTransform = Player.transform.position;
        PlayerTransform.y += cameraHeight;
        transform.position = PlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerTransform = Player.transform.position;
        PlayerTransform.y += cameraHeight;
        transform.position = PlayerTransform;
    }

  
}
