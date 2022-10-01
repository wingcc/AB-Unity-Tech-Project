 
using TMPro;


public class CharacterAItme : CharacterItme
{
    public TextMeshProUGUI Position;
    public TextMeshProUGUI Rotition;
    public void Setup(CharacterA character)
    {
        id.text = string.Format("id :{0}",  character.id);
        Name.text = string.Format("Name :{0}",  character.name);
        startTime.text = string.Format("startTime :{0}", character.startTime);
        model.text = string.Format("model :{0}",  character.model);
        endTime.text = string.Format("endTime :{0}",  character.endTime);
        Position.text = string.Format("Position :{0} {1} {0}",  character.Origin[0].position[0],
            character.Origin[0].position[1], character.Origin[0].position[2]);
        Rotition.text = string.Format("Position :{0} {1} {0}", character.Origin[0].rotation[0],
            character.Origin[0].rotation[1], character.Origin[0].rotation[2]);
        Onclick.onClick.AddListener(Onclicked);
    }
    

}
