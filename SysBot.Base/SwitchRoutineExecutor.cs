﻿using System.Threading;
using System.Threading.Tasks;

namespace SysBot.Base
{
    /// <summary>
    /// Commands a Bot to a perform a routine asynchronously.
    /// </summary>
    public abstract class SwitchRoutineExecutor
    {
        public readonly SwitchConnectionAsync ConnectionAsync;
        protected SwitchRoutineExecutor(string ip, int port) => ConnectionAsync = new SwitchConnectionAsync(ip, port);

        /// <summary>
        /// Connects to the console, then runs the bot.
        /// </summary>
        /// <param name="token">Cancel this token to have the bot stop looping.</param>
        public async Task RunAsync(CancellationToken token)
        {
            await ConnectionAsync.Connect().ConfigureAwait(false);
            await MainLoop(token).ConfigureAwait(false);
            ConnectionAsync.Disconnect();
        }

        protected abstract Task MainLoop(CancellationToken token);

        public async Task Click(SwitchButton b, int delay, CancellationToken token)
        {
            await ConnectionAsync.Send(SwitchCommand.Click(b), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }
    }
}