using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int currentPoints;
    public int highScore;

    public UI_Controller UIController;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void GetPoints (int givenPoints)
    {

        currentPoints += givenPoints;
        UIController.UpdatePoints(currentPoints);

    }
}
