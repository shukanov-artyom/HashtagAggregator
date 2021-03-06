﻿using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;

namespace HashtagAggregator.Data.Contracts.Interface.JobObjects
{
    public interface IBackgroundJob<in T>
    {
        ICommandResult Publish(T command);
    }
}
