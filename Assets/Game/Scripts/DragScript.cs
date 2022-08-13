using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    public bool isDragging = false;
    public Camera cam;
    public float pushForce;
    public Trajectory trajectory;
    Rigidbody rb;
    GameControl game;

    Vector3 startPoint;
    Vector3 endpoint;
    Vector3 direction;
    Vector3 force;
    float distance;

    bool started;
    bool ready;

    public void Start()
    {
        started = false;
        ready = true;

        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        trajectory = GameObject.FindGameObjectWithTag("Trajectory").GetComponent<Trajectory>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
    }

    public void Update()
    {
        if (isDragging)
        {
            OnDrag();
        }

        if(rb.velocity.x == 0 && started)
        {
            game.Pushed();
            started = false;
            rb.freezeRotation = false;
        }
    }

    private void OnMouseDown()
    {
        if (ready)
        {
            isDragging = true;
            OnDragStart();
        }
    }

    private void OnMouseUp()
    {
        if (ready)
        {
            isDragging = false;
            OnDragEnd();
            StartCoroutine(Wait());
            ready = false;
        }
    }

    void OnDragStart()
    {
        startPoint = Input.mousePosition;

        trajectory.Show();
    }

    void OnDrag()
    {
        endpoint = Input.mousePosition;
        distance = Vector2.Distance(startPoint, endpoint);
        direction = (startPoint - endpoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(this.transform.position, endpoint);

        trajectory.UpdateDots(this.transform.position, force);
    }

    void OnDragEnd()
    {
        isDragging = false;

        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(force.x, 0, 0));

        trajectory.Hide();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        started = true;
    }
}
