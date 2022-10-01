using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class elementsItme : MonoBehaviour
{
    public TextMeshProUGUI elementType;
    public TextMeshProUGUI animationId;
    public TextMeshProUGUI position;
    public TextMeshProUGUI rotation;
    public TextMeshProUGUI endTime;
    public TextMeshProUGUI loopCount;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI startTime;
    public TextMeshProUGUI id;
    
    // Start is called before the first frame update
    public void Setup(Element  element)

    {
        id.text = string.Format("id :{0}", element.id);
        elementType.text = string.Format("elementType :{0}", element.elementType);
        animationId.text = string.Format("animationId :{0}", element.animation.animationId);
        endTime.text = string.Format("endTime :{0}", element.animation.endTime);
        loopCount.text = string.Format("loopCount :{0}", element.animation.loopCount);
        Name.text = string.Format("Name :{0}", element.animation.name);
        startTime.text = string.Format("startTime :{0}", element.animation.startTime);
        position.text = string.Format("position :{0} {1} {2}", element.animation.destination.position[0],
            element.animation.destination.position[1], element.animation.destination.position[2]);
        rotation.text = string.Format("rotation :{0} {1} {2}", element.animation.destination.rotation[0],
            element.animation.destination.rotation[1], element.animation.destination.rotation[2]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
