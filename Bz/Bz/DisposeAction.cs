using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz
{
    /// <summary>
    /// 此类提供一个action当Dispose被调用的时候
    /// </summary>
    public class DisposeAction:IDisposable
    {
        private readonly Action _action;
        public DisposeAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("DisposeAction->action");
            }

            _action = action;
        }
        public void Dispose()
        {
            _action();
        }
    }
}
