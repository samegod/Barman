using System.Collections.Generic;
using UnityEngine;

namespace Stories.Childrens.Field.CarRacing
{
	public class LevelProgress : MonoBehaviour
	{
		[SerializeField] private List<ProgressStar> stars;

		public void ShowStars (int number)
		{
			number = CorrectNumber(number);

			for (int i = 0; i < number; i++)
			{
				stars[i].Show();
			}
		}

		public void ShowStarsWithEffect (int number)
		{
			number = CorrectNumber(number);

			for (int i = 0; i < number; i++)
			{
				stars[i].ShowWithEffect();
			}
		}

		private int CorrectNumber (int number)
		{
			int correctedNumber = 0;

			if (number < 0)
				correctedNumber = 0;

			if (number > stars.Count)
				correctedNumber = stars.Count;

			return correctedNumber;
		}
	}
}
