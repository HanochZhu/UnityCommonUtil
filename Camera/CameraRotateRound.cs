/**
 * Distance control is basd on vector,
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateRound : MonoBehaviour
{
    public Transform CenterTarget;

    public Transform LookAtTarget;

    private Transform reallookAtTarget;

    public float RotateSpeed = 5;

    public float ScrollSpeed = 5;

    [Header("-------rotate property")]

    public float MinYAngle = 30;

    public float MaxYAngle = 80;
    [Header("-------scroll property")]

    public float MinDistance = 1;
    public float MaxDistance = 5;

    private Vector3 offsetToTarget;

    private bool isRotate = false;


    private void Start()
    {
        reallookAtTarget = GetLookAtTransform();
        transform.LookAt(reallookAtTarget);
        offsetToTarget = this.transform.position - CenterTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotate = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // actually it's not a stable method
            // When mouse push up out of the game view, the method will not be invoke.
            isRotate = false;
        }
        if (isRotate)
        {
            RotateView();
        }

        ScrollView();
        transform.position = CenterTarget.position + offsetToTarget;
    }

    private void ScrollView()
    {
        float distance = offsetToTarget.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        if (distance < MinDistance)
        {
            distance = MinDistance;
        }
        else if (distance > MaxDistance)
        {
            distance = MaxDistance;
        }
        offsetToTarget = offsetToTarget.normalized * distance;
    }

    private void RotateView()
    {
        float mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴增量
        float mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴增量

        this.transform.RotateAround(CenterTarget.position, Vector3.up, mouse_x * RotateSpeed);

        Vector3 originPosition = transform.position;
        Quaternion originRotation = transform.rotation;

        this.transform.RotateAround(CenterTarget.position, transform.right, mouse_y * RotateSpeed);

        print(transform.eulerAngles.x);
        if (transform.eulerAngles.x < MinYAngle || transform.eulerAngles.x > MaxYAngle)
        {
            transform.position = originPosition;
            transform.rotation = originRotation;
        }
        offsetToTarget = transform.position - CenterTarget.position;
        transform.LookAt(reallookAtTarget);
        print(reallookAtTarget.name);
    }

    #region public method for other module
    // ---------------------
    public void SetMaxDistanceOfCamera(float maxScrollDistance)
    {
        //this.MaxDistance = maxScrollDistance / 2f;
    }
    #endregion

    #region private method
    private Transform GetLookAtTransform()
    {
        if (LookAtTarget == null)
        {
            return CenterTarget;
        }
        return LookAtTarget;
    }
    #endregion
}