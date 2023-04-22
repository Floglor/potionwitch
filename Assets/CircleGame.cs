using System;
using Microsoft.Unity.VisualStudio.Editor;
using Sirenix.OdinInspector;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class CircleGame : MonoBehaviour
{
    [ShowInInspector] public float RotationAngle;

    private const float MaxAngle = 122;
    private float _correctMaxAngle;
    private const float MinAngle = 69;
    private float _correctMinAngle;
    private Vector3 _pointerOriginalPosition;
    private Quaternion _pointerOriginalRotation;
    public GameObject TryHittingButton;

    [ReadOnly] public float ZRotation;
    public Transform ObjectParent;

    public Image WhiteBox;

    public bool IsGameRunning;

    public Action<bool> OnCompletion;
    
    

    private void Awake()
    {
        WhiteBox.gameObject.SetActive(false);
        _pointerOriginalPosition = transform.position;
        _pointerOriginalRotation = transform.rotation;
    }

    public void StartGame(float rotationAngleSpeed, float startingAngle)
    {
        ObjectParent.eulerAngles = new Vector3(0, 0, startingAngle );
        RotationAngle = rotationAngleSpeed;
        transform.position = _pointerOriginalPosition;
        transform.rotation = _pointerOriginalRotation;
        
        TryHittingButton.gameObject.SetActive(true);
        FindCorrectAngles();
        IsGameRunning = true;
        TryHittingButton.SetActive(true);
    }
    

    private void Update()
    {
        if (!IsGameRunning) return;
        
        transform.RotateAround(ObjectParent.position, Vector3.forward, RotationAngle * Time.deltaTime);

    }

    public void TryHitting()
    {
        ZRotation = transform.eulerAngles.z;

        if (ZRotation > _correctMaxAngle || ZRotation < _correctMinAngle)
        {
            // Debug.Log("Wrong Angle!");
            //WhiteBox.color = Color.red;
            CompleteGame(false);
        }
        else
        {
            //Debug.Log("Right Angle!");
            //WhiteBox.color = Color.green;
            CompleteGame(true);

        }
    }

    private void CompleteGame(bool won)
    {
        OnCompletion?.Invoke(won);
        IsGameRunning = false;
        this.gameObject.transform.parent.gameObject.SetActive(false);
        TryHittingButton.gameObject.SetActive(false);

    }

    private void FindCorrectAngles()
    {
        float rotation;
        if (ObjectParent.eulerAngles.z <= 180f)
        {
            rotation = ObjectParent.eulerAngles.z;
        }
        else
        {
            rotation = ObjectParent.eulerAngles.z - 360f;
        }

        _correctMaxAngle = rotation + MaxAngle;
        if (_correctMaxAngle >= 360)
        {
            _correctMaxAngle = MaxAngle % 360;
        }

        _correctMinAngle = rotation + MinAngle;
        if (_correctMinAngle >= 360)
        {
            _correctMaxAngle = MinAngle % 360;
        }
    }
}