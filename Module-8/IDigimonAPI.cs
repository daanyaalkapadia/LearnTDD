using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTDD.Module_8
{
    public interface IDigimonAPI
    {
        Task<string> GetNameById(string token, int id);
    }
}
