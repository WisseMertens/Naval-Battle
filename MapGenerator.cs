using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public int mapRadius;
    public Material sea;
    public Material land;

    [Range(0,1)]
    public float outlinePercent;
    [Range(0, 1)]
    public float landPercent;

    float sqrt3 = Mathf.Sqrt(3f);

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapholder = new GameObject(holderName).transform;
        mapholder.parent = transform;

        for ( int x = -mapRadius ; x < mapRadius + 1; x++)
        {
            for (int y = -mapRadius; y < mapRadius + 1;  y++)
            {
                if (Mathf.Abs(x+y) <= mapRadius)
                {
                    Vector3 tilePosition = new Vector3(sqrt3 * (x + 0.5f * y), 0, 1.5f * y);
                    Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90));
                    newTile.localScale = Vector3.one * (1 - outlinePercent);
                    var tileRenderer = newTile.GetComponent<Renderer>();
                    tileRenderer.material = sea;
                    newTile.name = "Hex_" + x + "_" + y;
                    newTile.parent = mapholder;
                }
            }
        }
    }
}
