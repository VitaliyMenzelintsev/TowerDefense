using UnityEngine;

public class WayPoints : MonoBehaviour 
{

	public static Transform[] Points; //список точек маршрута
	void Awake ()
	{
		Points = new Transform[transform.childCount]; //обращаемся к дочернему обьекту
		for (int i = 0; i < Points.Length; i++)
        {
			Points[i] = transform.GetChild(i);
        }
	}
}
