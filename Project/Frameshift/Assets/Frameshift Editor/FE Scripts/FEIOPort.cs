using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum NodeDataType
{
    Flux,
    Int,
    Float,
    String,
    Bool,
    Vector2,
    Vector3,
    Vector4,
    Quaternion
}

public class FEIOPort : MonoBehaviour, IPointerClickHandler
{
    public NodeDataType nodeDataType;
    public int ioNumber;
    public bool isInput, isLinked;
    public int totalLinks;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            FindObjectOfType<FENodeManager>().SelectPort(this);
            if(FindObjectOfType<FENodeManager>().selectedPort == null)
            {
                GetComponent<Button>().OnDeselect(eventData);
            }

            //probably should mention ioNumber to SelectPort() as a parameter
            //maybe we should use it
        }
    }
}
