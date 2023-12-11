﻿using UnityEngine;

namespace UnityCommon.ScriptGeneration.Test
{

	[AutoGenerate("%t%Test", false, "float", "int", "Vector2", "Vector3")]
	public abstract class TestClass<T> : MonoBehaviour
	{

		[SerializeField] private T data;


	}

}
