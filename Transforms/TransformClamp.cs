using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransformClamp : MonoBehaviour
{
    public enum ModeSelect { None, Absolute, Relative }

    public ModeSelect clampPosition = ModeSelect.None;
    public Vector3 minPosition = Vector3.zero;
    public Vector3 maxPosition = Vector3.zero;
    public ModeSelect clampRotation = ModeSelect.None;
    public Vector3 minRotation = Vector3.zero;
    public Vector3 maxRotation = Vector3.zero;
    public ModeSelect clampScale = ModeSelect.None;
    public Vector3 minScale = Vector3.zero;
    public Vector3 maxScale = Vector3.zero;

    void Awake()
    {
        if (clampPosition == ModeSelect.Relative)
        {
            minPosition = transform.position + minPosition;
            maxPosition = transform.position + maxPosition;
        }
        if (clampRotation == ModeSelect.Relative)
        {
            //add min/max rotation to initial rotation
            Vector3 minAngle = transform.eulerAngles + minRotation;
            Vector3 maxAngle = transform.eulerAngles + maxRotation;

            minRotation = new Vector3(Mathfx.normAngle180(minAngle.x), Mathfx.normAngle180(minAngle.y), Mathfx.normAngle180(minAngle.z));
            maxRotation = new Vector3(Mathfx.normAngle180(maxAngle.x), Mathfx.normAngle180(maxAngle.y), Mathfx.normAngle180(maxAngle.z));
        }
        else
        {
            minRotation = new Vector3(Mathfx.normAngle180(minRotation.x), Mathfx.normAngle180(minRotation.y), Mathfx.normAngle180(minRotation.z));
            maxRotation = new Vector3(Mathfx.normAngle180(maxRotation.x), Mathfx.normAngle180(maxRotation.y), Mathfx.normAngle180(maxRotation.z));
        }
        if (clampScale == ModeSelect.Relative)
        {
            minScale = transform.localScale + minScale;
            maxScale = transform.localScale + maxScale;
        }   
    }

    public void clamp(Transform t)
    {
        if (clampPosition != ModeSelect.None) {
            t.position = Mathfx.Clamp(t.position, minPosition, maxPosition);
        }
        if (clampRotation != ModeSelect.None) {
            t.eulerAngles = new Vector3(Mathfx.ClampAngle(t.eulerAngles.x, minRotation.x, maxRotation.x), Mathfx.ClampAngle(t.eulerAngles.y, minRotation.y, maxRotation.y), Mathfx.ClampAngle(t.eulerAngles.z, minRotation.z, maxRotation.z));
        }
        if (clampScale != ModeSelect.None) {
            t.localScale = Mathfx.Clamp(t.localScale, minScale, maxScale);
        }
    }



}
