﻿using System;
using MyStudyProject.Core.Contracts.Interface.Cqrs.Command;

namespace MyStudyProject.Data.Contracts.Interface.JobObjects
{
    public interface IBackgroundJob<in T>
    {
        ICommandResult Publish(T command);
    }
}
