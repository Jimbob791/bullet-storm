using UnityEngine;

public class BoxPlatform : MonoBehaviour
{
    public GameObject boxPiece;
    public Vector2 spawnArea1;
    public Vector2 spawnArea2;

    private void Start()
    {
        // Generate width
        float width = Random.Range(3, 7);
        
        // Move to random spo
        transform.position = new Vector3 (
            Mathf.RoundToInt(Random.Range(spawnArea1.x + (width / 2), spawnArea2.x - (width / 2))) + 0.5f, 
            Mathf.RoundToInt(Random.Range(spawnArea1.y, spawnArea2.y)) + 0.5f, 
            0f
        );

        if (width % 2 == 0) { transform.position += new Vector3(0.5f, 0, 0); }

        // Spawn pieces
        for (int i = 0; i < width; i++)
        {
            GameObject newPiece = Instantiate(boxPiece, transform);
            newPiece.transform.localPosition = new Vector3 (i - (width / 2) + 0.5f, 0, 0);
        }

        // Set collider size
        GetComponent<BoxCollider2D>().size = new Vector2(width, 1);

        AstarPath.active.Scan();
    }
}
