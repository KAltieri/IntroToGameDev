using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Rigidbody _myRB;
    private float _jumpForceScalar = 5;

    private bool _onGround;

    private RaycastHit _myRCH;

    private AudioSource _myAS;

	void Start () {
        _myRB = GetComponent<Rigidbody>();
        _myAS = GetComponent<AudioSource>();
	}
	
	void Update () {
        GroundCheck();

        if (_onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                _myAS.Play();
            }
        }
	}

    private void Jump()
    {
        _myRB.AddForce(Vector3.up * _jumpForceScalar, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        Physics.Raycast(transform.position, Vector3.down, out _myRCH, Mathf.Infinity);

        if (_myRCH.distance < .55f)
            _onGround = true;
        else
            _onGround = false;
    }
}
