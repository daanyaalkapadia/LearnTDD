using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTDD.Module_8
{
    public class DigimonControllerShould
    {
        private readonly DigimonController _digitalController;
        public DigimonControllerShould()
        {
            _digitalController = new DigimonController();
        }

        [Fact]
        public void Return_Name()
        {
            string login = "dk";
            string password = "dk";
            int id = 1;
            var result = _digitalController.GetNameById(login, password, id);

            result.Should().Be("");
        }
    }
    public class DigimonController
    {
        public string GetNameById(string login, string password, int id)
        {
            return "";
        }
    }
}
