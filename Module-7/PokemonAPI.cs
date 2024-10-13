using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTDD.Module_7
{
    public class PokemonAPIShould
    {
        [Fact]
        public void ReturnPokemonById()
        {
            PokemonAPI pokemonAPI = new PokemonAPI();
            var pokemon = pokemonAPI.GetPokemonById(1);
            pokemon.Should().NotBeNull();
        }
    }
    public class PokemonAPI
    {
        public Pokemon GetPokemonById(int id)
        {
            return new Pokemon();
        }
    }
}
