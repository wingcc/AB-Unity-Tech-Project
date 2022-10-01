using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawGuiItmes : MonoBehaviour
{

    static public DrawGuiItmes instance { get { return g_Instance; } }
    static protected DrawGuiItmes g_Instance;
    [SerializeField] Button Exit,Serch;
    [SerializeField] InputField input;
    [SerializeField] TextMeshProUGUI MainNode;
    [SerializeField] RectTransform scrollContent;
    [SerializeField] NodeItme NodeItme;
    [SerializeField] conditionsItme conditionsItme;
    [SerializeField] SNodeItme SNodeItme;
    [SerializeField] nodeElementsItme nodeElementsItme;
    [SerializeField] SoundItme SoundItme;
    [SerializeField] CharacterItme CharacterItme;
    [SerializeField] CharacterAItme CharacterAItme;
    [SerializeField] elementsItme elementsItme;
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
    
    void Start()
    {
        Serch.onClick.AddListener(SerchById);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //--------------Draw LNodes-+conditionsItme---------------------->
    public void DrawNodesByID(int NodeId)
    {
        InSoundItme = false;
        ClearOldElement();
        
        for (int i = 0; i < DataManager.instance.MainNode.main.nodes.Count; i++)
        {
            if (DataManager.instance.MainNode.main.nodes[i].nodeId== NodeId)
            {
                if (DataManager.instance.MainNode.main.nodes[i].nodeType =="SNode")
                {
                    SNode SNode = new SNode(DataManager.instance.MainNode.main.nodes[i] as SNode);
                    SNodeItme s = Instantiate(SNodeItme, scrollContent);
                    s.Setup(SNode);
                    for (int DD = 0; DD < SNode.nodeElements.Count; DD++)
                    {
                        nodeElementsItme c = Instantiate(nodeElementsItme, scrollContent);
                        c.Setup(SNode.nodeElements[DD], NodeId,DD);
                    }
                    

                } else if (DataManager.instance.MainNode.main.nodes[i].nodeType == "LNode")
                {
                    NodeItme g = Instantiate(NodeItme, scrollContent);
                    g.Setup(DataManager.instance.MainNode.main.nodes[i]);
                    LNode lNode =new LNode( DataManager.instance.MainNode.main.nodes[i] as LNode,true);

                    foreach (var item in lNode.conditions)
                    {
                        conditionsItme c = Instantiate(conditionsItme, scrollContent);
                        c.Setup(item);
                    }
                }

                Exit.onClick.RemoveAllListeners();
                Exit.onClick.AddListener(DrawNodes);
            }
        }
        Exit.onClick.RemoveAllListeners();
        Exit.onClick.AddListener(DrawNodes);
    }
    //--------------Draw Sound + Character ----------------------->
    private bool isnodes,InSoundItme = false;
    public void DrawSoundItme(int NodeId,int objectId)
    {
       
        ClearOldElement();
        foreach (var item in DataManager.instance.MainNode.main.nodes)
        {
            if (item.nodeId == NodeId)
            {
                
                    SNode SNode = new SNode(item as SNode);
                    
                        if (SNode.nodeElements[objectId].elementType== "Sound")
                        {
                            NodeElement_Sound NodeElement = new NodeElement_Sound(SNode.nodeElements[objectId] as NodeElement_Sound);
                            
                                SoundItme S = Instantiate(SoundItme, scrollContent);
                                S.Setup(NodeElement);
                            
                        }
                        else if(SNode.nodeElements[objectId].elementType == "Character")
                        {
                             NodeElement_Character NodeElement = new NodeElement_Character(SNode.nodeElements[objectId] as NodeElement_Character);
                                try
                                {
                                    CharacterA ca = new CharacterA(NodeElement.character as CharacterA);
                                    if (ca.Origin[0].position.Count > 0)
                                    {
                                        CharacterAItme S = Instantiate(CharacterAItme, scrollContent);
                                        S.Setup(ca);
                                        foreach (var Sitem in ca.elements)
                                        {
                                            elementsItme EL = Instantiate(elementsItme, scrollContent);
                                            EL.Setup(Sitem);
                                
                                        }
                                    }
                                    else
                                    {
                                        CharacterItme S = Instantiate(CharacterItme, scrollContent);
                                        S.Setup(NodeElement);
                                        foreach (var Sitem in ca.elements)
                                        {
                                            elementsItme EL = Instantiate(elementsItme, scrollContent);
                                            EL.Setup(Sitem);

                                        }
                                    }
                                  }
                                catch (System.Exception)
                                {
                                    CharacterItme S = Instantiate(CharacterItme, scrollContent);
                                    S.Setup(NodeElement);
                                    foreach (var Sitem in NodeElement.character.elements)
                                    {
                                        elementsItme EL = Instantiate(elementsItme, scrollContent);
                                        EL.Setup(Sitem);

                                    }
                                }
                               

                                

                        }
                if (!InSoundItme)
                {
                    InSoundItme = true;
                    Exit.onClick.RemoveAllListeners();
                    Exit.onClick.AddListener(() => { DrawNodesByID(NodeId); });
                }
                else
                {
                    Exit.onClick.RemoveAllListeners();
                    Exit.onClick.AddListener(DrawNodes);
                }   
                
                       
            }
        }
    }
    //--------------DrawNodes- ----------------------->
    public void DrawNodes()
    {
        InSoundItme = false;
        MainNode.text = DataManager.instance.MainNode.mainName;
        ClearOldElement();
        for (int i = 0; i < DataManager.instance.MainNode.main.nodes.Count; i++)
        {
            NodeItme g = Instantiate(NodeItme, scrollContent);
            g.Setup(DataManager.instance.MainNode.main.nodes[i]);
        }
        Exit.onClick.RemoveAllListeners();
        Exit.onClick.AddListener(DrawNodes);
    }
    private void ClearOldElement()
    {

        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            Destroy(scrollContent.transform.GetChild(i).gameObject);
        }
    }
    //--------------Serch by Id ----------------------->

    public void SerchById()
    {
        ClearOldElement();
        string id = input.text;
        Debug.Log(id);
        foreach (var item in DataManager.instance.MainNode.main.nodes)
        {
            if (item.nodeId.ToString()== id)
            {
                NodeItme g = Instantiate(NodeItme, scrollContent);
                g.Setup(item);
                Debug.Log("NodeItme");
            }
            else
            {
                if (item.nodeType == "SNode")
                {
                    SNode SNode = new SNode(item as SNode);
                    foreach (var item2 in SNode.nodeElements)
                    {
                        if (item2.elementType == "Character")
                        {
                            NodeElement_Character NodeElement = new NodeElement_Character(item2 as NodeElement_Character);
                            if (NodeElement.character.id== id)
                            {
                                try
                                {
                                    CharacterA ca = new CharacterA(NodeElement.character as CharacterA);
                                    if (ca.Origin[0].position.Count > 0)
                                    {
                                        CharacterAItme S = Instantiate(CharacterAItme, scrollContent);
                                        S.Setup(ca);
                                       
                                    }
                                    else
                                    {
                                        CharacterItme S = Instantiate(CharacterItme, scrollContent);
                                        S.Setup(NodeElement);
                                        
                                    }
                                }
                                catch (System.Exception)
                                {
                                    CharacterItme S = Instantiate(CharacterItme, scrollContent);
                                    S.Setup(NodeElement);
                                    
                                }
                            }
                            else
                            {
                                foreach (var item3 in NodeElement.character.elements)
                                {
                                    if (item3.id==id)
                                    {
                                        try
                                        {
                                            CharacterA ca = new CharacterA(NodeElement.character as CharacterA);
                                            if (ca.Origin[0].position.Count > 0)
                                            {
                                                    elementsItme EL = Instantiate(elementsItme, scrollContent);
                                                    EL.Setup(item3);
                                            }
                                            else
                                            {
                                                elementsItme EL = Instantiate(elementsItme, scrollContent);
                                                EL.Setup(item3);

                                            }
                                        }
                                        catch (System.Exception)
                                        {
                                            elementsItme EL = Instantiate(elementsItme, scrollContent);
                                            EL.Setup(item3);

                                        }

                                    }
                                }
                            }
                        }
                        else if (item2.elementType == "Sound")
                        {
                            NodeElement_Sound NodeElement = new NodeElement_Sound(item2 as NodeElement_Sound);

                            if (NodeElement.sound.objectId==id)
                            {
                                SoundItme S = Instantiate(SoundItme, scrollContent);
                                S.Setup(NodeElement);
                            }
                           
                        }

                    }
                }
                else if(item.nodeType == "LNode")
                {
                    LNode lNode = new LNode(item as LNode, true);
                    if (lNode.nodeId.ToString()==id)
                    {
                        DrawNodesByID(lNode.nodeId);
                    }

 
                }
                
            }
        }
        Debug.Log("End Serch");
    }

}
