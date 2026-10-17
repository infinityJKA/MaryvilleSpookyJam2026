using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TexturedPrism : MonoBehaviour
{
    // Components
    public Material wallMaterial;
    public Material topBottomMaterial;

    [Min(0.01f)] public float wallTileSize = 1f;
    [Min(0.01f)] public float topTileSize = 1f;

    void Start()
    {
        BuildPrism();
    }

    // Build Prism lets us test to see how it looks in scene view
    [ContextMenu("Build Prism")]
    public void BuildPrism()
    {
        // Standard cube dimensions: 1 x 1 x 1.
        // Transform scale determines the final world dimensions.
        Mesh mesh = new Mesh();
        mesh.name = "Textured Prism";

        Vector3[] vertices =
        {
            // Front (-Z)
            new(-.5f, -.5f, -.5f), new(.5f, -.5f, -.5f),
            new(.5f, .5f, -.5f), new(-.5f, .5f, -.5f),

            // Back (+Z)
            new(.5f, -.5f, .5f), new(-.5f, -.5f, .5f),
            new(-.5f, .5f, .5f), new(.5f, .5f, .5f),

            // Left (-X)
            new(-.5f, -.5f, .5f), new(-.5f, -.5f, -.5f),
            new(-.5f, .5f, -.5f), new(-.5f, .5f, .5f),

            // Right (+X)
            new(.5f, -.5f, -.5f), new(.5f, -.5f, .5f),
            new(.5f, .5f, .5f), new(.5f, .5f, -.5f),

            // Top (+Y)
            new(-.5f, .5f, -.5f), new(.5f, .5f, -.5f),
            new(.5f, .5f, .5f), new(-.5f, .5f, .5f),

            // Bottom (-Y)
            new(-.5f, -.5f, .5f), new(.5f, -.5f, .5f),
            new(.5f, -.5f, -.5f), new(-.5f, -.5f, -.5f)
        };

        int[] walls = new int[24];
        int[] caps = new int[12];

        for (int face = 0; face < 6; face++)
        {
            int start = face * 4;
            int[] target = face < 4 ? walls : caps;
            int offset = face < 4 ? face * 6 : (face - 4) * 6;

            target[offset] = start;
            target[offset + 1] = start + 2;
            target[offset + 2] = start + 1;
            target[offset + 3] = start;
            target[offset + 4] = start + 3;
            target[offset + 5] = start + 2;
        }

        // Use absolute scale for texture repetition.
        Vector3 scale = transform.lossyScale;
        float width = Mathf.Abs(scale.x);
        float height = Mathf.Abs(scale.y);
        float depth = Mathf.Abs(scale.z);

        Vector2[] uv = new Vector2[24];

        // Four side faces.
        for (int face = 0; face < 4; face++)
        {
            int i = face * 4;
            bool alongX = face < 2;

            float horizontal =
                (alongX ? width : depth) / wallTileSize;
            float vertical = height / wallTileSize;

            uv[i]     = new Vector2(0, 0);
            uv[i + 1] = new Vector2(horizontal, 0);
            uv[i + 2] = new Vector2(horizontal, vertical);
            uv[i + 3] = new Vector2(0, vertical);
        }

        // Top and bottom faces.
        float u = width / topTileSize;
        float v = depth / topTileSize;

        for (int face = 4; face < 6; face++)
        {
            int i = face * 4;
            uv[i]     = new Vector2(0, 0);
            uv[i + 1] = new Vector2(u, 0);
            uv[i + 2] = new Vector2(u, v);
            uv[i + 3] = new Vector2(0, v);
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.subMeshCount = 2;
        mesh.SetTriangles(walls, 0);
        mesh.SetTriangles(caps, 1);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = mesh;

        GetComponent<MeshRenderer>().sharedMaterials =
            new Material[] { wallMaterial, topBottomMaterial };
    }
}