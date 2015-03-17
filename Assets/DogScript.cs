using UnityEngine;
using System.Collections;

public class DogScript : MonoBehaviour {

	public GameObject target;
	public GameObject human;

	NavMeshAgent agent;
	enum States {WAIT, FETCH, RETURN};
	int currentState;
	int count;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		currentState = (int)States.WAIT;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentState == (int)States.WAIT){
			Wait();
		} else if(currentState == (int)States.FETCH){
			Fetch();
		} else if(currentState == (int)States.RETURN){
			Return();
		}
	}
	
	void Wait(){
		if(Input.GetKeyDown("f")){
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
			count++;
			if(count%3 == 0){
				int num = Application.loadedLevel + 2;
				Application.LoadLevel("level"+num);
			}
		} else {
			agent.destination = human.transform.position;
		}
	}
}
