using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EMT
{
	class DetectorEvent : UnityEvent<GameObject> { }

	public class Detector : MonoBehaviour
	{
		Dictionary<string, DetectorEvent> layerEnterEvents = new Dictionary<string, DetectorEvent>();
		Dictionary<string, DetectorEvent> layerExitEvents = new Dictionary<string, DetectorEvent>();
		Dictionary<string, DetectorEvent> layerStayEvents = new Dictionary<string, DetectorEvent>();

		Dictionary<string, DetectorEvent> tagEnterEvents = new Dictionary<string, DetectorEvent>();
		Dictionary<string, DetectorEvent> tagExitEvents = new Dictionary<string, DetectorEvent>();
		Dictionary<string, DetectorEvent> tagStayEvents = new Dictionary<string, DetectorEvent>();

		//
		// Unity methods
		//

		void OnTriggerEnter(Collider other)
		{
			InvokeEnterListener(other.gameObject);
		}

		void OnTriggerStay(Collider other)
		{
			InvokeStayListener(other.gameObject);
		}

		void OnTriggerExit(Collider other)
		{
			InvokeExitListener(other.gameObject);
		}

		//
		// API
		//

		public void OnLayerEnter(string layerName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.layerEnterEvents, layerName, listener);
		}

		public void OnLayerExit(string layerName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.layerExitEvents, layerName, listener);
		}

		public void OnLayerStay(string layerName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.layerStayEvents, layerName, listener);
		}


		public void OnTagEnter(string tagName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.tagEnterEvents, tagName, listener);
		}

		public void OnTagExit(string tagName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.tagExitEvents, tagName, listener);
		}

		public void OnTagStay(string tagName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.tagStayEvents, tagName, listener);
		}

		//
		// Helpers
		//

		void InvokeEnterListener(GameObject other)
		{
			string layerName = GetLayerFor(other);
			DetectorEvent enterEvent;

			if (this.layerEnterEvents.TryGetValue(layerName, out enterEvent))
			{
				enterEvent.Invoke(other);
			}

			if (this.tagEnterEvents.TryGetValue(other.tag, out enterEvent))
			{
				enterEvent.Invoke(other);
			}
		}

		void InvokeStayListener(GameObject other)
		{
			string layerName = GetLayerFor(other);
			DetectorEvent enterEvent;

			if (this.layerStayEvents.TryGetValue(layerName, out enterEvent))
			{
				enterEvent.Invoke(other);
			}

			if (this.tagStayEvents.TryGetValue(other.tag, out enterEvent))
			{
				enterEvent.Invoke(other);
			}
		}

		void InvokeExitListener(GameObject other)
		{
			string layerName = GetLayerFor(other);
			DetectorEvent enterEvent;

			if (this.layerExitEvents.TryGetValue(layerName, out enterEvent))
			{
				enterEvent.Invoke(other);
			}

			if (this.tagExitEvents.TryGetValue(other.tag, out enterEvent))
			{
				enterEvent.Invoke(other);
			}
		}

		void UpdateListener(Dictionary<string, DetectorEvent> dict, string key, UnityAction<GameObject> listener)
		{
			if (!dict.ContainsKey(key))
				dict.Add(key, new DetectorEvent());

			DetectorEvent detectorEvent;

			if (dict.TryGetValue(key, out detectorEvent))
			{
				detectorEvent.AddListener(listener);
				return;
			}

			ThrowException();
		}

		void ThrowException()
		{
			throw new System.Exception("Error adding listener to detector");
		}

		string GetLayerFor(GameObject gameObject)
		{
			return LayerMask.LayerToName(gameObject.layer);
		}
	}
}
