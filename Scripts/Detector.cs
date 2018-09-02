using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EMT.Detector
{
	class DetectorEvent : UnityEvent<GameObject> { }

	public class Detector : MonoBehaviour
	{
		public LayerMask playerLayer;

		Dictionary<string, DetectorEvent> enterEvents = new Dictionary<string, DetectorEvent>();
		Dictionary<string, DetectorEvent> exitEvents = new Dictionary<string, DetectorEvent>();
		Dictionary<string, DetectorEvent> stayEvents = new Dictionary<string, DetectorEvent>();

		//
		// Unity methods
		//

		void OnTriggerStay(Collider other)
		{
			InvokeListener(this.stayEvents, other);
		}

		void OnTriggerEnter(Collider other)
		{
			InvokeListener(this.enterEvents, other);
		}

		void OnTriggerExit(Collider other)
		{
			InvokeListener(this.exitEvents, other);
		}

		//
		// API
		//

		public void OnDetectEnter(string layerName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.enterEvents, layerName, listener);
		}

		public void OnDetectExit(string layerName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.exitEvents, layerName, listener);
		}

		public void OnDetectStay(string layerName, UnityAction<GameObject> listener)
		{
			UpdateListener(this.stayEvents, layerName, listener);
		}

		//
		// Helpers
		//

		void InvokeListener(Dictionary<string, DetectorEvent> dict, Collider other)
		{
			string layerName = GetLayerFor(other.gameObject);
			DetectorEvent enterEvent;

			if (dict.TryGetValue(layerName, out enterEvent))
			{
				enterEvent.Invoke(other.gameObject);
			}
		}

		void UpdateListener(Dictionary<string, DetectorEvent> dict, string key, UnityAction<GameObject> listener)
		{
			if (!dict.ContainsKey(key))
				dict.Add(key, new DetectorEvent());

			DetectorEvent layerEvent;

			if (dict.TryGetValue(key, out layerEvent))
			{
				layerEvent.AddListener(listener);
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

		//
		// Test
		//

		void Start()
		{
			this.OnDetectEnter("Default", this.TestMethod);
			this.OnDetectStay("Water", this.TestMethod);
		}

		void TestMethod(GameObject go)
		{
			Debug.Log(go.name);
		}
	}
}
