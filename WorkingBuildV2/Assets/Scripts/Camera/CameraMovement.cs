using UnityEngine;
using System.Collections;
using RTS;

public class CameraMovement : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        MoveCamera();
    }

    private void MoveCamera() {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        // horizontal camera movement
        if (xpos >= 0 && xpos < ResourceManagement.ScrollWidth || Input.GetKey(KeyCode.A)) {
            // if the x position is near the left edge of the screen
            // but not beyond the screen
            movement.x -= ResourceManagement.ScrollSpeed;
        } else if (xpos <= Screen.width && xpos > Screen.width - ResourceManagement.ScrollWidth ||
            Input.GetKey(KeyCode.D)) {

            // if the x position is near the right edge of the screen
            // but not beyond the screen
            movement.x += ResourceManagement.ScrollSpeed;
        }

        // vertical movement
        if (ypos >= 0 && ypos < ResourceManagement.ScrollWidth || Input.GetKey(KeyCode.S)) {
            // if the y position is near the bottom edge of the screen
            // but not beyond the screen
            movement.z -= ResourceManagement.ScrollSpeed;
        } else if (ypos <= Screen.height && ypos > Screen.height - ResourceManagement.ScrollWidth ||
            Input.GetKey(KeyCode.W)) {

            // if the y position is near the top edge of the screen
            // but not beyond the screen
            movement.z += ResourceManagement.ScrollSpeed;
        }

        // convert movement from local space to world space with respect to the cameras position
        movement = Camera.main.transform.TransformDirection(movement);
        // not moving up or down
        movement.y = 0;

        // move camera up and down
        movement.y -= ResourceManagement.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        // calculate desired camera position based on receivedinput
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        // limit how far/close the camera can be to the ground
        if (destination.y > ResourceManagement.MaxCamerHeight) {
            destination.y = ResourceManagement.MaxCamerHeight;
        } else if (destination.y < ResourceManagement.MinCameraHeight) {
            destination.y = ResourceManagement.MinCameraHeight;
        }

        destination.x = Mathf.Clamp(destination.x, 3.0f, 248.0f);
        destination.z = Mathf.Clamp(destination.z, 20.0f, 225.0f);

        // if a change in position is detected perform the necessary update
        if (destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManagement.ScrollSpeed);
        }
    }
}
