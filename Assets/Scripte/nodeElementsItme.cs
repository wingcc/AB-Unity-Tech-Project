using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class nodeElementsItme : MonoBehaviour
{
    public TextMeshProUGUI elementType;
    public Button Onclick;
    private int _nodeId;
    private int _index;
    // Start is called before the first frame update
    public void Setup(NodeElement  nodeElement, int nodeId,int index)
    {
        _nodeId = nodeId;
        this._index = index;
        elementType.text = string.Format("Name :{0}", nodeElement.elementType);
        Onclick.onClick.AddListener(Onclicked);
    }

    // Update is called once per frame
    void Onclicked()
    {
        DrawGuiItmes.instance.DrawSoundItme(_nodeId, _index);
    }
}
