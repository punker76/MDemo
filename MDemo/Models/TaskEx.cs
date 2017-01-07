﻿namespace MDemo.Models
{
    using System.Threading.Tasks;

    /// <summary>
    /// This is only a helper class for the NET45, cause in 4.5 exists no TaskEx
    /// </summary>
    public static class TaskEx
    {
        public static Task Delay(int dueTime)
        {
            return Task.Delay(dueTime);
        }
    }
}
