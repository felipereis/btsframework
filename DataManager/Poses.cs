using UnityEngine;

public class Poses : MonoBehaviour
{
    //Mediapipe Standard
    public enum PoseMediapipe
    {
        NOSE = 0,
        L_EYE_INNER = 1,
        L_EYE = 2,
        L_EYE_OUTER = 3,
        R_EYE_INNER = 4,
        R_EYE = 5,
        R_EYE_OUTER = 6,
        L_EAR = 7,
        R_EAR = 8,
        L_MOUTH = 9,
        R_MOUTH = 10,
        L_SHOULDER = 12,
        R_SHOULDER = 11,
        L_ELBOW = 14,
        R_ELBOW = 13,
        L_WRIST = 16,
        R_WRIST = 15,
        L_PINKY = 17,
        R_PINKY = 18,
        L_INDEX = 19,
        R_INDEX = 20,
        L_THUMB = 21,
        R_THUMB = 22,
        L_HIP = 24,
        R_HIP = 23,
        L_KNEE = 26,
        R_KNEE = 25,
        L_ANKLE = 28,
        R_ANKLE = 27,
        L_HEEL = 30,
        R_HEEL = 29,
        L_FINDEX = 32,
        R_FINDEX = 31
    }

    //Mediapipe Only
    public enum PoseHand
    {
        WRIST = 0,
        THUMB_CMC = 1,
        THUMB_MCP = 2,
        THUMB_IP = 3,
        THUMP_TIP = 4,
        INDEX_FINGER_MCP = 5,
        INDEX_FINGER_PIP = 6,
        INDEX_FINGER_DIP = 7,
        INDEX_FINGER_TIP = 8,
        MIDDLE_FINGER_MCP = 9,
        MIDDLE_FINGER_PIP = 10,
        MIDDLE_FINGER_DIP = 11,
        MIDDLE_FINGER_TIP = 12,
        RING_FINGER_MCP = 13,
        RING_FINGER_PIP = 14,
        RING_FINGER_DIP = 15,
        RING_FINGER_TIP = 16,
        PINKY_MCP = 17,
        PINKY_PIP = 18,
        PINKY_DIP = 19,
        PINKY_TIP = 20
    }

    // BTS Standard = Kinect - Mediapipe different mapping
    public enum PoseBts
    {
        HipCenter = 1,
        Spine = 2,
        ShoulderCenter = 3,
        Head = 4,
        LeftShoulder = 5,
        LeftElbow = 6,
        LeftWrist = 7,
        LeftHand = 8,
        RightShoulder = 9,
        RightElbow = 10,
        RightWrist = 11,
        RightHand = 12,
        LeftHip = 13,
        LeftKnee = 14,
        LeftAnkle = 15,
        LeftFoot = 16,
        RightHip = 17,
        RightKnee = 18,
        RightAnkle = 19,
        RightFoot = 20
    }
}
