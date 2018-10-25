using System.Collections;
using System.Collections.Generic;
using Code.Readonly_Data;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(1f, 100f)] [SerializeField] float movementSpeed = 1f;
     float rotationSpeed = 1f;
	private Animator _playerAnimator;

	private void Start() {
		_playerAnimator = gameObject.GetComponentInChildren<Animator>();
	}

	private void Update() {
        ReadInput();
		ApplyAnimationTriggers();
    }

	private void ReadInput(){
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f * rotationSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * movementSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

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