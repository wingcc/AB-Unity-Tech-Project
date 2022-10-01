using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SNodeItme : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI nodeId;
    public TextMeshProUGUI nodeType;
    public TextMeshProUGUI nextNodeID;
    public TextMeshProUGUI startTime;
    public TextMeshProUGUI endTime;
    public TextMeshProUGUI position;
    public TextMeshProUGUI rotation;
    public Button Onclick;

    private int _nodeId;
    // Start is called before the first frame update
    public void Setup(SNode node)
    {
        _nodeId = node.nodeId;
        name.text = string.Format("Name :{0}", node.name);
        nodeId.text = string.Format("NodeId :{0}", node.nodeId);
        nodeType.text = string.Format("NodeType :{0}", node.nodeType);
        nextNodeID.text = string.Format("nextNodeID :{0}", node.nextNodeID);
        startTime.text = string.Format("startTime :{0}", node.startTime);
        endTime.text = string.Format("endTime :{0}", node.endTime);
        position.text = string.Format("position :[ {0}, {1}, {2}]", node.origin.position[0], node.origin.position[1], node.origin.position[2]);
        rotation.text = string.Format("rotation :[ {0}, {1}, {2}]", node.origin.rotation[0], node.origin.rotation[1], node.origin.rotation[2]);

        Onclick.onClick.AddListener(Onclicked);

    }

    // Update is called once per frame
    void Onclicked()
    {
        DrawGuiItmes.instance.DrawNodesByID(_nodeId);
    }
}
