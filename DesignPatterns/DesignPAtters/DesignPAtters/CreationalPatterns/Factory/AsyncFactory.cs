using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns.Factory
{
    public class AsyncFoo
    {
        private AsyncFoo()
        {
        }

        private async Task<AsyncFoo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<AsyncFoo> CreateAsync()
        {
            var asyncFoo = new AsyncFoo();
            return asyncFoo.InitAsync();
        }
    }
}
