// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System.Numerics;

public static class MathFunctions
{
    public const float E = 2.71828f;
    public const float Log10E = 0.434294f;
    public const float Log2E = 1.4427f;
    public const float PI = 3.14159f;
    public const float PiOver2 = 1.5708f;
    public const float PiOver4 = 0.785398f;
    public const float TwoPi = 6.28319f;
    public const float Epsilon = 4.76837158203125E-7f;
    public static Matrix4x4 ToMatrix(this Quaternion _q)
    {
        // Implementation from Watt and Watt, pg 362
        // See also http://www.flipcode.com/documents/matrfaq.html#Q54
        Quaternion q = _q;
        q *= 1.0f / MathF.Sqrt((q.X * q.X) + (q.Y * q.Y) + (q.Z * q.Z) + (q.W * q.W));

        float xx = 2.0f * q.X * q.X;
        float xy = 2.0f * q.X * q.Y;
        float xz = 2.0f * q.X * q.Z;
        float xw = 2.0f * q.X * q.W;

        float yy = 2.0f * q.Y * q.Y;
        float yz = 2.0f * q.Y * q.Z;
        float yw = 2.0f * q.Y * q.W;

        float zz = 2.0f * q.Z * q.Z;
        float zw = 2.0f * q.Z * q.W;

        return new Matrix4x4(1.0f - yy - zz, xy - zw, xz + yw, 0.0f,
            xy + zw, 1.0f - xx - zz, yz - xw, 0.0f,
            xz - yw, yz + xw, 1.0f - xx - yy, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
    }

}