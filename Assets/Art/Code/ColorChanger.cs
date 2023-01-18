using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	public Color[] colors;

	private void Awake()
	{

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

	}
}
