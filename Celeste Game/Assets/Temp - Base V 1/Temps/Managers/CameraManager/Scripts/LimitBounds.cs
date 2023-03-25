using UnityEngine;

public class LimitBounds : MonoBehaviour
{
    PolygonCollider2D _bounds;
	
	void Awake()
	{
		_bounds = GetComponent<PolygonCollider2D>();
	}
	
	public void SetBounds(PolygonCollider2D value)
	{
		_bounds.points = value.points;
	}
}
