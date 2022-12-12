using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class ScrollUV : MonoBehaviour
{
    private Camera _mainCamera;
    private MeshRenderer _meshRenderer;
    private Material _material;
    private Vector2 _offset;
    private void Start()
    {
        _mainCamera = Camera.main;
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _offset = GetComponent<MeshRenderer>().material.mainTextureOffset;
    }

    void Update()
    {
        RepeatCameraPosition();
    }

    private void RepeatCameraPosition()
    {
        _offset = _material.mainTextureOffset;
        Vector3 position = _mainCamera.transform.position;
        _offset.x = position.x;
        _offset.y = position.y;
        _material.mainTextureOffset = _offset;
    }
}