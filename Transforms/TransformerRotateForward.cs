using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerRotateForward : Transformer {
    public float rotationSpeed = 5;
    
    public override Vector3 dRotate() {
        Vector3 dPos = dT.dPosition();

        if (dPos != Vector3.zero) { //operation may fail if rotation needed 
            Quaternion toRotation = Quaternion.LookRotation(dPos);

            return Vector3x.dRotateTowards(transform.rotation.eulerAngles, toRotation.eulerAngles, rotationSpeed);
        }
        
        return Vector3.zero;
    } 
}
