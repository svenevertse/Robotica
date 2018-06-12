using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// Manager die de highscore opslaat en laad.
/// </summary>
public class XmlManager : MonoBehaviour {

    public static XmlManager ins;

    void Awake ()
    {

        ins = this;

    }

    public Highscore newHighscore;

    /// <summary>
    /// Functie dat de Highscore naar het XML bestand streamt. Word elke keer uitgevoerd wanneer de speler een nieuwe Highscore behaalt.
    /// Er is gekozen voor de persistent data pad zodat de speler moeilijker bij het bestand kan komen om eventueel vals te spelen 
    /// </summary>
    public void Save ()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Highscore));
        FileStream stream = new FileStream(Application.persistentDataPath + "/highScore.xml", FileMode.Create);
        serializer.Serialize(stream, newHighscore);
        stream.Close();

    }

    /// <summary>
    /// Functie die de highscore laad en deserialized van het XML bestand.
    /// </summary>
    public void Load ()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Highscore));
        FileStream stream = new FileStream(Application.persistentDataPath + "/highScore.xml", FileMode.Open);
        newHighscore = serializer.Deserialize(stream) as Highscore;
        stream.Close();

    }

}


/// <summary>
/// class met de Highscore variable die opgeslagen word
/// </summary>
[System.Serializable]
public class Highscore
{

    [XmlElement("Highscore")]
    public int highscore;


}


