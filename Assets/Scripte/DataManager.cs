using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using UnityEngine.Networking;



public class DataManager : MonoBehaviour
{
    static public DataManager instance { get { return g_Instance; } }
    static protected DataManager g_Instance;

    [SerializeField] [TextArea(2, 6)] private string JsonFileURL;
    [Space]
    [SerializeField] public  MainNode MainNode=new MainNode ();
    //---------------------------------->
    [Space(2)]
    public List<SNode> _SNode = new List<SNode>();
    public List<LNode> _LNode = new List<LNode>();
    public List<Character> _Character = new List<Character>();
    public List<Sound> _Sound = new List<Sound>();
    
    //---------------------------------->
    protected void Awake()
    {
        if (g_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            g_Instance = this;

        }
    }
    //--------------------------------->
    [ContextMenu("Start Data")]
    void Start()
    {
        StartCoroutine(GetJsonTextFromUrl());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------- GenerateJsonFile -------------------------------->
    [HideInInspector] public bool IsDataGinirated = false;

    [ContextMenu("Generate Data")]
    public void GenerateJsonFile()
    {
        string jsonPath = string.Format("{0}{1}", Application.dataPath, "/Main.json");
        var settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.None,
             
        };
        string jsonData = JsonConvert.SerializeObject(MainNode, Formatting.Indented, settings);
        
        if (jsonData == string.Empty)
        {
            IsDataGinirated = false;
            Debug.Log(string.Format("<color=#CE1158>[MainNode] The json Data Is Empte:{0} </color>", jsonData));
        }
        else
        {

            System.IO.File.WriteAllText(jsonPath, jsonData);
            IsDataGinirated = true;
            Debug.Log("MainNode is  Generate To Json File: ");
            Debug.Log(string.Format("<color=#1ED0A5>[MainNode] The Json Path:{0}</color>", jsonPath));
        }

    }
    //---------------------------------------- GetJsonTextFromUrl ------------------------------>
    [HideInInspector] public bool isDataLoaded;
    [ContextMenu("GetJson Data")]
    public  IEnumerator GetJsonTextFromUrl()
    {
        UnityWebRequest request = new UnityWebRequest();
        var Root = new Root.Root();
        if (string.IsNullOrEmpty(JsonFileURL))
        {
            Debug.Log("<color=#CE1158>[MainNode] Error: Your Url Is Empty, .</color>");
            isDataLoaded = false;
        }
        else
        {
            request = UnityWebRequest.Get(DirectLinkGenerator(JsonFileURL));
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogFormat("<color=#CE1158>[AdsDATA] Error:{0} , .</color>", request.error);
                isDataLoaded = false;
            }
            else
            {
                isDataLoaded = true;
                try
                {
                   
                    Debug.Log(request.downloadHandler.text);
                    Root = JsonConvert.DeserializeObject<Root.Root>(request.downloadHandler.text);
                    Debug.Log("<color=orange>[MainNode]:</color><color=#00796b> MainNode DATA  Is loaded successfully from server.</color>");
                    isDataLoaded = true;

                }
                catch (Exception e)
                {
                    Debug.LogFormat("<color=#CE1158>[MainNode] Error:{0} ,</color>", e.Message);
                    isDataLoaded = false;
                }

            }
        }
        request.Dispose();
        if (isDataLoaded)
        {
          //  MainNode = new MainNode();
            MainNode.mainName = Root.mainName;
            MainNode.main.nodes = new List<Node>();
            foreach (var item in Root.main.nodes)
            {
                if (item.nodeType == "SNode")
                {
                    SNode an= new SNode(item);
                    MainNode.main.nodes.Add(an);
                    _SNode.Add(an);
                }
                else if (item.nodeType == "LNode")
                {
                    LNode ln=new LNode(item);
                    MainNode.main.nodes.Add(ln);
                    _LNode.Add(ln);
                }
            }
            yield return new WaitForEndOfFrame();
            DrawGuiItmes.instance.DrawNodes();

        }

    }


   
    //----------------------------------->


    #region Direct Link Generator
    // #ff4455 Google Drive default preview link :
    //https://drive.google.com/file/d/FILE_ID/view?usp=sharing

    //#33aa55 Google Drive direct download link :
    //https://drive.google.com/uc?export=download&id=FILE_ID 


    public string DirectLinkGenerator(string Url)
    {
        string Endindex = "/view?usp=sharing";
        string Startindex = "https://drive.google.com/file/d/";

        string piece = Url.Substring(Startindex.Length, (Url.Length - (Startindex.Length + Endindex.Length)));
        return string.Format("https://drive.google.com/uc?export=download&id={0}", piece);
        //Debug.Log(DirectLink);

    }
    #endregion
}



//-----------------Root Data------------------>
[System.Serializable]

public class Animation
{
    public string animationId;
    public Destination destination=new Destination ();
    public int endTime;
    public int loopCount;
    public string name;
    public int startTime;
}
[System.Serializable]
public class Character
{
    public string id;
    public string name;
    public int startTime;
    public int endTime;
    public string model;
    public List<Element> elements = new List<Element>();

    public Character()
    {

    }
    public Character(string id, string name, int startTime, int endTime, string model, List<Element> elements)
    {
        this.id= id;
        this.name= name;
        this.startTime= startTime;
        this.endTime= endTime;
        this.model= model;
        this.elements = elements;
    }

    public Character(Root.Character characterA)
    {

        this.id = characterA.id;
        this.name = characterA.name;
        this.startTime = characterA.startTime;
        this.endTime = characterA.endTime;
        this.model = characterA.model;
        this.elements = JsonConvert.DeserializeObject<List<Element>>(JsonConvert.SerializeObject(characterA.elements));
    }
   
}
[System.Serializable]
public class CharacterA: Character
{
    public List<Origin> Origin=new List<Origin>();
    public CharacterA(string id, string name, int startTime, int endTime, string model, List<Element> elements, List<Origin> Origin)
        :base(id, name, startTime, endTime, model, elements)
    {
        this.Origin = Origin;
    }
   
    public CharacterA(CharacterA characterA)
        : this(characterA.id, characterA.name, characterA.startTime, characterA.endTime, characterA.model
             , characterA.elements, characterA.Origin){ }

    public CharacterA(Root.Character characterA) 
    {
        
        this.id = characterA.id;
        this.name = characterA. name;
        this.startTime = characterA.startTime;
        this.endTime = characterA.endTime;
        this.model = characterA.model;

        this.elements = JsonConvert.DeserializeObject<List<Element>>(JsonConvert.SerializeObject(characterA.elements));
        if (characterA.origin != null)
        {
            this.Origin.Add(new Origin( characterA.origin.position, characterA.origin.rotation));
        }

    }
 
}

[System.Serializable]
public class Condition
{
    public string conditionType;
    public Event @event;

    public static explicit operator Root.Condition(Condition obj)
    {
        return JsonConvert.DeserializeObject<Root.Condition>(JsonConvert.SerializeObject(obj));
    }
    public Condition(Root.Condition condition)
    {

    }

}
[System.Serializable]
public class Destination
{
    public List<int> position=new List<int> ();
    public List<int> rotation=new List<int> ();
}
[System.Serializable]
public class Element
{
    public string elementType;
    public Animation animation;
    public string id;
    public Element()
    {

    }
}
[System.Serializable]
public class Event
{
    public int desiredEventValue;
    public string eventType;
}
[System.Serializable]
public class Main
{
    public List<Node> nodes=new List<Node> ();

}
[System.Serializable]
public class Node
{
    public string name;
    public int nodeId;
    public string nodeType;

    public Node()
    {

    }
    public Node(string name, int nodeId, string nodeType)
    {
        this.name = name;
        this.nodeId = nodeId;
        this.nodeType = nodeType;
    }
}
[System.Serializable]

public class SNode : Node
{
    public int nextNodeID;
    public int startTime;
    public int endTime;
    public Origin origin=new Origin ();
    public List<NodeElement> nodeElements=new List<NodeElement> ();
    public SNode(string name, int nodeId, string nodeType, int nextNodeID,
                int startTime, int endTime)
                : base(name, nodeId, nodeType)
    {
        this.nextNodeID = nextNodeID;
        this.startTime = startTime;
        this.endTime = endTime;
    }
    public SNode(string name, int nodeId, string nodeType, int nextNodeID,
                int startTime, int endTime, Origin origin, List<NodeElement> nodeElements) 
                : base(name, nodeId, nodeType)
    {
        this.nextNodeID = nextNodeID;
        this.startTime = startTime;
        this.endTime = endTime;
        this.origin = origin;
        this.nodeElements = nodeElements;
    }

    public SNode(SNode sNode):this(sNode.name, sNode.nodeId, sNode.nodeType, sNode.nextNodeID
        , sNode.startTime, sNode.endTime, sNode.origin, sNode.nodeElements)
    {

    }
    public SNode(Root.Node sNode) : this(sNode.name, sNode.nodeId, sNode.nodeType, sNode.nextNodeID
      , sNode.startTime, sNode.endTime)
    {
        //this.origin =new Origin(sNode.origin);
        // we can use constracter or json 
        this.origin= JsonConvert.DeserializeObject<Origin>(JsonConvert.SerializeObject(sNode.origin));


        foreach (var item in sNode.nodeElements)
        {
            if (item.elementType== "Character")
            {
                NodeElement_Character ns = new NodeElement_Character(item);
                this.nodeElements.Add(ns);
                DataManager.instance._Character.Add(ns.character);
               
               
            }
            else if (item.elementType == "Sound")
            {
                NodeElement_Sound ns= new NodeElement_Sound(item);
                this.nodeElements.Add(ns);
                 DataManager.instance._Sound.Add(ns.GetSounds());
              
            }

        }

       
    }

}
[System.Serializable]

public class LNode:Node
{
    public List<Condition> conditions=new List<Condition> ();

    public LNode(string name, int nodeId, string nodeType, List<Condition> conditions) : base(name, nodeId, nodeType)
    {
        this.conditions = conditions;
    }
    public LNode(string name, int nodeId, string nodeType) : base(name, nodeId, nodeType)
    {
    }
    public LNode(LNode lNode):this(lNode.name, lNode.nodeId, lNode.nodeType, lNode.conditions)
    {

    }
    public LNode(Root.Node lNode) : this(lNode.name, lNode.nodeId, lNode.nodeType)
    {
       this.conditions = JsonConvert.DeserializeObject<List<Condition>>(JsonConvert.SerializeObject(lNode.conditions));
    }
    public LNode(LNode Node,bool Action) : this(Node.name, Node.nodeId, Node.nodeType)
    {
        this.conditions = JsonConvert.DeserializeObject<List<Condition>>(JsonConvert.SerializeObject(Node.conditions));
    }
}


[System.Serializable]
public class NodeElement
{
    public string elementType;
    public NodeElement(string elementType)
    {
        this.elementType = elementType;
    }

    public virtual Sound GetSounds()
    {
        return null;
    }
}

[System.Serializable]
public class NodeElement_Sound : NodeElement
{
    public Sound sound=new Sound ();

    public NodeElement_Sound(string elementType) : base(elementType) { }
    public NodeElement_Sound(Root.NodeElement nodeElement):base(nodeElement.elementType)
    {
        this.sound= JsonConvert.DeserializeObject<Sound>(JsonConvert.SerializeObject(nodeElement.sound));
    }
    public NodeElement_Sound(NodeElement_Sound nodeElement_Sound) : base(nodeElement_Sound.elementType)
    {
        this.sound = nodeElement_Sound.sound;
    }

    public override Sound GetSounds()
    { return this.sound; }

}
[System.Serializable]
public class NodeElement_Character : NodeElement
{
    public Character character  =new Character ();
     
    public NodeElement_Character(string elementType) : base(elementType) { }

    public NodeElement_Character(Root.NodeElement nodeElement) : base(nodeElement.elementType)
    {
        if (nodeElement.character.origin.position.Count > 0|| nodeElement.character.origin.rotation.Count>0)
        {
            this.character = new CharacterA(nodeElement.character);
        }
        else
        {
            this.character = new Character(nodeElement.character);
        }        
        
    }

    public NodeElement_Character(NodeElement_Character nodeElement_Character) : base(nodeElement_Character.elementType)
    {
        this.character = nodeElement_Character.character;
    }
    public Character GetCharacters()
    {
        return this.character;
    }
}


[System.Serializable]
public class Origin
{
    public List<int> position;
    public List<int> rotation;
    public Origin() { }
    
    public Origin(List<int> position, List<int> rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
   
    public Origin(Root.Origin origin):this(origin.position, origin.rotation){}

    
}
[System.Serializable]
public class MainNode
{
    public string mainName;
    public Main main;
}
[System.Serializable]
public class Sound
{
    public string objectName;
    public string objectId;
    public string audioType;
    public string audioUrl;
    public bool loop;
    public int volume;
    public int pitch;
    public string spatialMode;
    public int minDistance;
    public int maxDistance;
    public int startTime;
    public int endTime;
}
 
