

using System;

[System.Serializable]
/// - Serializable : 클래스나 구조체를 직렬화시킵니다.
/// - SerializeField : 필드를 직렬화 시킵니다.
public struct BestDataInfo
{
	public bool empty;

	public int year;
	public int month;
	public int day;

	public int hour;
	public int minute;
	public int second;

	public double bestScore;

	public BestDataInfo(DateTime date, double bestScore, bool empty)
	{
		this.empty = empty;

		year = date.Year;
		month = date.Month;
		day = date.Day;

		hour = date.Hour;
		minute = date.Minute;
		second = date.Second;

		this.bestScore = bestScore;
	}
}
