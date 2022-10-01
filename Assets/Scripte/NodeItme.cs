using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NodeItme : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI nodeId;
    public TextMeshProUGUI nodeType;
    public Button Onclick ;
 
    private int _nodeId;
    // Start is called before the first frame update
    public void Setup(Node node)
    {
        _nodeId = node.nodeId;
        name.text = string.Format("Name :{0}", node.name);
        nodeId.text = string.Format("NodeId :{0}", node.nodeId);
        nodeType.text = string.Format("NodeType :{0}", node.nodeType);
        Onclick.onClick.AddListener(Onclicked);

    }

    // Update is called once per frame
    void Onclicked()
    {
        DrawGuiItmes.instance.DrawNodesByID(_nodeId);
    }
}
