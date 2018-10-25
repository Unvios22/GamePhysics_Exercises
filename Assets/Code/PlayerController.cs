using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1f, 100f)] [SerializeField] float movementSpeed = 1f;
     float rotationSpeed = 1f;
	private Animator _playerAnimator;

	private void Start() {
		_playerAnimator = gameObject.GetComponentInChildren<Animator>();
	}

	private void Update() {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * movementSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) {

			_playerAnimator.ResetTrigger("isStationary");

			if (Input.GetKey(KeyCode.LeftShift)) {
				_playerAnimator.SetTrigger("isRunning");
				_playerAnimator.ResetTrigger("isMoving");
			}
			else if (Input.GetKey(KeyCode.C)) {
				_playerAnimator.SetTrigger("isSneaking");
				_playerAnimator.ResetTrigger("isMoving");
			}
			_playerAnimator.SetTrigger("isMoving");
			
		}
		else {
			_playerAnimator.SetTrigger("isStationary");
			_playerAnimator.ResetTrigger("isMoving");
			_playerAnimator.ResetTrigger("isSneaking");
			_playerAnimator.ResetTrigger("isRunning");
			
		}
    }
	
}