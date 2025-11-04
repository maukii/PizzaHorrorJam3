using UnityEngine;

public class Pentagram : MonoBehaviour
{
    [Range(.01f, 10f)]
    [SerializeField] float lineWidth = 1f;
    [SerializeField] LineRenderer lr;
    [SerializeField] Transform[] points;


    void OnValidate()
    {
        
    }

    void Start()
    {
        lr.startWidth = lr.endWidth = lineWidth;
        lr.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
