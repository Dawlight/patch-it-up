using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sew : MonoBehaviour
{
    private float _timeToReverse;
    private float _currentNeedleTime;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private RunMachine _runMachine;

    public List<Vector3> threadPoints;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stichWidth;

    void Start()
    {
        _timeToReverse = 0.5f;
        _currentNeedleTime = 0;
        var position = transform.position;
        _startPosition = position + Vector3.left * stichWidth;
        _endPosition = position + Vector3.right * stichWidth;

        transform.position = _startPosition;

        _runMachine = GetComponent<RunMachine>();
        threadPoints = new List<Vector3>
        {
            _runMachine.transform.localPosition, 
            _runMachine.transform.localPosition
        };
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(threadPoints.ToArray());
    }

    void Update()
    {
        if (!_runMachine.isRunning) return;

        _currentNeedleTime += Time.deltaTime;
        var t = _currentNeedleTime / _timeToReverse;

        threadPoints[threadPoints.Count - 1] = _runMachine.transform.localPosition;
        lineRenderer.SetPosition(threadPoints.Count -1, _runMachine.transform.localPosition);

        if (_currentNeedleTime >= _timeToReverse)
        {
            threadPoints.Add(_runMachine.transform.localPosition);
            lineRenderer.positionCount = threadPoints.Count;
            lineRenderer.SetPositions(threadPoints.ToArray());

            _currentNeedleTime = 0;
            var tempPosition = _startPosition;
            _startPosition = _endPosition;
            _endPosition = tempPosition;
            t = 0;
        }

        transform.position = Vector3.Lerp(_startPosition, _endPosition, t);
    }

    public List<Vector3> GetThreadPositionInWorldSpace()
    {
        var positionsInWorldSpace = new List<Vector3>();
        foreach (var threadPoint in threadPoints)
        {
            Debug.Log($"Point: {threadPoint.ToString()}");
            Debug.Log($"World space: {lineRenderer.gameObject.transform.TransformPoint(threadPoint).ToString()}");
            positionsInWorldSpace.Add(lineRenderer.gameObject.transform.TransformPoint(threadPoint));
        }

        return positionsInWorldSpace;
    }
}