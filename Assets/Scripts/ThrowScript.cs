using UnityEngine;
using System.Collections;

public class ThrowScript : MonoBehaviour {
	public GameObject bone;
	public float strength;
	private bool isHolding;
	public Vector3 holdingOffset;

	void Start () {
		bone.transform.position = transform.position;
		isHolding = true;
	}

	void Update () {
		if (isHolding) {
			//hold bone above the player, ready to throw
			bone.transform.position = transform.position + holdingOffset;
			bone.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			if (Input.GetKey(KeyCode.F)) {
				throwBone();
			}
		} else {
			if(bone.transform.position.y < 0){
				float x = bone.transform.position.x % 20f;
				float z = bone.transform.position.z % 20f;
				bone.transform.position = new Vector3(x, 10, z);
			}
		}
	}

	private void throwBone(){
		Camera c = GetComponent<Camera>();
		Vector3 throwForce = c.transform.forward;
		bone.GetComponent<Rigidbody> ().AddForce (throwForce * strength);
		isHolding = false;
	}

	void onReturn(){
		Debug.Log ("got Bone");
		isHolding = true;
	}
}
