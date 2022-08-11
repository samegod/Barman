using DG.Tweening;
using UnityEngine;

public static class DOTweenUtils
{
	public static void KillIfValid(this Tween tween)
	{
		if (tween?.IsActive() ?? false)
			tween.Kill();
	}

	public static Tween DORotateAround(this Transform transform, float time)
	{
		return transform.DORotate(new Vector3(0, 0, -180), time / 2, RotateMode.LocalAxisAdd)
			.OnComplete(() =>
				transform.DORotate(new Vector3(0, 0, -180), time / 2, RotateMode.LocalAxisAdd)
					.SetEase(Ease.Linear))
			.SetEase(Ease.Linear);
	}
}