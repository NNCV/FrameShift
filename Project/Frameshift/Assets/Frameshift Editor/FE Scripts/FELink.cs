using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FELink : MonoBehaviour
{
    public Transform firstPoint, secondPoint;
    public FEFundamentalNode nodeIn, nodeOut;
    public int nodeInSlot, nodeOutSlot;
    public object data;
    public LineRenderer lr;
    
    public static FELink FindLink(FEFundamentalNode nodeIN, FEFundamentalNode nodeOUT, int nodeINslot, int nodeOUTslot)
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("FELink"))
        {
            if(go.GetComponent<FELink>().nodeIn == nodeIN && go.GetComponent<FELink>().nodeOut == nodeOUT)
            {
                if(go.GetComponent<FELink>().nodeInSlot == nodeINslot && go.GetComponent<FELink>().nodeOutSlot == nodeOUTslot)
                {
                    return go.GetComponent<FELink>();
                }
            }
        }
        return null;
    }

    private void Start()
    {
        UpdateLinkDisplay();
    }

    public void UpdateLinkDisplay()
    {
        lr.SetPosition(0, new Vector3(firstPoint.position.x, firstPoint.position.y, firstPoint.position.z));
        lr.SetPosition(1, new Vector3(firstPoint.position.x + (secondPoint.position.x - firstPoint.position.x) / 2, firstPoint.position.y, firstPoint.position.z));
        lr.SetPosition(2, new Vector3(firstPoint.position.x + (secondPoint.position.x - firstPoint.position.x) / 2, secondPoint.position.y, firstPoint.position.z));
        lr.SetPosition(3, new Vector3(secondPoint.position.x, secondPoint.position.y, firstPoint.position.z));
    }

    public void Update()
    {
        UpdateLinkDisplay();
    }

    public void DestroyLink()
    {
        nodeOut.inputIO[nodeOutSlot].isLinked = false;
        nodeOut.inputIO[nodeOutSlot].totalLinks--;
        nodeIn.outputIO[nodeInSlot].totalLinks--;

        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        if(nodeIn == null || nodeOut == null)
        {
            nodeIn.outputIO[nodeInSlot].totalLinks--;
            nodeOut.inputIO[nodeOutSlot].totalLinks--;

            if (nodeIn == null)
            {
                nodeOut.inputIO[nodeOutSlot].isLinked = false;
            }

            Destroy(this.gameObject);
        }

        switch(nodeIn.nodeState)
        {
            case NodeState.Disabled:
                break;
            case NodeState.Flux:
                switch (nodeOut.nodeState)
                {
                    case NodeState.Disabled:
                        break;
                    case NodeState.Flux:
                        nodeIn.FluxFunction(nodeOut);
                        break;
                    case NodeState.Continuous:
                        nodeIn.FluxFunction(nodeOut);
                        break;
                }
                break;
            case NodeState.Continuous:
                switch (nodeOut.nodeState)
                {
                    case NodeState.Disabled:
                        break;
                    case NodeState.Flux:
                        nodeIn.ContinuousFunction(nodeOut);
                        break;
                    case NodeState.Continuous:
                        nodeIn.ContinuousFunction(nodeOut);
                        break;
                }
                break;
        }
    }

}
