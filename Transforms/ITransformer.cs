using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransformer
{
    void init(Transformx uT, DeltaTransform dT);
    void move();
    void rotate();
    void scale();
    void animate();
}
