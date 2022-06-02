using UnityEngine;

public class WayPoints : MonoBehaviour {

	public static Transform[] points; //список точек маршрута
	void Awake ()
	{
		points = new Transform[transform.childCount]; //обращаемся к дочернему обьекту
		for (int i = 0; i < points.Length; i++)
        {
			points[i] = transform.GetChild(i);
        }
	}
}
