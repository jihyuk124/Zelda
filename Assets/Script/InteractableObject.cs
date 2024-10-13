using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public enum InteractableType
	{
		None,
		Collectible,
		Dialogue,
	}
	private InteractableType type;
	public InteractableType Type => type;
}
