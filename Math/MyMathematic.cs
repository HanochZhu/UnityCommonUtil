using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMathematic : MonoBehaviour
{
    public struct Matrix3x3
    {
        public float m00;
        public float m01;
        public float m02;
        public float m10;
        public float m11;
        public float m12;
        public float m20;
        public float m21;
        public float m22;

        public Matrix3x3(float defaultValue = 0)
        {
            m00 = m01 = m02 = m10 = m11 = m12 = m20 = m21 = m22 = defaultValue;
        }

        // 左乘
        public Vector3 LeftMulitplay(Vector3 leftVector)
        {
            return new Vector3(m00 * leftVector.x + m01 * leftVector.y + m02 * leftVector.z,
                               m10 * leftVector.x + m11 * leftVector.y + m12 * leftVector.z,
                               m20 * leftVector.x + m21 * leftVector.y + m22 * leftVector.z);
        }
        
    }
}