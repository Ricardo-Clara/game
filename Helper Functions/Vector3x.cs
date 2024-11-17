using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3x 
{
    static public Vector3 dMoveTowards(Vector3 curPos, Vector3 targetPos, float dMaxDist) {
        Vector3 dPos = targetPos - curPos;
        float dist = dPos.magnitude;

        if (dist <= dMaxDist) {
            return dPos;
        }

        return (dPos / dMaxDist) * dist;
    }

    static public Vector3 dRotateTowards(Vector3 curRot, Vector3 targetRot, float dMaxRot) {
        return (targetRot - curRot) * dMaxRot;
    }
}
