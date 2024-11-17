using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    protected Animator animator;
    protected Transformable target;
    protected DeltaTransform dT;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        if (!animator) {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public virtual void init(Transformable target, DeltaTransform dT) {
        this.target = target;
        this.dT = dT;
    }


    public void move() { target.move(dMove() * Time.deltaTime); }
    public void rotate() { target.rotate(dRotate() * Time.deltaTime); }
    public void scale() { target.scale(dScale() * Time.deltaTime); }

    public virtual void animate() {}
    public virtual Vector3 dMove() { return Vector3.zero; }
    public virtual Vector3 dRotate() { return Vector3.zero; }
    public virtual Vector3 dScale() { return Vector3.zero; }
}
