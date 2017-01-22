﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyStudyProject.Core.Contracts.Interface.Cqrs.Command;
using MyStudyProject.Core.Cqrs.Abstract;
using MyStudyProject.Core.Models.Commands;
using MyStudyProject.Core.Models.Results.Command;
using MyStudyProject.Data.DataAccess.Context;
using MyStudyProject.Domain.Cqrs.EF.Assemblers;

namespace MyStudyProject.Domain.Cqrs.EF.Handlers
{
    public class EfMesssagesCreateCommandHandler : CommandHandler<MessagesCreateCommand>
    {
        private readonly SqlApplicationDbContext context;

        public EfMesssagesCreateCommandHandler(SqlApplicationDbContext context)
        {
            this.context = context;
        }

        public override async Task<ICommandResult> ExecuteAsync(MessagesCreateCommand command)
        {
            MessagesCommandToEntityMapper mapper = new MessagesCommandToEntityMapper();
            var items = mapper.MapBunch(command.Messages);
            var unique = items.Where(x => !context.Messages.Any(
                z => z.NetworkId == x.NetworkId && 
                z.UserId == x.UserId))
                .ToList();

            await context.Messages.AddRangeAsync(unique);
            context.SaveChanges();
            return new CommandResult { Success = true };
        }

        public override Task<ICommandResult> ExecuteAsync(List<MessagesCreateCommand> command)
        {
            throw new NotImplementedException();
        }
    }
}