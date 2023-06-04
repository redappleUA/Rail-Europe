using BezierSolution;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMoveController : MonoBehaviour
{
    public event Action<Way> OnTrainStop;

    [SerializeField] BezierWalkerWithSpeed _walker;
    [SerializeField] Train _train;
    [SerializeField, Range(0f, 2f)] float _timeToStopSplineWalker, _timeToStopBeforeTranslating;

    private const float MAX_DISTANCE_TO_TRANSLATE = 1f;

    BezierSpline previousSpline, nextSpline, newSpline;
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

        // Знищити новий сплайн після того, як другий walker перейшов на наступний сплайн
        if (newSpline != null)
        {
            Destroy(newSpline.gameObject);
            _walker.spline = _splines[_currentSplineIndex];

            if(_walker.spline == _train.Route.WaysBetweenCities[0].GetComponent<BezierSpline>() ||
                _walker.spline == _train.Route.WaysBetweenCities[^1].GetComponent<BezierSpline>())
            {
                _walker.spline.InvertSpline();
            }
        }

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
        previousSpline = _walker.spline; 
        nextSpline = _splines[_currentSplineIndex];

        yield return new WaitForSeconds(_timeToStopBeforeTranslating);

        OnTrainStop(previousSpline.GetComponent<Way>());

        if (CheckSplineForTrains(nextSpline))
        {
            // Створити новий сплайн на місці першого сплайна і інвертувати його
            newSpline = Instantiate(nextSpline, nextSpline.transform.position, nextSpline.transform.rotation);

            if (CheckDistance(previousSpline[^1].transform.position, newSpline[^1].transform.position, newSpline[0].transform.position) || (previousSpline == newSpline))
            {
                newSpline.InvertSpline();
            }

            _walker.spline = newSpline; // Встановити новий сплайн для другого walker

            yield return StartCoroutine(TranslateObject(newSpline[0].transform.position)); // Translating to the next spline
        }
        else
        {
            if (CheckDistance(previousSpline[^1].transform.position, nextSpline[^1].transform.position, nextSpline[0].transform.position) || (previousSpline == nextSpline))
            {
                nextSpline.InvertSpline();
            }

            _walker.spline = nextSpline;
            yield return StartCoroutine(TranslateObject(nextSpline[0].transform.position)); //Translating to the next spline    
        }
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
    /// Перевіряє відстань між останньою точкою та точками наступного сплайну
    /// </summary>
    /// <param name="lastPointOfPrevoius">Остання точка попереднього сплайну</param>
    /// <param name="firstPointOfNext">Перша точка наступного сплайну</param>
    /// <param name="secondPointOfNext">Друга точка наступного сплайну</param>
    /// <returns>Якщо відстань між першою та останньою менша за відстань між другою та останньою - true. Інакше - false</returns>
    private bool CheckDistance(Vector2 lastPointOfPrevoius, Vector2 firstPointOfNext, Vector2 secondPointOfNext)
    {
        if (Vector2.Distance(lastPointOfPrevoius, firstPointOfNext) < Vector2.Distance(lastPointOfPrevoius, secondPointOfNext))
            return true;
        else
            return false;
    }

    private IEnumerator TranslateObject(Vector2 targetPosition)
    {
        while ((Vector2)_walker.transform.position != targetPosition)
        {
            if (Vector2.Distance(_walker.transform.position, targetPosition) > MAX_DISTANCE_TO_TRANSLATE)
                yield break;

            _walker.transform.position = Vector2.MoveTowards(_walker.transform.position, targetPosition, Time.deltaTime * _train.Speed);
            yield return null;
        }
    }

    private bool CheckSplineForTrains(BezierSpline spline)
    {
        foreach(var train in TrainService.Trains)
        {
            if (train == _train) continue;

            var trainSpline = train.GetComponent<BezierWalkerWithSpeed>().spline;

            if(trainSpline == spline)
                return true;
        }
        return false;
    }
}
