using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMathematic
{
    // 根据三点计算所形成的圆的半径
    // 投影到XOZ平面
    public static float R(Vector3 p1, Vector3 p2, Vector3 p3)
    {

        float x1 = p1.x;
        float y1 = p1.z;
        float x2 = p2.x;
        float y2 = p2.z;
        float x3 = p3.x;
        float y3 = p3.z;

        float A = x1 * (y2 - y3) - y1 * (x2 - x3) + x2 * y3 - x3 * y2;

        float b11 = (x1 * x1 + y1 * y1) * (y3 - y2);
        float b12 = (x2 * x2 + y2 * y2) * (y1 - y3);
        float b13 = (x3 * x3 + y3 * y3) * (y2 - y1);

        float B = b11 + b12 + b13;

        float c11 = (x1 * x1 + y1 * y1) * (x2 - x3);
        float c12 = (x2 * x2 + y2 * y2) * (x3 - x1);
        float c13 = (x3 * x3 + y3 * y3) * (x1 - x2);

        float C = c11 + c12 + c13;

        float d11 = (x1 * x1 + y1 * y1) * (x3 * y2 - x2 * y3);
        float d12 = (x2 * x2 + y2 * y2) * (x1 * y3 - x3 * y1);
        float d13 = (x3 * x3 + y3 * y3) * (x2 * y1 - x1 * y2);

        float D = d11 + d12 + d13;

        return Mathf.Sqrt((B * B + C * C - 4 * A * D) / (4 * A * A));
    }

    // 根据两个向量所成的夹脚计算余弦值
    // 
    public static float GetVelocityByCos(float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        return 2 * Mathf.Cos(angle) - 1;
    }
    // 幂函数,y=x^-2
    // ^
    // |
    // |
    // ||
    // ||
    // |\
    // | \
    // |  \
    // |   \_________
    // ________________________________
    public static float GetVelocityByPower(float angle)
    {
        if (angle < 1f)
        {
            angle = 1f;
        }
        return Mathf.Pow(angle, -2) * 2;
    }

    // 对数函数
    // 
    public static float GetVelocityByLog(float angle)
    {
        if (Mathf.Abs(angle) < 0.0001f)
        {
            return 1;
        }
        if (angle < 0)
        {
            return -1;
        }
        float v = Mathf.Log(angle, 0.05f) * 2f - 1f;
        if (v > 1)
        {
            v = 1;
        }
        return v;
    }

}

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

    public Matrix3x3(float _m00, float _m01, float _m02,
                    float _m10, float _m11, float _m12,
                    float _m20, float _m21, float _m22)
    {
        m00 = _m00; m01 = _m01; m02 = _m02;
        m10 = _m10; m11 = _m11; m12 = _m12;
        m20 = _m20; m21 = _m21; m22 = _m22;
    }

    // 左乘
    public Vector3 LeftMulitplay(Vector3 leftVector)
    {
        return new Vector3(m00 * leftVector.x + m01 * leftVector.y + m02 * leftVector.z,
                            m10 * leftVector.x + m11 * leftVector.y + m12 * leftVector.z,
                            m20 * leftVector.x + m21 * leftVector.y + m22 * leftVector.z);
    }
        
}
