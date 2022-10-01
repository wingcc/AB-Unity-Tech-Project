using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SoundItme : MonoBehaviour
{
    public TextMeshProUGUI objectName;
    public TextMeshProUGUI objectId;
    public TextMeshProUGUI audioType;
    public TextMeshProUGUI audioUrl;
    public TextMeshProUGUI loop;
    public TextMeshProUGUI volume;
    public TextMeshProUGUI pitch;
    public TextMeshProUGUI spatialMode;
    public TextMeshProUGUI minDistance;
    public TextMeshProUGUI maxDistance;
    public TextMeshProUGUI startTime;
    public TextMeshProUGUI endTime;
    private int _nodeId;
    private string _objectId;
    public Button Onclick;
    // Start is called before the first frame update
    public void Setup(NodeElement_Sound sound)
    {
   
           _objectId = sound.sound.objectId;
        objectName.text = string.Format("objectName :{0}", sound.sound.objectName);
        objectId.text = string.Format("objectId :{0}", sound.sound.objectId);
        audioType.text = string.Format("audioType :{0}", sound.sound.audioType);
        audioUrl.text = string.Format("audioUrl :{0}", sound.sound.audioUrl);
        loop.text = string.Format("loop :{0}", sound.sound.loop);
        volume.text = string.Format("volume :{0}", sound.sound.volume);
        pitch.text = string.Format("pitch :{0}", sound.sound.pitch);
        spatialMode.text = string.Format("spatialMode :{0}", sound.sound.spatialMode);
        minDistance.text = string.Format("minDistance :{0}", sound.sound.minDistance);
        maxDistance.text = string.Format("maxDistance :{0}",  sound.sound.maxDistance);
        startTime.text = string.Format("startTime :{0}", sound.sound.startTime);
        endTime.text = string.Format("endTime :{0}", sound.sound.endTime);
   
        Onclick.onClick.AddListener(Onclicked);
    }

    // Update is called once per frame
    void Onclicked()
    {
     //   DrawGuiItmes.instance.DrawNodesByID(_nodeId);
    }
}
