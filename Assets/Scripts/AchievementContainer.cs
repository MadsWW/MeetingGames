using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("AchievementCollection")]
public class AchievementContainer
{
    [XmlArray("Achievements")]
    [XmlArrayItem("Achievement")]
    public List<Achievement> Achievements = new List<Achievement>();
}
