using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] private Color[] colors;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
	private void Start()
	{
		foreach (Color color in colors)
		{
			Debug.Log(color);
		}
	}

	// Update is called once per frame
	private void Update()
	{
		int index = (int)Time.time % colors.Length;
		spriteRenderer.color = colors[index];
	}
}
