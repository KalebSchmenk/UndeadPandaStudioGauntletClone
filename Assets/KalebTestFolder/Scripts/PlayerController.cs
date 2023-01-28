using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();
        RotatePlayer();

    }


    private void MovePlayer()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0, Space.World);
    }

    private void RotatePlayer()
    {
        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var direction = hit.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
        }

        // Implement controller rotation
  
    }


}
