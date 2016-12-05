﻿using System;
using AutoMapper;
using MyStudyProject.Core.Models.Commands;
using MyStudyProject.Core.Models.Results.Query;
using MyStudyProject.ViewModels;

namespace MyStudyProject.Configuration
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<MessageQueryResult, MessageViewModel>();
            CreateMap<MessageQueryResult, MessageCreateCommand>();
            CreateMap<MessagesQueryResult, MessagesCreateCommand>();
        }
    }
}
