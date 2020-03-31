using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadMath : MonoBehaviour 
{
	public int A;
	public int B;
	public int C;
	public bool POS;

	public double answer;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		answer = quadForm (A, B, C, POS);
	}

	static double quadForm(int a, int b, int c, bool pos)
	{
		var preRoot = b * b - 4 * a * c;
		if (preRoot < 0)
		{
			return double.NaN;
		}
		else
		{
			var sgn = pos ? 1.0 : -1.0;
			return (sgn * Mathf.Sqrt(preRoot) - b) / (2.0 * a);
		}
	}
}
