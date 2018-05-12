using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("CardInfoCollection")]
public class CardInfoContainer
{
    [XmlArray("CardBackInfos")]
    [XmlArrayItem("CardBackInfo")]
    public List<CardInfo> CardBackInfo = new List<CardInfo>();
    public List<CardInfo> CardFrontInfo = new List<CardInfo>();
}
