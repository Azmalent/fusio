using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Block : MonoBehaviour
{
    const float TILE_SIZE = 1;

    // Use this for initialization
    void Start()
    {
        ApplyTexture();
    }

    public void ApplyTexture()
    {
        var filter = GetComponent<MeshFilter>();
        var mesh = filter.sharedMesh;
        var uvs = mesh.uv;

        //Front
        uvs[0] = new Vector2(0f, 0f);
        uvs[1] = new Vector2(TILE_SIZE, 0f);
        uvs[2] = new Vector2(0f, 1f);
        uvs[3] = new Vector2(TILE_SIZE, 1f);

        // Top
        uvs[8] = new Vector2(0f, 0f);
        uvs[9] = new Vector2(TILE_SIZE, 0f);
        uvs[4] = new Vector2(0f, 1f);
        uvs[5] = new Vector2(TILE_SIZE, 1f);

        // Back
        uvs[7] = new Vector2(0f, 0f);
        uvs[6] = new Vector2(TILE_SIZE, 0f);
        uvs[11] = new Vector2(0f, 1f);
        uvs[10] = new Vector2(TILE_SIZE, 1f);

        // Bottom
        uvs[15] = new Vector2(0f, 0f);
        uvs[14] = new Vector2(TILE_SIZE, 0f);
        uvs[12] = new Vector2(0f, 1f);
        uvs[13] = new Vector2(TILE_SIZE, 1f);

        // Left
        uvs[16] = new Vector2(0f, 0f);
        uvs[19] = new Vector2(TILE_SIZE, 0f);
        uvs[17] = new Vector2(0f, 1f);
        uvs[18] = new Vector2(TILE_SIZE, 1f);

        // Right        
        uvs[20] = new Vector2(0f, 0f);
        uvs[23] = new Vector2(TILE_SIZE, 0f);
        uvs[21] = new Vector2(0f, 1f);
        uvs[22] = new Vector2(TILE_SIZE, 1f);

        mesh.uv = uvs;
    }
}