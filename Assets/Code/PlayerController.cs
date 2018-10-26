using System;
using System.Collections;
using System.Collections.Generic;
using Code.Readonly_Data;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1f, 100f)] [SerializeField] float movementSpeed = 1f;
	[SerializeField] private Rigidbody _playerRigidbody;
	[SerializeField] private float _jumpForce = 450f;
	float rotationSpeed = 1f;
	private Animator _playerAnimator;
	private bool _isOnGround;
	private Transform _playerTransform;
	private bool _gameStarted;
	
	private void Start() {
		_playerAnimator = gameObject.GetComponentInChildren<Animator>();
		_playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
		_playerTransform = this.transform;
		_gameStarted = true;

	}

	private void Update() {
        ReadInput();
		ApplyAnimationTriggers();
    }
	
	void OnDrawGizmos()
	{
		if (_gameStarted){
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(
				new Vector3(_playerTransform.position.x, _playerTransform.position.y - 1f, _playerTransform.position.z),
				0.4f);
		}
	}

	private void ReadInput(){
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f * rotationSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * movementSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		Collider[] collisions = Physics.OverlapSphere(new Vector3(_playerTransform.position.x, _playerTransform.position.y - 1f, _playerTransform.position.z), 0.4f);
		foreach (var collision in collisions){
			if (!collision.CompareTag("Player") && collision.gameObject.layer != 9){ // 9 == PlayerArmature layer
				Debug.Log(collision.name);
				Debug.Log(collision.gameObject.layer);
				_isOnGround = true;
				break;
			}
			_isOnGround = false;
		}
		
		if (Input.GetKeyDown(KeyCode.Space) && _isOnGround){
			_playerRigidbody.AddForce(Vector3.up * _jumpForce);
		}
	}
	
	private void ApplyAnimationTriggers(){
		if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) {
			_playerAnimator.ResetTrigger(AnimatorTriggers.STATIONARY);
			
			if (Input.GetKey(KeyCode.LeftShift)) {
				_playerAnimator.ResetTrigger(AnimatorTriggers.SNEAKING);
				_playerAnimator.ResetTrigger(AnimatorTriggers.WALKING);
				_playerAnimator.SetTrigger(AnimatorTriggers.RUNNING);
			}
			else if (Input.GetKey(KeyCode.C)) {
				_playerAnimator.ResetTrigger(AnimatorTriggers.WALKING);
				_playerAnimator.ResetTrigger(AnimatorTriggers.RUNNING);
				_playerAnimator.SetTrigger(AnimatorTriggers.SNEAKING);
			}
			else{
				_playerAnimator.ResetTrigger(AnimatorTriggers.RUNNING);
				_playerAnimator.ResetTrigger(AnimatorTriggers.SNEAKING);
				_playerAnimator.SetTrigger(AnimatorTriggers.WALKING);
			}
		}
		else {
			_playerAnimator.ResetTrigger(AnimatorTriggers.RUNNING);
			_playerAnimator.ResetTrigger(AnimatorTriggers.SNEAKING);
			_playerAnimator.ResetTrigger(AnimatorTriggers.WALKING);
			_playerAnimator.SetTrigger(AnimatorTriggers.STATIONARY);
		}
	}
}