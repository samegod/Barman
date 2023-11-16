using System;

namespace Game
{
	public class LivesController
	{
		private readonly int _maxHealth;
		private int _currentHealth;

		public event Action OnHealthIsOver;

		public int CurrentHealth => _currentHealth;

		public LivesController(int maxHealth)
		{
			_maxHealth = maxHealth;

			_currentHealth = maxHealth;
		}

		public void IncreaseHealth(int count)
		{
			if(count <= 0) return;

			if (count + _currentHealth >= _maxHealth)
				_currentHealth = _maxHealth;

			_currentHealth += count;
		}

		public void DecreaseHealth(int count)
		{
			if (count <= 0) return;

			if (_currentHealth - count <= 0)
			{
				_currentHealth = 0;

				OnHealthIsOver?.Invoke();
			}

			_currentHealth -= count;
		}
	}
}