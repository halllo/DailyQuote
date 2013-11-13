using Microsoft.Phone.Scheduler;
using System;
using System.Windows;

namespace DailyQuote
{
	/// <summary>
	/// start background agents logic from Microsoft: http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202941(v=vs.105).aspx
	/// </summary>
	public class UpdateBackgroundAgent
	{
		PeriodicTask periodicTask;
		string periodicTaskName = "PeriodicAgent";
		public bool agentsAreEnabled = true;

		public void StartPeriodicAgent()
		{
			agentsAreEnabled = true;
			periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
			if (periodicTask != null)
			{
				RemoveAgent(periodicTaskName);
			}

			periodicTask = new PeriodicTask(periodicTaskName);
			periodicTask.Description = "This demonstrates a periodic task.";

			try
			{
				ScheduledActionService.Add(periodicTask);
				ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(20));
			}
			catch (InvalidOperationException exception)
			{
				if (exception.Message.Contains("BNS Error: The action is disabled"))
				{
					MessageBox.Show("Background agents for this application have been disabled by the user.");
					agentsAreEnabled = false;
				}

				if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
				{
					// No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
				}
			}
			catch (SchedulerServiceException)
			{
				// No user action required.
			}
		}

		private void RemoveAgent(string name)
		{
			try
			{
				ScheduledActionService.Remove(name);
			}
			catch (Exception)
			{
			}
		}
	}
}
