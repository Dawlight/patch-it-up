using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Running,
        ScoreScreen,
        LevelEnded
    }

    private GameState _state;
    private float _levelTimer;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private RunMachine runMachine;
    [SerializeField] private Sew sew;
    [SerializeField] private PolygonCollider2D patchCollider;
    
    void Start()
    {
        _state = GameState.Ready;
        timeText.enabled = false;
        scoreText.enabled = false;
    }

    void Update()
    {
        switch (_state)
        {
            case GameState.Ready:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _state = GameState.Running;
                    _levelTimer = 0;
                }
                break;
            case GameState.Running:
                runMachine.SetEnabled(true);
                _levelTimer += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _state = GameState.ScoreScreen;
                }
                break;
            case GameState.ScoreScreen:
                timeText.enabled = true;
                timeText.text = $"Time: {_levelTimer:0.##} s.";
                runMachine.SetEnabled(false);

                var noOfOverlappingThreadPoints = 0;
                var threadPositionInWorldSpace = sew.GetThreadPositionInWorldSpace();
                foreach (var threadPoint in threadPositionInWorldSpace)
                {
                    if (patchCollider.OverlapPoint(threadPoint))
                    {
                        noOfOverlappingThreadPoints++;
                    }   
                }
                
                var overlappingRatio = noOfOverlappingThreadPoints / (float) sew.threadPoints.Count;
                var absoluteRatio = Mathf.Abs(overlappingRatio - 0.5f) * 2;
                var result = 1 - absoluteRatio;
                
                var threadPointBounds = new Bounds();
                foreach (var threadPoint in threadPositionInWorldSpace)
                {
                    threadPointBounds.Encapsulate(threadPoint);
                }

                var threadArea = threadPointBounds.size.x * threadPointBounds.size.y;
                var patchArea = patchCollider.bounds.size.x * patchCollider.bounds.size.y;

                var coveredPatchArea = threadArea / patchArea;
                
                // if (threadPointBounds.Contains(patchCollider.bounds.max) && threadPointBounds.Contains(patchCollider.bounds.min))
                if (coveredPatchArea >= 0.8f)
                {
                    scoreText.text = $"Score: {(result*100):0.##} %";
                }
                else
                {
                    scoreText.text = "Cover the area, fool!";
                }
                
                scoreText.enabled = true;

                _state = GameState.LevelEnded;
                break;
            case GameState.LevelEnded:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(0);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
