using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelection : MonoBehaviour {

    public GameObject MouseClick;   // Instantiate object on area you want to units to move
    public int listSize = 0;        // Keep track of how many units are currently selected

    public bool isSelecting = false;       //If selection or not
    Vector3 mousePosition1;         //Position of mouse
                                    // Keep reference of all units currently selected
    private List<GameObject> players = new List<GameObject>();
                                    // Keep track of what gameobjects to remove in RemoveDeadPlayers()
    private List<GameObject> playersToRemove = new List<GameObject>();
    private RaycastHit hitInfo;     // Keep track of where in the world space player clicked
                                    // compared to where they clicked on the screen space
                                    
    private float raycastLength = 500;  // Longest distance raycast will detect collision

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1) // Make sure the game is playing
        {
            // Selecting units
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)){
                isSelecting = true;
                mousePosition1 = Input.mousePosition;
                PlayerSelect(ray);
                }
            if (Input.GetMouseButtonUp(0)) {
                //For each rabbit, check if IsWithinSelectionBounds is true
                //If thats true, and if they already arent selected, add them to the list, and increment the list size, and activate the selected marker
                if (isSelecting) {
                    foreach (var rabbit in FindObjectsOfType<PlayerMovement>()) {
                        if (!players.Contains(rabbit.gameObject) &&
                            IsWithinSelectionBounds(rabbit.gameObject) &&
                            rabbit.tag == "Player") {
                            players.Add(rabbit.gameObject);
                            rabbit.transform.Find("Selected marker").gameObject.SetActive(true);
                        }
                    }
                }

                isSelecting = false;
            }

            // Moving units
            if (Input.GetMouseButtonDown(1))
                SetMovePoint(ray);
            if (Input.GetKeyDown(KeyCode.LeftControl)) {
                foreach (GameObject rabbit in players) {
                    // Hide marker showing player units are selected
                    rabbit.transform.Find("Selected marker").gameObject.SetActive(false);
                }
                players.Clear(); // Clear all references to player units from list of units
                listSize = 0;
            }

            
        }
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = SelectionBox.GetScreenRect(mousePosition1, Input.mousePosition);
            //Draws a transparent rectangle in the center
            SelectionBox.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            //Draws a bordered rectangle
            SelectionBox.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)  //Checks to see if game object passed in is within the selection bounds
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            SelectionBox.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }

    // Add or remove unit(s) clicked from the list of currently selected units
    void PlayerSelect(Ray ray) {
        if (Physics.Raycast(ray, out hitInfo, raycastLength)) {
            if (hitInfo.transform.gameObject.tag == "Player") {
                // checks if player is already selected
                if (!players.Contains(hitInfo.rigidbody.gameObject)) {
                    players.Add(hitInfo.rigidbody.gameObject); // Reference the player unit
                    hitInfo.rigidbody.transform.Find("Selected marker").gameObject.SetActive(true);
                    // Show marker that indicates unit is selected
                    listSize++;
                } else {
                    players.Remove(hitInfo.rigidbody.gameObject); // Dereference the player unit
                    hitInfo.rigidbody.transform.Find("Selected marker").gameObject.SetActive(false);
                    // Show marker that indicates unit is selected
                    listSize--;
                }
            } else {
                foreach(GameObject p in players) {
                    p.transform.Find("Selected marker").gameObject.SetActive(false);
                    // Hide marker showing player units are selected
                }

                players.Clear(); // Clear all references to player units from list of units
                listSize = 0;
            }
        }
    }

    // Sets destination point for selected units to move towards using their navmesh
    void SetMovePoint (Ray ray) {
        if (Physics.Raycast(ray, out hitInfo, raycastLength))
        {
            if (hitInfo.transform.gameObject.tag == "Ground" || hitInfo.transform.gameObject.tag == "Bush") {

                Instantiate(MouseClick, hitInfo.point, Quaternion.identity); // Checks if ground is hit then makes the mouse indicator object

                foreach (GameObject rabbit in players) {
                    if (Vector3.Distance(hitInfo.point, rabbit.transform.position) > 2.0) {
                        rabbit.GetComponent<PlayerMovement>().dest = hitInfo;       // Set point for all player units to follow
                        rabbit.GetComponent<PlayerMovement>().MoveUnit();           // Call function starting movement of player units
                    }
                }
            }
        }
    }

    // Removes rabbits that are no longer able to be controlled by the player from the
    // list of rabbits the player is currently controlling
    public void RemovePlayers () {
        foreach (GameObject r in players) {
            if (r.tag == "Dead" || r.tag == "Survived") {
                playersToRemove.Add(r);
            }
        }

        foreach (GameObject i in playersToRemove) {
            players.Remove(i);
        }
    }
}
