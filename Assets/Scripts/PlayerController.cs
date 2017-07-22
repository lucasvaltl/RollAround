using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text gameText;
	public GameObject success;

	private Rigidbody rb;
	private int count;
	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private bool ingame;

	void Start ()
	{
	 rb = GetComponent<Rigidbody>();
		count = 0;
		setCountText ();
		gameText.text = "";
		winText.text = "0";
		ingame = true;
	}

	void FixedUpdate ()
	{
		if (checkIfFallen ()) {
			ingame = false;
			winText.text = "0";
			StartCoroutine (reset());
		}
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
		if (ingame && Time.timeSinceLevelLoad >= 0) {
			winText.text = Time.timeSinceLevelLoad.ToString("n2");
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count++;
			setCountText ();
		}

	}

	void setCountText(){
		countText.text = "Count: " + count;
		if (count >=40){
			ingame = false;
			success.gameObject.SetActive (true);
			StartCoroutine (openURL ());
		}
	}

	IEnumerator reset() {
		gameText.text = "Game Over :(\nTry collecting all the spheres without falling! :) ";
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
	}

	IEnumerator openURL() {
		yield return new WaitForSeconds(6f);
		//potentially call a web adress to upload highscore and open high score table
		//Application.OpenURL("");
	}

	bool checkIfFallen(){
		return transform.position.y < -5;
	}

	void delay (float t){
		float start = Time.realtimeSinceStartup;
		while ((Time.realtimeSinceStartup - start)< t) {
			
		}
	}
}
