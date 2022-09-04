using System;
using Google.Protobuf.Collections;
using Mediapipe;
using UnityEngine;

public class MediapipeManager : MonoBehaviour
{
    private static RepeatedField<NormalizedLandmark> _landmarks;
    public static RepeatedField<NormalizedLandmark> VectorHand;
    private int counter;
    private bool startCounter = true;
    private static bool _poseFound;

    private void Start()
    {
        VectorHand = new RepeatedField<NormalizedLandmark>();
    }

    // Auxiliary function
    public void SetPose(RepeatedField<NormalizedLandmark> newLandmarks)
    {
        if (startCounter)
        {
            startCounter = false;
        }

        if (newLandmarks.Count > 0)
        {
            _poseFound = true;
            _landmarks = newLandmarks;
            counter = counter + 1;
        }
    }

    // If has pose return true
    public static bool HasPose()
    {
        return _poseFound;
    }

    // Return vector3 with pose
    public static Vector3 GetPoint(Poses.PoseBts idxPoint)
    {
        if (HasPose())
        {
            return new Vector3(
                _landmarks[(int)idxPoint].X * 640,
                (1f - _landmarks[(int)idxPoint].Y) * 480,
                -320 * _landmarks[(int)idxPoint].Z);
        }
        else
        {
            return Vector3.zero;
        }
    }

    // All hand poses
    public void GetHandVector(RepeatedField<NormalizedLandmark> handLandmarkLists)
    {
        VectorHand = handLandmarkLists;
    }

    // Convert BTS pose standard to Mediapipe
    public static Poses.PoseBts ConvertKinectMapToMediaPipe(Poses.PoseBts poseBts)
    {
        switch ((int)poseBts)
        {
            case 4:
                return (Poses.PoseBts)0;
            case 5:
                return (Poses.PoseBts)12;
            case 6:
                return (Poses.PoseBts)14;
            case 7:
                return (Poses.PoseBts)16;
            case 8:
                return (Poses.PoseBts)19;
            case 9:
                return (Poses.PoseBts)11;
            case 10:
                return (Poses.PoseBts)13;
            case 11:
                return (Poses.PoseBts)15;
            case 12:
                return (Poses.PoseBts)20;
            case 13:
                return (Poses.PoseBts)24;
            case 14:
                return (Poses.PoseBts)26;
            case 15:
                return (Poses.PoseBts)28;
            case 16:
                return (Poses.PoseBts)32;
            case 17:
                return (Poses.PoseBts)23;
            case 18:
                return (Poses.PoseBts)25;
            case 19:
                return (Poses.PoseBts)27;
            case 20:
                return (Poses.PoseBts)31;
            default:
                return (Poses.PoseBts)(-1);
        }
    }
}