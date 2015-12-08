using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	protected int targetHitPoints;
	protected static int waveCount; // TODO get rid of static variable
	protected float waveTime;

	public int getTargetHitPoints()
	{
		return targetHitPoints;
	}

	public void setTargetHitPoints(int thp)
	{
		targetHitPoints = thp;
	}

	public int getWaveCount()
	{
		return waveCount;
	}

	public void setWaveCount(int wc)
	{
		waveCount = wc;
	}

	public float getWaveTime()
	{
		return waveTime;
	}

	public void setWaveTime(float wt)
	{
		waveTime = wt;
	}
}
