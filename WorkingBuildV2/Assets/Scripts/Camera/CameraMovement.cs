using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
                                    // how far from the edge of the screen the mouse needs
    public float ScrollWidth;       //  to be to start scrolling the camera
    public float ScrollSpeed;       // how fast the camera scrolls
    public float MinCameraHeight;   // min height of the camera
    public float MaxCamerHeight;    // max height of the camera
    public float minCameraX, maxCameraX;
    public float minCameraZ, maxCameraZ;

    // Update is called once per frame
    void Update() {
        MoveCamera();
    }

    private void MoveCamera() {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        // horizontal camera movement
        if (xpos >= 0 && xpos < ScrollWidth || Input.GetKey(KeyCode.A)) {
            // if the x position is near the left edge of the screen
            // but not beyond the screen
            movement.x -= ScrollSpeed;
        } else if (xpos <= Screen.width && xpos > Screen.width - ScrollWidth ||
            Input.GetKey(KeyCode.D)) {

            // if the x position is near the right edge of the screen
            // but not beyond the screen
            movement.x += ScrollSpeed;
        }

        // vertical movement
        if (ypos >= 0 && ypos < ScrollWidth || Input.GetKey(KeyCode.S)) {
            // if the y position is near the bottom edge of the screen
            // but not beyond the screen
            movement.z -= ScrollSpeed;
        } else if (ypos <= Screen.height && ypos > Screen.height - ScrollWidth ||
            Input.GetKey(KeyCode.W)) {

            // if the y position is near the top edge of the screen
            // but not beyond the screen
            movement.z += ScrollSpeed;
        }

        // convert movement from local space to world space with respect to the cameras position
        movement = Camera.main.transform.TransformDirection(movement);
        // not moving up or down
        movement.y = 0;

        // move camera up and down
        movement.y -= ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        // calculate desired camera position based on receivedinput
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        // limit how far/close the camera can be to the ground
        if (destination.y > MaxCamerHeight) {
            destination.y = MaxCamerHeight;
        } else if (destination.y < MinCameraHeight) {
            destination.y = MinCameraHeight;
        }

        destination.x = Mathf.Clamp(destination.x, minCameraX, maxCameraX);
        destination.z = Mathf.Clamp(destination.z, minCameraZ, maxCameraZ);

        // if a change in position is detected perform the necessary update
        if (destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ScrollSpeed);
        }
    }
}
