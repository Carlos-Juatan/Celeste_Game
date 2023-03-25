using UnityEngine;

public class LimitBoundsReferece : MonoBehaviour
{
    PolygonCollider2D _bounds;
	
	void Awake()
	{
		_bounds = GetComponent<PolygonCollider2D>();
	}
	
	public PolygonCollider2D GetBounds()
	{
		return this._bounds;
	}
}
