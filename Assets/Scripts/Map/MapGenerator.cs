using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        public int width = 512;
        public int height = 512;
        public int pointCount = 10;
        public GameObject cellPrefab;

        private void Start()
        {
            GenerateVoronoi();
        }

        private void GenerateVoronoi()
        {
            // Generate random points
            List<Vector2> points = new List<Vector2>();
            for (int i = 0; i < pointCount; i++)
            {
                points.Add(new Vector2(Random.Range(0, width), Random.Range(0, height)));
            }

            // Create a texture for the Voronoi diagram
            Texture2D voronoiTexture = new Texture2D(width, height);
            Color[] colors = new Color[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                colors[i] = Random.ColorHSV(); // Assign random colors to each cell
            }

            // Assign each pixel to the nearest point
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float minDistance = float.MaxValue;
                    int closestPointIndex = 0;

                    for (int i = 0; i < points.Count; i++)
                    {
                        float distance = Vector2.Distance(new Vector2(x, y), points[i]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestPointIndex = i;
                        }
                    }

                    voronoiTexture.SetPixel(x, y, colors[closestPointIndex]);
                }
            }

            voronoiTexture.Apply();

            // Create GameObjects for each cell
            for (int i = 0; i < points.Count; i++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(points[i].x, points[i].y, 0),
                    Quaternion.identity);
                cell.name = $"Cell_{i}";

                // Create a texture for the individual cell
                Texture2D cellTexture = new Texture2D(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (voronoiTexture.GetPixel(x, y) == colors[i])
                        {
                            cellTexture.SetPixel(x, y, colors[i]);
                        }
                        else
                        {
                            cellTexture.SetPixel(x, y, Color.clear); // Transparent for other cells
                        }
                    }
                }

                cellTexture.Apply();

                // Create a sprite from the texture
                Sprite cellSprite = Sprite.Create(cellTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

                // Assign the sprite to the cell's SpriteRenderer
                SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = cellSprite;
                }
            }
        }
    }
}