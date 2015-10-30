using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void ChangeToScene (string targetScene) {
		Application.LoadLevel (targetScene);
	
	}
}
