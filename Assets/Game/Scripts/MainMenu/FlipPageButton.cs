using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
	[RequireComponent(typeof(Button))]
	public class FlipPageButton : MonoBehaviour
	{
		private enum FlipDirection
		{
			Next,
			Back
		}

		[SerializeField] private Book book;
		[SerializeField] private FlipDirection flipDirection;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(OnButtonClicked);
		}

		private void OnButtonClicked()
		{
			if(flipDirection == FlipDirection.Back)
				book.PreviousPage();
			else
				book.NextPage();
		}
	}
}