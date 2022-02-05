using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerGame : MonoBehaviour
{
    private BoxCollider[] colliders;
    private float timer = 3.0f;
    private int score = 0;
    private int lastColliderIdx = -1;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        colliders = this.GetComponentsInChildren<BoxCollider>();
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update() 
    {
        // If player presses "B" quit game
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit()
            #endif
        }

        this.timer -= Time.deltaTime;
        if (this.timer <= 0.0f)
        {
            onTime();
            this.timer = 3.0f;
        }
    }

    private void onTime()
    {
        // Display a random light
        int randIdx = Random.Range(0, colliders.Length);
        // Turn off the previous light if last active collider set
        if (lastColliderIdx >= 0)
        {
            colliders[lastColliderIdx].Toggle();
        }
        colliders[randIdx].Toggle();
        lastColliderIdx = randIdx;
    }

    // Function called in BoxCollider while a player is touching any of the box colliders.
    // Collider parameter is the collider that the player is currently touching.
    public void TouchingLight(BoxCollider collider)
    {
        // Check if Oculus A button is pressed.
        if (OVRInput.Get(OVRInput.Button.One) && lastColliderIdx >= 0)
        {
            // Check if touching the current enabled box-collider.
            BoxCollider activeCollider = colliders[lastColliderIdx];
            if (activeCollider == collider) {
                // Turn off light
                activeCollider.Toggle();
                // Set last active collider to null
                lastColliderIdx = -1;
                // Set the timer to a random time between 0 and 3 seconds
                timer = Random.Range(0.0f, 8.0f);
                // Increment the score.
                score += 1;
                scoreText.text = "Score: " + score;
            }
        }
    }
}
