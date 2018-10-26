using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code{
	public class WreckingBall : MonoBehaviour{

		[SerializeField] private float _ballForceBoost = 3f;
		[SerializeField] private float _ballForce = 1f;

		private Animator _characterAnimator;

		private void OnCollisionEnter(Collision other){
			if (other.gameObject.CompareTag("Player")){
				ApplyRagdoll(other.gameObject);
			}

			Rigidbody collidedRigidbody = other.gameObject.GetComponent<Rigidbody>();
			var vector = other.transform.position - transform.position;
			var direction = vector.normalized;
			collidedRigidbody.AddForce(direction * _ballForceBoost);

			List<Rigidbody> childRigidbodies = ReturnChildRigidbodies(other.gameObject);
			foreach (var rb in childRigidbodies){
				rb.AddForce(direction * _ballForce * _ballForceBoost);
			}
		}

		private void ApplyRagdoll(GameObject player){
			_characterAnimator = player.GetComponentInChildren<Animator>();
			_characterAnimator.enabled = false;
			StartCoroutine(ResetCharacter());
		}

		private IEnumerator ResetCharacter(){
			yield return new WaitForSeconds(3);
			_characterAnimator.enabled = true;
		}

		private static List<Rigidbody> ReturnChildRigidbodies(GameObject parentGameObject){
			var list = new List<Rigidbody>();
			GetChildRecursive(parentGameObject, list);
			return list;
		}

		private static void GetChildRecursive(GameObject obj, ICollection<Rigidbody> list){
			if (obj == null){
				return;
			}

			foreach (Transform child in obj.transform){
				if (child == null){
					continue;
				}

				var rigidbody = child.gameObject.GetComponent<Rigidbody>();
				if (rigidbody != null){
					list.Add(rigidbody);
				}

				GetChildRecursive(child.gameObject, list);
			}
		}
	}
}
