using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NodeType
{
    UndefinedNode,
    FluxNode,
    DataNode,
    TestNode
}

public enum NodeState
{
    Disabled,
    Flux,
    Continuous
}

public class FEFundamentalNode : MonoBehaviour
{
    //Functional
    public NodeType nodeType;
    public NodeState nodeState;
    public float fluxCurrent, fluxMaximum;
    public Image fluxBarCurrent;
    public float fluxBarMaximumWidth;
    public FEIOPort[] inputIO, outputIO;

    //Visual
    public Vector3 nodeDragOffset;
    public Vector2 finalPosition;
    public RectTransform canvasRT;
    public float dragSpeed;
    
    public void DedstroyNode()
    {
        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, finalPosition) <= 0.05f)
        {
            transform.position = finalPosition;
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, finalPosition, Time.deltaTime * dragSpeed);
        }
    }

    public virtual void Update()
    {
        UpdateFluxBar();
    }

    public void UpdateFluxBar()
    {
        if(fluxCurrent > fluxMaximum)
        {
            fluxCurrent = fluxMaximum;
        }
        else if(fluxCurrent < 0)
        {
            fluxCurrent = 0;
        }
        fluxBarCurrent.rectTransform.sizeDelta = new Vector2((fluxCurrent / fluxMaximum) * fluxBarMaximumWidth, fluxBarCurrent.rectTransform.sizeDelta.y);
    }

    public virtual void FluxFunction(FEFundamentalNode output) { }

    public virtual void ContinuousFunction(FEFundamentalNode output) { }

    public void MoveToMouse()
    {
        Vector3 tempPos = Input.mousePosition + nodeDragOffset * 36/Camera.main.orthographicSize;
        tempPos.z = canvasRT.position.z;
        finalPosition = Camera.main.ScreenToWorldPoint(tempPos);
    }
}
