using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Highscore {
    
    [XmlElement("Highscore")]
    public int highscore;

}
