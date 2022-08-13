using UnityEditor;
using UnityEngine;

namespace Additions.UI
{
	[CustomEditor(typeof(RetractableUiElement))]
	public class RetractableUiElementEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var panel = (RetractableUiElement) target;

			if (GUILayout.Button("Copy Show Position")) panel.CopyShowPosition();

			if (GUILayout.Button("Copy Hidden Position")) panel.CopyHiddenPosition();
		}
	}
}