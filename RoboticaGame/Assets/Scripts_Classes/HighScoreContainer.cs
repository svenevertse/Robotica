using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("HighScoreContainer")]
public class HighScoreContainer{


    [XmlArray("Highscores"), XmlArrayItem("Highscore")]
    public List<Highscore> Highscore = new List<Highscore>();

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(HighScoreContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
            stream.Close();
        }
    }

    public static HighScoreContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(HighScoreContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as HighScoreContainer;
        }
    }

}
