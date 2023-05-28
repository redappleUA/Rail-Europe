using BezierSolution;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class TrainMoveController : MonoBehaviour
{
    public event Action<Way> OnTrainStop;

    [SerializeField] BezierWalkerWithSpeed _walker;
    [SerializeField] Train _train;
    [SerializeField, Range(0f, 2f)] float _timeToStopSplineWalker, _timeToStopBeforeTranslating;

    private const float MAX_DISTANCE_TO_TRANSLATE = 1f;

    private List<BezierSpline> _splines = new();
    private int _currentSplineIndex = 0;
    private bool _isForward = true;

    void Start()
    {
        _walker.onPathCompleted.AddListener(SwitchSpline);
        _walker.spline = _train.Route.WaysBetweenCities[0].GetComponent<BezierSpline>();
        AddSplines(ref _splines);

        _walker.speed = _train.Speed;
    }

    private void SwitchSpline() => StartCoroutine(NextSpline());

    private IEnumerator NextSpline()
    {
        StartCoroutine(_walker.StopWalkerForSeconds(_timeToStopSplineWalker));
 
        if (_isForward)
        {
            _currentSplineIndex++;
            if (_currentSplineIndex >= _splines.Count)
            {
                _currentSplineIndex = _splines.Count - 1;
                _isForward = false;
            }
        }
        else
        {
            _currentSplineIndex--;
            if (_currentSplineIndex < 0)
            {
                _currentSplineIndex = 0;
                _isForward = true;
            }
        }
        BezierSpline previousSpline = _walker.spline, nextSpline = _splines[_currentSplineIndex];

        if (CheckDistance(previousSpline[^1].transform.position, nextSpline[^1].transform.position, nextSpline[0].transform.position))
        {
            nextSpline.InvertSpline();
        }

        yield return new WaitForSeconds(_timeToStopBeforeTranslating);

        OnTrainStop(previousSpline.GetComponent<Way>());

        yield return StartCoroutine(TranslateObject(nextSpline[0].transform.position)); //Translating to the nest spline

        _walker.spline = nextSpline;
    }

    private void AddSplines(ref List<BezierSpline> splines)
    {
        foreach(var way in _train.Route.WaysBetweenCities)
        {
            var spline = way.gameObject.GetComponent<BezierSpline>();
            splines.Add(spline); //TODO: SerializeField for BezierSpline
        }
    }
    /// <summary>
    /// ѕерев≥р€Ї в≥дстань м≥ж останньою точкою та точками наступного сплайну
    /// </summary>
    /// <param name="lastPointOfPrevoius">ќстанн€ точка попереднього сплайну</param>
    /// <param name="firstPointOfNext">ѕерша точка наступного сплайну</param>
    /// <param name="secondPointOfNext">ƒруга точка наступного сплайну</param>
    /// <returns>якщо в≥дстань м≥ж першою та останньою менша за в≥дстань м≥ж другою та останньою - true. ≤накше - false</returns>
    private bool CheckDistance(Vector2 lastPointOfPrevoius, Vector2 firstPointOfNext, Vector2 secondPointOfNext)
    {
        if (Vector2.Distance(lastPointOfPrevoius, firstPointOfNext) < Vector2.Distance(lastPointOfPrevoius, secondPointOfNext))
            return true;
        else
            return false;
    }

    private IEnumerator TranslateObject(Vector2 targetPosition)
    {
        if (Vector2.Distance(_walker.transform.position, targetPosition) > MAX_DISTANCE_TO_TRANSLATE)
            yield break;

        while ((Vector2)_walker.transform.position != targetPosition)
        {
            _walker.transform.position = Vector2.MoveTowards(_walker.transform.position, targetPosition, Time.deltaTime * _train.Speed);
            yield return null;
        }
    }
}
