using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://t-pot.com/program/2_3rdcurve/index.html
public class FergusonCoons : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    Transform c1;
    Transform c2;
    public GameObject pointPrefab;
    public int RESO = 30;
    public float veloGain = 10.0f;
    public Transform parent;

    void Start()
    {
        c1 = p1.Find("control");
        c2 = p2.Find("control");
    }

    void Update()
    {
        Create();
    }

    Vector3 Calc(float t)
    {
        if (t < 0.0f) t = 0.0f;
        if (t > 1.0f) t = 1.0f;

        Vector4 p1_ = new Vector4(p1.position.x, p1.position.y, p1.position.z, 1.0f);
        Vector4 p2_ = new Vector4(p2.position.x, p2.position.y, p2.position.z, 1.0f);


        Vector4 v1 = veloGain * (c1.position - p1.position);
        v1.w = 1.0f;
        Vector4 v2 = veloGain * (c2.position - p2.position);
        v2.w = 1.0f;

        Vector4 T = new Vector4(2 * t * t * t - 3 * t * t + 1,
                                    -2 * t * t * t + 3 * t * t,
                                    t * t * t - 2 * t * t + t,
                                    t * t * t - t * t);
        Matrix4x4 V = new Matrix4x4(p1_, p2_, v1, v2);

        Vector4 x = V * T;

        return x;
    }

    void Create()
    {
        foreach(Transform obj in parent)
        {
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < RESO; i++)
        {
            float r = 1.0f / (float)RESO * i;
            var newobj = Instantiate(pointPrefab, Calc(r), Quaternion.identity);
            newobj.transform.SetParent(parent);
        }
    }
}
