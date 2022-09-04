using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Text;
using Google.Protobuf.Collections;
using Mediapipe;

public class Utils : MonoBehaviour
{
    // Function used in Compress and Decompress
    private static void CopyTo(Stream src, Stream dest)
    {
        byte[] bytes = new byte[4096];

        int cnt;

        while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
        {
            dest.Write(bytes, 0, cnt);
        }
    }

    // Compress Data
    public static byte[] Compress(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);

        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new DeflateStream(mso, CompressionMode.Compress))
            {
                CopyTo(msi, gs);
            }

            return mso.ToArray();
        }
    }

    // Decompress sent data
    public static string Decompress(byte[] bytes)
    {
        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new DeflateStream(msi, CompressionMode.Decompress))
            {
                CopyTo(gs, mso);
            }

            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }

    private static RepeatedField<NormalizedLandmark> landmarks;

    // Get2dAngle using 3 positions (first, middle, last)
    public float Get2DAngle(Poses.PoseBts idxFirst, Poses.PoseBts idxMid, Poses.PoseBts idxLast)
    {
        var first = landmarks[(int)idxFirst];
        var mid = landmarks[(int)idxMid];
        var last = landmarks[(int)idxLast];
        var result = Mathf.Rad2Deg *
                     (Mathf.Atan2(last.Y - mid.Y, last.X - mid.X) - Mathf.Atan2(first.Y - mid.Y, first.X - mid.X));
        result = Mathf.Abs(result);
        if (result > 180)
        {
            result = 360 - result;
        }

        return result;
    }

    // Get3dAngle using 3 positions (first, middle, last)
    public float Get3DAngle(Poses.PoseBts idxFirst, Poses.PoseBts idxMid, Poses.PoseBts idxLast)
    {
        var first = landmarks[(int)idxFirst];
        var mid = landmarks[(int)idxMid];
        var last = landmarks[(int)idxLast];

        var b = new Vector3(mid.X, mid.Y, mid.Z);
        var ba = (new Vector3(first.X, first.Y, first.Z)) - b;
        var bc = (new Vector3(last.X, last.Y, last.Z)) - b;

        var cosine = Vector3.Dot(ba, bc) / (ba.magnitude * bc.magnitude);
        var angle = Mathf.Acos(cosine);
        var result = Mathf.Rad2Deg * angle;
        return result;
    }
}