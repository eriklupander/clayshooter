using UnityEngine;
using System.Collections;

public class HitInfo {
	private float time;
	private float distance;
	private float velocity;
	//private Vector3 position;

	public HitInfo(float time, float distance, float velocity) {
		this.time = time;
		this.distance = distance;
		this.velocity = velocity;
	}

	public float Time {
		get { 
			return time;
		}
	}
	public float Distance {
		get { 
			return distance;
		}
	}
	public float Velocity {
		get { 
			return velocity;
		}
	}
}
