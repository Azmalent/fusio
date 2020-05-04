using System.Collections.Generic;
using UnityEngine;

public class PerspectiveManager : MonoBehaviour
{
    const float SECONDS_TO_ROTATE = 0.5f;

    const int PLAYER_LAYER = 8;
    const int INVISIBLE_BLOCK_LAYER_X = 9;
    const int INVISIBLE_BLOCK_LAYER_Z = 10;
    const int INVISIBLE_BLOCK_LAYER_RED = 11;

    public Transform RedBlocks;
    public Transform GreenBlocks;
    public Transform BlueBlocks;
    public GameObject InvisibleBlockPrefab;
    public Perspective Side = Perspective.Front;

    private bool isRotating = false;
    private float rotationProgress = 0f;
    private Quaternion previousGreenRotation;
    private Quaternion prevoiusBlueRotation;

    private List<Transform> invisibleBlocks = new List<Transform>();

    private CameraMovement cameraMovement;

    void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();

        foreach (Vector3 pos in GetInvisibleBlockPositions(GreenBlocks))
        {
            CreateInvisibleBlock(pos, GreenBlocks);
        }

        foreach (Vector3 pos in GetInvisibleBlockPositions(RedBlocks, true))
        {
            CreateInvisibleBlock(pos, RedBlocks);
        }

        UpdateCollisionSettings();
    }

    void Update()
    {
        if (isRotating)
        {
            if (rotationProgress < 1)
            {
                rotationProgress += Time.deltaTime / SECONDS_TO_ROTATE;
                GreenBlocks.rotation = Quaternion.Lerp(previousGreenRotation, GreenRotation, rotationProgress);
                BlueBlocks.rotation = Quaternion.Lerp(prevoiusBlueRotation, BlueRotation, rotationProgress);
            }
            else FinishRotating();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q)) RotateClockwise();
            else if (Input.GetKeyDown(KeyCode.E)) RotateCounterclockwise();

            if (Input.GetKeyDown(KeyCode.Tab)) SwitchCameraMode();
        }
    }

    public Quaternion GreenRotation => Quaternion.Euler(0, 90 * (int)Side, 0);
    public Quaternion BlueRotation => Quaternion.Euler(0, 0, 90 * (int)Side);
    private void ResetRotation()
    {
        Side = Perspective.Front;
        isRotating = false;
    }

    private void RotateClockwise()
    {
        previousGreenRotation = GreenRotation;
        prevoiusBlueRotation = BlueRotation;

        if (Side != Perspective.Right) Side++;
        else Side = Perspective.Front;

        StartRotating();
    }

    private void RotateCounterclockwise()
    {
        previousGreenRotation = GreenRotation;
        prevoiusBlueRotation = BlueRotation;

        if (Side != Perspective.Front) Side--;
        else Side = Perspective.Right;

        StartRotating();
    }

    private void StartRotating()
    {
        SoundManager.Instance.PlaySound("Swing");
        isRotating = true;

        UpdateCollisionSettings();
    }

    private void FinishRotating()
    {
        rotationProgress = 0;
        isRotating = false;
        cameraMovement.Shake(0.2f, 0.5f);
    }

    private void SwitchCameraMode()
    {
        Camera.main.orthographic = !Camera.main.orthographic;
        UpdateCollisionSettings();
    }

    private void UpdateCollisionSettings()
    {
        bool lookingAtZ = Side == Perspective.Front || Side == Perspective.Back;
        bool is3D = !Camera.main.orthographic;

        bool ignoreX = lookingAtZ || is3D;
        bool ignoreZ = !lookingAtZ || is3D;
        Physics.IgnoreLayerCollision(PLAYER_LAYER, INVISIBLE_BLOCK_LAYER_X, ignoreX);
        Physics.IgnoreLayerCollision(PLAYER_LAYER, INVISIBLE_BLOCK_LAYER_Z, ignoreZ);
        Physics.IgnoreLayerCollision(PLAYER_LAYER, INVISIBLE_BLOCK_LAYER_RED, is3D);
    }

    private HashSet<Vector3> GetInvisibleBlockPositions(Transform blockSet, bool zOnly = false)
    {
        var blockLocations = new HashSet<Vector3>();
        foreach (Transform item in blockSet)
        {
            if (!zOnly && item.localPosition.x != 0)
            {
                var pos = new Vector3(0, item.localPosition.y, item.localPosition.z);
                blockLocations.Add(pos);
            }

            if (item.localPosition.z != 0)
            {
                var pos = new Vector3(item.localPosition.x, item.localPosition.y, 0);
                blockLocations.Add(pos);
            }
        }

        foreach (Transform item in blockSet) blockLocations.Remove(item.localPosition);

        return blockLocations;
    }

    private GameObject CreateInvisibleBlock(Vector3 position, Transform parent)
    {
        var block = Instantiate(InvisibleBlockPrefab) as GameObject;
        block.transform.parent = parent;
        block.transform.localPosition = position;

        if (parent == RedBlocks) block.layer = INVISIBLE_BLOCK_LAYER_RED;
        else if (position.x == 0) block.layer = INVISIBLE_BLOCK_LAYER_X;
        else block.layer = INVISIBLE_BLOCK_LAYER_Z;

        return block;
    }
}
