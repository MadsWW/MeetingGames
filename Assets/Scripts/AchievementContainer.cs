using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("AchievementCollection")]
public class AchievementContainer
{
    [XmlArray("Achievements")]
    [XmlArrayItem("Achievement")]
    public List<Achievement> Achievements = new List<Achievement>();
    public int Coins;
}
