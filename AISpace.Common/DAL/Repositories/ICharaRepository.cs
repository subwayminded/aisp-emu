using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AISpace.Common.DAL.Entities;

namespace AISpace.Common.DAL.Repositories
{
    internal interface ICharaRepository
    {
        Task<User?> GetCharaByIdAsync(int charaId);
    }
}
