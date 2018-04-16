using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XmlManager : MonoBehaviour {

    public static XmlManager ins;

    void Awake ()
    {

        ins = this;

    }

    public Highscore newHighscore;

    public void Save ()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Highscore));
        FileStream stream = new FileStream(Application.persistentDataPath + "/highScore.xml", FileMode.Create);
        serializer.Serialize(stream, newHighscore);
        stream.Close();

    }

    public void Load ()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Highscore));
        FileStream stream = new FileStream(Application.persistentDataPath + "/highScore.xml", FileMode.Open);
        newHighscore = serializer.Deserialize(stream) as Highscore;
        stream.Close();

    }

}


[System.Serializable]
public class Highscore
{

    [XmlElement("Highscore")]
    public int highscore;


}


