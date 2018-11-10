using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FECameraManager : MonoBehaviour
{
    public Vector3 finalPosition;
    public Vector3 baseCameraPosition;
    public Vector3 baseCameraConvertedPosition;
    public float movementSpeed;
    public float movementLerpSpeed;

    private void Update()
    {
        if(Vector3.Distance(transform.position, finalPosition) <= 0.05f)
        {
            transform.position = finalPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * movementLerpSpeed);
        }
    }
    
    public void SetBaseCameraPosition()
    {
      //  baseCameraPosition = Input.mousePosition;
      //  baseCameraConvertedPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void DragFromCameraPosition()
    {
      //  Vector3 temp = Camera.main.ScreenToViewportPoint(Input.mousePosition - baseCameraPosition);
      //  finalPosition = new Vector3((temp.x) * movementSpeed + baseCameraConvertedPosition.x, (temp.y) * movementSpeed + baseCameraConvertedPosition.y, -10f);
       // finalPosition = baseCameraPosition + movementSpeed * Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
