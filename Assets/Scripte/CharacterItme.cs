using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class CharacterItme : MonoBehaviour
{
    public TextMeshProUGUI id;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI startTime;
    public TextMeshProUGUI endTime;
    public TextMeshProUGUI model;
 
    public Button Onclick;
    private int _nodeId;
    private string _objectId;
    // Start is called before the first frame update
    public void Setup(NodeElement_Character NodeElement)
    {
        id.text = string.Format("id :{0}", NodeElement.character.id);
        Name.text = string.Format("Name :{0}", NodeElement.character.name);
        startTime.text = string.Format("startTime :{0}", NodeElement.character.startTime);
        model.text = string.Format("model :{0}", NodeElement.character.model);
        endTime.text = string.Format("endTime :{0}", NodeElement.character.endTime);
 

        Onclick.onClick.AddListener(Onclicked);
    }

    // Update is called once per frame
   public  void Onclicked()
    {
        //   DrawGuiItmes.instance.DrawNodesByID(_nodeId);
    }
}
