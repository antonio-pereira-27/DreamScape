using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Lightning : MonoBehaviour
{
    // References
    private LineRenderer lineRenderer;
    private Vector2 midPoint;
    private Color colour = Color.white;
    private EdgeCollider2D edgeCollider;

    // Variables
    private int numberSegments = 12;

    private float maxZ = 5f;
    private float posRange = 0.15f;
    private float radius = 1f;

    private float damage = 0.05f;
    

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer.SetVertexCount(numberSegments);
        lineRenderer.alignment = LineAlignment.TransformZ;

        midPoint = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));

        for (int i = 1; i < numberSegments - 1; i++)
        {
            float z = ((float)i) * maxZ / (float)(numberSegments - 1);

            float x = -midPoint.x * z * z / 16f + z * midPoint.x / 2f;
            float y = -midPoint.y * z * z / 16f + z * midPoint.y / 2f;

            lineRenderer.SetPosition(i,
                new Vector3(
                x + Random.Range(-posRange, posRange),
                y + Random.Range(-posRange, posRange),
                z));
        }

        lineRenderer.SetPosition(0, new Vector3(0f, 0f, 0f));
        lineRenderer.SetPosition(numberSegments - 1, new Vector3(0f, 0f, 5f));

        
    }

    // Update is called once per frame
    void Update()
    {
        SetEdgeCollider(lineRenderer);
        colour.a -= 5f * Time.deltaTime;

        lineRenderer.SetColors(colour, colour);

        if (colour.a <= 0f)
            Destroy(gameObject);

        VerifyCollison();

       
    }

    void SetEdgeCollider(LineRenderer line)
    {
        List<Vector2> edges = new List<Vector2>();

        for (int i = 0; i < line.positionCount; i++)
        {
            Vector3 lineRendererPoint = line.GetPosition(i);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

    private void VerifyCollison()
    {
        float time = 0f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                time += Time.deltaTime;

                if (time >= 2f)
                {
                    hit.collider.gameObject.GetComponent<Enemy>().agent.isStopped = true;
                }
            }

            if (hit.collider.CompareTag("Boss"))
            {
                hit.collider.gameObject.GetComponent<Boss>().TakeDamage(damage);
                time += Time.deltaTime;

                if (time >= 2f)
                {
                    hit.collider.gameObject.GetComponent<Boss>().agent.isStopped = true;
                }
            }
        }
    }
}

