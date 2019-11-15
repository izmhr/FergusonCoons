using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawControlLine : MonoBehaviour
{
    LineRenderer line;
    public Transform controlPoint;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, controlPoint.position);
    }
}
