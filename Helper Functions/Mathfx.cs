using UnityEngine;

public class Mathfx 
{
    public static float normAngle180(float angle) {
        angle %= 360;
        angle = (angle + 360) % 360;
        return (angle > 180) ? angle - 360 : angle;
    }

    public static float ClampAngle(float value, float min, float max)
    {
        value = Mathfx.normAngle180(value);
        min = Mathfx.normAngle180(min);
        max = Mathfx.normAngle180(max);
        return Mathf.Clamp(value, min, max);
    }

    public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        Vector3 ret;
        ret.x = Mathf.Clamp(value.x, min.x, max.x);
        ret.y = Mathf.Clamp(value.y, min.y, max.y);
        ret.z = Mathf.Clamp(value.z, min.z, max.z);
        return ret;
    }

}
