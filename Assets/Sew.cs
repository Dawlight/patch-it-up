using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sew : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private RunMachine _runMachine;

    public List<Vector3> threadPoints;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stichWidth;
    [SerializeField] private AnimationCurve needleHeightCurve;

    void Start()
    {
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

        threadPoints[threadPoints.Count - 1] = new Vector3(_runMachine.transform.localPosition.x, _runMachine.transform.localPosition.y, -0.01f);
        lineRenderer.SetPosition(threadPoints.Count -1, _runMachine.transform.localPosition);

        if (_runMachine.hasCompletedCycle)
        {
            threadPoints.Add(new Vector3(_runMachine.transform.localPosition.x, _runMachine.transform.localPosition.y, -0.01f));
            lineRenderer.positionCount = threadPoints.Count;
            lineRenderer.SetPositions(threadPoints.ToArray());

            var tempPosition = _startPosition;
            _startPosition = _endPosition;
            _endPosition = tempPosition;
        }

        var transformPosition = Vector3.Lerp(_startPosition, _endPosition, _runMachine.lerpTime);
        transformPosition.z = -needleHeightCurve.Evaluate(Mathf.Clamp(_runMachine.lerpTime, 0, 1));
        transform.position = transformPosition;
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