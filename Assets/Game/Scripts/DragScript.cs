using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragScript : MonoBehaviour
{
    [SerializeField] private float pushForce;

    [SerializeField, HideInInspector] private Rigidbody rb;

    private Trajectory _trajectory;

    private Vector3 _startPoint;
    private Vector3 _endpoint;
    private Vector3 _force;
    private float _distance;

    private bool _isDragging;
    private bool _started;
    private bool _ready;

    public event Action OnDragStop;

    public void Start()
    {
        _started = false;
        _ready = true;
    }

    public void Update()
    {
        if (_isDragging)
            OnDrag();

        if (rb.velocity.x != 0 || _started == false) return;

        _started = false;
        rb.freezeRotation = false;
    }

    public void Init(Trajectory trajectory) =>
        _trajectory = trajectory;

    private void OnMouseDown()
    {
        if (_ready)
        {
            _isDragging = true;
            OnDragStart();
        }
    }

    private void OnMouseUp()
    {
        if (_ready)
        {
            _isDragging = false;
            OnDragEnd();

            OnDragStop?.Invoke();

            StartCoroutine(Wait());
            _ready = false;
        }
    }

    private void OnDragStart()
    {
        _startPoint = Input.mousePosition;

        _trajectory.Show();
    }

    private void OnDrag()
    {
        _endpoint = Input.mousePosition;
        _distance = Vector2.Distance(_startPoint, _endpoint);
        _force = (_startPoint - _endpoint).normalized * _distance * pushForce;

        _trajectory.UpdateDots(transform.position, _force);
    }

    private void OnDragEnd()
    {
        _isDragging = false;

        rb.AddForce(new Vector3(_force.x, 0, 0));

        _trajectory.Hide();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        _started = true;
    }

    #region Editor

    private void OnValidate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    #endregion
}
