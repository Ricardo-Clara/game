using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{   
 public List<Transformer> transformer;
 public Transformable transformable;

 public TransformClamp clamper = null;
 protected DeltaTransform dT;

 void Awake() {
    if (!transformable) Debug.Log($"gameObject {transformable} not found");
    dT = gameObject.AddComponent<DeltaTransform>();
 }

 void Start() {
    for (int i = 0; i < transformer.Count; i++) {
        transformer[i].init(transformable, dT);
        transformer[i].enabled = true;
    }
 }   

 void Update() {
    for (int i = 0; i < transformer.Count; i++) {
        if (transformer[i].enabled) {
            transformer[i].move();
            transformer[i].rotate();
            transformer[i].scale();
            transformer[i].animate();
        }
    }

    if (clamper) clamper.clamp(transform);

 }
}
