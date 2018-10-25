using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
	[SerializeField] private float _time;
	private Transform _elevatorTransform;
	private Vector3 _elevatorStart = new Vector3(0f, 0f, 0f);
	private Vector3 _elevatorStop = new Vector3(0f, 10f, 0f);
	private Vector3 _elevatorGround;
	private bool _goingUp;
	private bool _playerOnGround;
	private Transform _playerTransform;

	void Start() {
		_elevatorTransform = this.transform;
		_elevatorStart = _elevatorTransform.position;
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		_elevatorGround = GameObject.FindGameObjectWithTag("ElevatorGround").transform.position;
	}

	private void OnTriggerStay(Collider collider) {
		_elevatorTransform.position = Vector3.Lerp(_elevatorTransform.position, _elevatorStop, _time * Time.deltaTime);
		_goingUp = true;
		_playerOnGround = false;
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			_goingUp = false;
		}
	}

	void Update() {
		if (_playerTransform.position.y <= _elevatorGround.y + 1f) {
			_playerOnGround = true;
		}
		if (!_goingUp) {
			_elevatorTransform.position =
				Vector3.Lerp(_elevatorTransform.position, _elevatorStart, _time * Time.deltaTime);
			if (!_playerOnGround) {
				_playerTransform.position = Vector3.Lerp(_playerTransform.position,
					(new Vector3(_playerTransform.position.x, _elevatorStart.y, _playerTransform.position.z)),
					_time * Time.deltaTime);
			}
		}
	}
}