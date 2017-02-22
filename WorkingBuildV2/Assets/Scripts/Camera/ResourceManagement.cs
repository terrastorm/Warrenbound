using UnityEngine;
using System.Collections;

namespace RTS {
    public static class ResourceManagement
    {
                                                                    // how far from the edge of the screen the mouse needs
        public static int ScrollWidth { get { return 25; } }        //  to be to start scrolling the camera
        public static float ScrollSpeed { get { return 25f; } }     // how fast the camera scrolls
        public static float MinCameraHeight { get { return 10f; } } // min height of the camera
        public static float MaxCamerHeight { get { return 40f; } }    // max height of the camera
    }
}
