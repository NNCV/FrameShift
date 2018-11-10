using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;

public class FENodeManager : MonoBehaviour
{
    public RectTransform canvasRT;
    public List<FEFundamentalNode> allNodes;
    public FELink nodeLink;
    public Color[] dataTypeColors;
    public Dropdown nodeLister;

    public FEIOPort selectedPort;

    /*
    public void Start()
    {
        List<Dropdown.OptionData> optionsToSet = new List<Dropdown.OptionData>();

        for(int a = 0; a < allNodes.Count; a++)
        {
            optionsToSet.Add(new Dropdown.OptionData(allNodes[a].gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
        }

        nodeLister.options = optionsToSet;

        nodeLister.Show();
    }
    */

    public void AddNode(int index)
    {
     //   FEFundamentalNode nodeToInstantiate = allNodes[nodeLister.value];
        FEFundamentalNode nodeToInstantiate = allNodes[index];

        nodeToInstantiate.canvasRT = canvasRT;

        Vector3 tempPos = Input.mousePosition + new Vector3(0, 100f, 0) + nodeToInstantiate.nodeDragOffset * 36/Camera.main.orthographicSize;
        tempPos.z = canvasRT.position.z;
        nodeToInstantiate.finalPosition = Camera.main.ScreenToWorldPoint(tempPos);

        Instantiate(nodeToInstantiate, new Vector3(nodeToInstantiate.finalPosition.x, nodeToInstantiate.finalPosition.y, 1f), new Quaternion(0f,0f,0f, 0f), canvasRT);
        
       // nodeLister.gameObject.SetActive(false);
    }

    public void AddSchematic(string path)
    {
        string[] nodes;
    }

    public void SelectPort(FEIOPort port)
    {
        if(selectedPort == null)
        {
            selectedPort = port;
        }
        else
        {
            if(selectedPort != port)
            {
                LinkNode(selectedPort, port);
                selectedPort = null;
            }
            else
            {
                selectedPort = null;
            }
        }
    }

    public void LinkNode(FEIOPort port1, FEIOPort port2)
    {
        switch(port1.isInput)
        {
            case true:
                if (!port1.isLinked)
                {
                    switch (port2.isInput)
                    {
                        case true:
                            break;
                        case false:
                            if (port1.nodeDataType == port2.nodeDataType)
                            {
                                FELink newLink = nodeLink;
                                newLink.nodeInSlot = port2.ioNumber;
                                newLink.nodeOutSlot = port1.ioNumber;
                                newLink.firstPoint = port2.transform;
                                newLink.secondPoint = port1.transform;
                                newLink.nodeIn = port2.gameObject.GetComponentInParent<FEFundamentalNode>();
                                newLink.nodeOut = port1.gameObject.GetComponentInParent<FEFundamentalNode>();
                                port1.isLinked = true;

                                port1.totalLinks++;
                                port2.totalLinks++;

                                Color linkColor = GetCorrectColor(port2.nodeDataType);
                                newLink.GetComponent<LineRenderer>().startColor = linkColor;
                                newLink.GetComponent<LineRenderer>().endColor = linkColor;

                                Instantiate(newLink);
                            }
                            break;
                    }
                }
                break;
            case false:
                switch(port2.isInput)
                {
                    case true:
                        if (!port2.isLinked)
                        {   if (port1.nodeDataType == port2.nodeDataType)
                            {
                                FELink newLink = nodeLink;
                                newLink.nodeInSlot = port1.ioNumber;
                                newLink.nodeOutSlot = port2.ioNumber;
                                newLink.firstPoint = port1.transform;
                                newLink.secondPoint = port2.transform;
                                newLink.nodeIn = port1.gameObject.GetComponentInParent<FEFundamentalNode>();
                                newLink.nodeOut = port2.gameObject.GetComponentInParent<FEFundamentalNode>();
                                port2.isLinked = true;

                                port1.totalLinks++;
                                port2.totalLinks++;

                                Color linkColor = GetCorrectColor(port1.nodeDataType);
                                newLink.GetComponent<LineRenderer>().startColor = linkColor;
                                newLink.GetComponent<LineRenderer>().endColor = linkColor;

                                Instantiate(newLink);
                            }
                        }
                        break;
                    case false:
                        break;
                }
                break;
        }
    }

    
    //!
    //! Maybe in the future turn this into a gradient
    //! (when you implement cross-link interpretation)
    //!
    public Color GetCorrectColor(NodeDataType portReference)
    {
        /*
          Flux,
          Int,
          Float,
          String,
          Bool,
          Vector2,
          Vector3,
          Vector4,
          Quaternion
        */
        switch (portReference)
        {
            case NodeDataType.Flux:
                return dataTypeColors[1];
            case NodeDataType.Int:
                return dataTypeColors[2];
            case NodeDataType.Float:
                return dataTypeColors[3];
            case NodeDataType.String:
                return dataTypeColors[4];
            case NodeDataType.Bool:
                return dataTypeColors[5];
            case NodeDataType.Vector2:
                return dataTypeColors[6];
            case NodeDataType.Vector3:
                return dataTypeColors[7];
            case NodeDataType.Vector4:
                return dataTypeColors[8];
            case NodeDataType.Quaternion:
                return dataTypeColors[9];
        }

        return dataTypeColors[0];
    }

    public void HideDropdown()
    {/*
        nodeLister.Hide();
        nodeLister.gameObject.SetActive(false);
     */
    }

    public void ToggleDropdown()
    {
        /*
        nodeLister.gameObject.SetActive(true);
        nodeLister.Show();
        nodeLister.RefreshShownValue();

        nodeLister.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        */
    }
}
