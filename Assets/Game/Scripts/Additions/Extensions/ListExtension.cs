using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Additions.Extensions
{
	public static class ListExtension
	{
		public static IEnumerable<Vector3> Positions(this List<Transform> transforms) =>
			transforms.Select(x => x.transform.position);

		public static List<Transform> Positions<T>(this List<T> transforms) where T : MonoBehaviour =>
			transforms.Select(x => x.transform).ToList();

		public static IEnumerable<Vector3> LocalPositions(this List<Transform> transforms) =>
			transforms.Select(x => x.transform.localPosition);

		public static T GetRandomElement<T>(this IList<T> list) =>
			list[UnityEngine.Random.Range(0, list.Count)];

		public static T GetRandomElement<T>(this IList<T> list, Func<T, bool> predicate)
		{
			int max = list != null && list.Count != 0
				? list.Count
				: throw new ArgumentException("Get Random Element from empty list");

			int index = UnityEngine.Random.Range(0, max);
			int num = index;
			T randomElement;

			do
			{
				randomElement = list[index];
				if (predicate(randomElement))
					return randomElement;
				index = (index + 1) % max;
			} while (index != num);

			return randomElement;
		}

		public static int GetRandomIndex<T>(this IList<T> list, Func<T, bool> predicate)
		{
			int max = list != null && list.Count != 0
				? list.Count
				: throw new ArgumentException("Get Random Element from empty list");

			int num1 = UnityEngine.Random.Range(0, max);
			int num2 = num1;
			int index;

			do
			{
				index = num1;
				if (predicate(list[index]))
					return index;
				num1 = (num1 + 1) % max;
			} while (num1 != num2);

			return index;
		}

		public static T GetRandomElement<T>(this Transform container, Func<Transform, T> predicate)
		{
			int childCount = container.childCount;
			if (childCount == 0)
				return default(T);

			int index = UnityEngine.Random.Range(0, childCount);
			int num = index;

			do
			{
				T randomElement = predicate(container.GetChild(index));
				if ((object) randomElement != null)
					return randomElement;
				index = (index + 1) % childCount;
			} while (index != num);

			return default(T);
		}

		public static T[] CopyArray<T>(this T[] array)
		{
			int length = array.Length;
			var destinationArray = new T[length];
			Array.Copy(array, destinationArray, length);

			return destinationArray;
		}

		public static List<T> Shuffle<T>(this List<T> list, bool createCopy = true)
		{
			var list1 = createCopy ? new List<T>(list) : list;
			list1.ShuffleThis();

			return list1;
		}

		public static T[] Shuffle<T>(this T[] array, bool createCopy = true)
		{
			T[] list = createCopy ? array.CopyArray() : array;
			list.ShuffleThis();

			return list;
		}

		public static void ShuffleThis<T>(this IList<T> list)
		{
			int count = list.Count;
			if (count < 2)
				return;

			for (; count > 0; --count)
			{
				int index = UnityEngine.Random.Range(0, count);
				(list[count - 1], list[index]) = (list[index], list[count - 1]);
			}
		}
	}
}