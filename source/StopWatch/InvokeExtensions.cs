using System.ComponentModel;
using System.Windows.Forms;

namespace StopWatch
{
    internal static class InvokeExtensions
    {
        /// <summary>
        /// Make thread-safing your UI controls easier
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
    }
}
