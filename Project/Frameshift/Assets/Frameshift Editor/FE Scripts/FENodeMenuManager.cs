using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FENodeMenuManager : MonoBehaviour
{
    public int depth;
    public float nmYThreshold;
    public FENodeManager nodeManger;
    public RectTransform nm;
    public Vector3 inactive, active;
    public Vector3 finalPosition;
    public bool isActive;
    public float speed;
    
    public void Update()
    {
        if(Input.mousePosition.y <= nmYThreshold)
        {
            finalPosition = active;
        }
        else
        {
            finalPosition = inactive;
        }

        if(Vector3.Distance(nm.transform.localPosition, finalPosition) <= 0.05f)
        {
            nm.transform.localPosition = finalPosition;
        }
        else
        {
            nm.transform.localPosition = Vector3.Lerp(nm.transform.localPosition, finalPosition, Time.deltaTime * speed);
        }
    }
}
