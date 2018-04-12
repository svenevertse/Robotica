using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class HighscoreTest : MonoBehaviour {

    HighScoreContainer hsContainer;
    public GameManager gm;

	void Start () {

        hsContainer = HighScoreContainer.Load(Path.Combine(Application.dataPath, "Saves/highScore.xml"));

    }
	

	void Update () {

        if (Input.GetButtonDown("HSTS"))
        {

            hsContainer.Save(Path.Combine(Application.dataPath, "Saves/highScore.xml"));
            print(gm.hsSerializer.highscore);

        }
		
	}
}
