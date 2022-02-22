using System;
using System.Threading.Tasks;

namespace Extentions
{
    public static class TasksExtentions
    {
        public static async Task DoActionAfterSecondsAsync(this Action action, float seconds)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            action.Invoke();
        }



        public static async Task<T> DoActionAfterSecondsAsync<T>(this Func<T> action, float seconds)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            await Task.Delay(TimeSpan.FromSeconds(seconds));
            return action.Invoke();
        }
    }
}
