﻿using System.Collections.Generic;

namespace MyStudyProject.Core.Contracts.Interface
{
    public interface IMapper<TModel, TEntity>
    {
        TEntity MapBunch(IEnumerable<TModel> messages, string hashtag);
    }
}
