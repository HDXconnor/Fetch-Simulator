using UnityEngine;
using System.Collections;


public class DogScript : MonoBehaviour {
	private NavMeshAgent agent;
	private enum States {WAIT, FETCH, RETURN};
	private int currentState, throwCount;
	public GameObject target, human;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		agent = GetComponent<NavMeshAgent>();
		currentState = (int)States.WAIT;
		throwCount = 0;

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.F2)) {
			RestartGame();
		}

		if(currentState == (int)States.WAIT){
			Wait();
		} else if(currentState == (int)States.FETCH){
			Fetch();
		} else if(currentState == (int)States.RETURN){
			Return();
		}
	}
	
	void Wait(){
		if(Input.GetKeyDown(KeyCode.F)){
			agent.destination = target.transform.position;
			currentState = (int)States.FETCH;
		} else if(Mathf.Abs((transform.position - human.transform.position).magnitude) > 2){
			agent.destination = human.transform.position;
		} else {
			agent.destination = transform.position;
		}
	}
	
	void Fetch(){
		if(Mathf.Abs((transform.position - target.transform.position).magnitude) < 2){
			agent.destination = human.transform.position;
			currentState = (int)States.RETURN;
		} else {
			agent.destination = target.transform.position;
		}
	}
	
	void Return(){
		if(Mathf.Abs((transform.position - human.transform.position).magnitude) < 2){
			currentState = (int)States.WAIT;
			throwCount++;

			//send a message saying that the bone has been returned
			human.SendMessage("onReturn");

			if(throwCount%3 == 0){
				int num = Application.loadedLevel + 2;
				Application.LoadLevel("level"+num);
			}
		} else {
			//hold the bone above the dog as its walking back
			target.transform.position = transform.position + new Vector3(0, 2, 0);
			agent.destination = human.transform.position;
		}
	}

	void RestartGame(){
		Application.LoadLevel("level1");
	}
}
