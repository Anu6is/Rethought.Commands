﻿using Optional;
using Rethought.Commands.Actions;
using Rethought.Extensions.Optional;

namespace Rethought.Commands.Strategies
{
    public class Prototype<TContext> : IStrategy<TContext>
    {
        private readonly IAsyncAction<TContext> asyncAction;

        public Prototype(IAsyncAction<TContext> asyncAction)
        {
            this.asyncAction = asyncAction;
        }

        public IAsyncAction<TContext> Invoke(Option<IAsyncAction<TContext>> nextAsyncActionOption)
        {
            return nextAsyncActionOption.TryGetValue(out var nextAction)
                ? Actions.Enumerating.Enumerator<TContext>.Create(asyncAction, nextAction)
                : asyncAction;
        }
    }
}