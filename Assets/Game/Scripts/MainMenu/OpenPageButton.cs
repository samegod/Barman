using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
	[RequireComponent(typeof(Button))]
	public class OpenPageButton : MonoBehaviour
	{
		[SerializeField] private Book book;
		[SerializeField] private Page pageToOpen;
		
		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(OnButtonClicked);
		}

		private void OnButtonClicked()
		{
			book.OpenPage(pageToOpen);
		}
	}
}
