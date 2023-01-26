using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 PlayerTransform = Player.transform.position;
        PlayerTransform.y += 15.0f;
        transform.position = PlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerTransform = Player.transform.position;
        PlayerTransform.y += 25.0f;
        transform.position = PlayerTransform;
    }
}
