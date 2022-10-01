
using System.Collections.Generic;

namespace Root
{
    [System.Serializable]
    public class Root
    {
        public string mainName;
        public Main main;
    }
    //--------------------------------------->
    [System.Serializable]
 
public class Animation
{
        public string animationId;
        public Destination destination;
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
        public List<Element> elements;
        public Origin origin=new Origin();
}
[System.Serializable]
public class Condition
{
        public string conditionType;
        public Event @event;
}
[System.Serializable]
public class Destination
{
        public List<int> position;
        public List<int> rotation;
}
[System.Serializable]
public class Element
{
        public string elementType;
        public Animation animation;
        public string id;
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
        public List<Node> nodes;
}
[System.Serializable]
public class Node
{
        public string name;
        public int nodeId;
        public string nodeType;
        public int nextNodeID;
        public int startTime;
        public int endTime;
        public Origin origin;
        public List<NodeElement> nodeElements;
        public List<Condition> conditions;
}
[System.Serializable]
public class NodeElement
{
        public string elementType;
        public Sound sound;
        public Character character;
}
[System.Serializable]
public class Origin
{
        public List<int> position=new List<int> ();
        public List<int> rotation = new List<int>();
        public Origin()
        {

        }
        public Origin(List<int> position, List<int> rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }


    }

[System.Serializable]
public class Sound
{
        public string objectName;
        public string objectId ;
        public string audioType;
        public string audioUrl;
        public bool loop ;
        public int volume;
        public int pitch;
        public string spatialMode;
        public int minDistance;
        public int maxDistance;
        public int startTime;
        public int endTime;
}
}


