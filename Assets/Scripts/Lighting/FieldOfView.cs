using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    private float halfViewAngle;
    private int stepCount;
    private float stepAngleSize;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Transform rotatingTransform;

    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public float maskCutawayDistance = 0.1f;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    void Start()
    {
        halfViewAngle = viewAngle / 2;
        stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        stepAngleSize = viewAngle / stepCount;
        viewMesh = new Mesh();
        viewMesh.name = "View mesh";
        viewMeshFilter.mesh = viewMesh;
        if (rotatingTransform == null)
        {
            rotatingTransform = FindObjectOfType<Rotation>().transform;
        }

        StartCoroutine("FindTargetsWithDelay", 2f);
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        Queue<Vector2> viewPoints = new Queue<Vector2>();

        float angle = Rotation.getCurrentRotation() - halfViewAngle;
        ViewCastInfo oldViewCast = ViewCast(angle);
        ViewCastInfo newViewCast = new ViewCastInfo();
        viewPoints.Enqueue(oldViewCast.point);

        for (int i = 1; i <= stepCount; i++)
        {
            angle = Rotation.getCurrentRotation() - halfViewAngle + stepAngleSize * i;
            newViewCast = ViewCast(angle);
            
            //bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (oldViewCast.hit != newViewCast.hit)
            {
                EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                if (edge.pointA != Vector2.zero)
                {
                    viewPoints.Enqueue(edge.pointA);
                }
                if (edge.pointB != Vector2.zero)
                {
                    viewPoints.Enqueue(edge.pointB);
                }
            }

            viewPoints.Enqueue(newViewCast.point);
            oldViewCast = newViewCast;
            //Debug.DrawLine(transform.position, (Vector2)transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int j = 0; j < vertexCount - 1; j++)
        {
            vertices[j + 1] = transform.InverseTransformPoint(viewPoints.Dequeue()) + rotatingTransform.forward * maskCutawayDistance;

            if (j < vertexCount - 2)
            {
                triangles[j * 3] = 0;
                triangles[j * 3 + 1] = j + 1;
                triangles[j * 3 + 2] = j + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector2 minPoint = Vector2.zero;
        Vector2 maxPoint = Vector2.zero;
        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector2 dir = DirFromAngle(globalAngle, true);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);
        if (hit)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, (Vector2)transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 dirToTarget = ((Vector2)(target.position - transform.position)).normalized;
            if (Vector2.Angle (transform.forward, dirToTarget) < halfViewAngle)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector2 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector2 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _distance;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector2 pointA;
        public Vector2 pointB;

        public EdgeInfo(Vector2 _pointA, Vector2 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
