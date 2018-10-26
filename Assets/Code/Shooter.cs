using UnityEngine;

namespace Code {
	public class Shooter : MonoBehaviour {

		[SerializeField] Camera camera;
		[SerializeField] GameObject bulletPrefab;
		[SerializeField] float force;
		float distance = 10f;

		void Update(){
			if(Input.GetMouseButtonDown(0))
				Shoot();
		}

		void Shoot(){
			var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
			position = camera.ScreenToWorldPoint(position);
			var bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
			bulletInstance.transform.LookAt(position);
			var bulletRigidBody = bulletInstance.GetComponent<Rigidbody>();
			bulletRigidBody.AddForce(bulletInstance.transform.forward * force);
		}
	}
}
