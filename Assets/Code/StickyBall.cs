using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBall : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.transform.CompareTag("StickyWall")) {
			var rigidbody = other.gameObject.GetComponent<Rigidbody>();
			if (rigidbody == null) {
				return;
			}
			var joint = gameObject.AddComponent<FixedJoint>();
			joint.connectedBody = rigidbody;
		}
	}
}
