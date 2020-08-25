using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{

    public LineRenderer lineLeftRender;
    public LineRenderer lineRightRender;


    public Material Mat;

    private void Start()
    {
        Mat = GetComponent<MeshRenderer>().materials[0];
        //lineRender = GetComponent<LineRenderer>();
    }


    public void SetChoosen()
    {
        Mat.SetColor("_Color", Color.green);
    }

   public void SetParent()
   {
        // lineRender.SetPositions(new Vector3[] { transform.position, point});
        Mat.SetColor("_Color",Color.red);
   }
   public void SetLeft(Vector3 point)
   {
        lineLeftRender.materials[0].SetColor("_Color", Color.green);
        lineLeftRender.SetPositions(new Vector3[] { transform.position, point });
   }
   public void SetRight(Vector3 point)
   {
        lineRightRender.materials[0].SetColor("_Color", Color.blue);
        lineRightRender.SetPositions(new Vector3[] { transform.position, point });
   }

    public void ResetItem()
    {
        lineLeftRender.materials[0].SetColor("_Color", Color.white);
        lineRightRender.materials[0].SetColor("_Color", Color.white);
        lineRightRender.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        lineLeftRender.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        Mat.SetColor("_Color", Color.white);
    }
}
