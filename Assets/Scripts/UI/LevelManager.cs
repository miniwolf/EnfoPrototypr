using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour{

	protected int targetHitPoint;
	protected static int waveCount; // TODO get rid of static variable
	protected float waveTime;

	public int getTargetHitPoints()
	{
		return targetHitPoint;
	}

	public void setTargetHitPoint(int thp)
	{
		targetHitPoint = thp;
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
