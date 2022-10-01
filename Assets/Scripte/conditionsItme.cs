using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class conditionsItme : MonoBehaviour
{
    public TextMeshProUGUI conditionType;
    public TextMeshProUGUI desiredEventValue;
    public TextMeshProUGUI eventType;

    public Button Onclick;
    // Start is called before the first frame update
    public void Setup(Condition condition)
    {
        conditionType.text= string.Format("Name :{0}", condition.conditionType);
        desiredEventValue.text = string.Format("Name :{0}", condition.@event.desiredEventValue);
        eventType.text = string.Format("Name :{0}", condition.@event.eventType);

        Onclick.onClick.AddListener(Onclicked);
    }

    // Update is called once per frame
    void Onclicked()
    {

    }
}
