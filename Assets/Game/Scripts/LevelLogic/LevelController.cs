using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using Game.Scripts.LevelLogic;
using Scripts.Enemy.LevelLogic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[Header("Camera")]
	[SerializeField] private FollowCamera followCamera;

	[Header("Beer"), Space(5)]
	[SerializeField] private Beer beer;
	[SerializeField] private Transform beerParent;
	[SerializeField] private Transform startPoint;

	[Header("Level Points"), Space(5)]
	[SerializeField] private List<LevelPoint> levelPoints;

	[Header("Health on level"), Space(5)]
	[SerializeField] private int lives = 5;

	[Header("Trajectory Component"), Space(5)]
	[SerializeField] private Trajectory trajectory;

	[Header("UiManager"), Space(5)]
	[SerializeField] private UiManager uiManager;

	private LivesController _livesController;

	private Beer _currentBeer;

	private bool _isActive;

	private void OnEnable() =>
		Beer.OnBeerStopped += CheckProgress;

	private void OnDisable() =>
		Beer.OnBeerStopped -= CheckProgress;

	private void Awake()
	{
		_livesController = new LivesController(lives);
		_livesController.OnHealthIsOver += LooseLevel;

		uiManager.Init(_livesController.CurrentHealth);
	}

	private void Start()
	{
		_isActive = true;

		InitBeer();

		followCamera.WaitAndOverview(3, levelPoints.Last().transform);
	}

	private void CheckProgress()
	{
		if (levelPoints.Any(levelPoint => levelPoint.IsComplete == false) && _isActive)
		{
			RespawnBeer();

			return;
		}

		CompleteLevel();
	}

	private void RespawnBeer()
	{
		_currentBeer.gameObject.SetActive(false);
		_currentBeer = null;

		_livesController.DecreaseHealth(1);
		uiManager.HideHealthPoint();

		InitBeer();
	}

	private void InitBeer()
	{
		if(_currentBeer != null) return;

		SpawnBeer();

		followCamera.SetTarget(_currentBeer.transform);
	}

	private void SpawnBeer()
	{
		var beerObj = BeerPool.Instance.Pop(beer);
		beerObj.Init(beerParent, startPoint.position, trajectory);

		_currentBeer = beerObj;
	}

	private void LooseLevel()
	{
		_isActive = false;

		uiManager.ShowLoosePanel();
	}

	private void CompleteLevel()
	{
		_isActive = false;

		uiManager.ShowWinPanel();
	}
}
