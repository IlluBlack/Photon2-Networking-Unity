using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private float movementSpeed = 5.0f;

    private void Update()
    {
        if (photonView.IsMine)
        {
            ReadInput();
        }
        else
        {
            Debug.Log("Not mine " + photonView.name);
        }
    }

    private void ReadInput()
    {
        Vector3 direction = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0.0f,
            z = Input.GetAxisRaw("Vertical")
        }.normalized;

        controller.SimpleMove(direction*movementSpeed);
    }
}
