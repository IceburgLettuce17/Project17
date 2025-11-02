namespace UnityEditor
{
	internal class Project17TickTimerHelper
	{
		private double m_NextTick;

		private double m_Interval;

		public Project17TickTimerHelper(double intervalBetweenTicksInSeconds)
		{
			m_Interval = intervalBetweenTicksInSeconds;
		}

		public bool DoTick()
		{
			if (EditorApplication.timeSinceStartup > m_NextTick)
			{
				m_NextTick = EditorApplication.timeSinceStartup + m_Interval;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			m_NextTick = 0.0;
		}
	}
}
