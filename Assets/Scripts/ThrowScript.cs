using UnityEngine;
using System.Collections;

public class ThrowScript : MonoBehaviour {
	public GameObject bone;
	public float strength;
	public float lowerForceLimit;
	public float forceBarSpeed;
	private bool isHolding;
	public Vector3 holdingOffset;
	private float forceMultiplier = (float).3;
	private bool firstPress = false;
	private bool goingUp = true;

	void Start () {
		bone.transform.position = transform.position;
		isHolding = true;
	}

	void Update () {
		if (isHolding) {
			//hold bone above the player, ready to throw
			bone.transform.position = transform.position + holdingOffset;
			bone.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			if (Input.GetKeyDown(KeyCode.F)) {
				firstPress = !firstPress;
				if(!firstPress){
					throwBone();
				}
			}
			if (firstPress){
				goForceMeter ();
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
		bone.GetComponent<Rigidbody> ().AddForce (throwForce * (strength * forceMultiplier));
		isHolding = false;
		forceMultiplier = 0;
		FindObjectOfType<DogScript> ().SendMessage ("onBoneThrow");
	}

	//forceMultiplier will bounce between lowerForceLimit and 1 at a rate of forceBarSpeed per frame 
	private void goForceMeter(){
		if(forceMultiplier > 1){
			goingUp = false;
		}
		if (forceMultiplier < lowerForceLimit){
			goingUp = true;
		}
		if (goingUp){
			forceMultiplier = forceMultiplier + forceBarSpeed;
		}else{
			forceMultiplier = forceMultiplier - forceBarSpeed;
		}
	}

	void onReturn(){
		Debug.Log ("got Bone");
		isHolding = true;
	}
}
