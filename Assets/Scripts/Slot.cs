﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public GameObject item
	{
		get
		{
			if (transform.childCount > 0)
			{
				return transform.GetChild(0).gameObject;
			}

			return null;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (!item)
		{
			DragHandler.draggedItem.transform.SetParent(transform);
		}
	}
}
