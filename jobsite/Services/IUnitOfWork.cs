using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public interface IUnitOfWork
    {
        // repos

        void Save();
    }
}
