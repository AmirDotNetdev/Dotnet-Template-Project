﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepositories
{
    public interface IRepositoryWrapper
    {
        Task<int> SaveChangesAsync();
         int SaveChanges();
    }
}
