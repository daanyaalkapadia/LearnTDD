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
        [Theory]
        [InlineData(1, "Charmander", PokemonType.Fire)]
        [InlineData(2, "Squirtle", PokemonType.Water)]
        public void ReturnPokemonById(int id, string name, PokemonType pokemonType)
        {
            PokemonAPI pokemonAPI = new PokemonAPI();
            var pokemon = pokemonAPI.GetPokemonById(id);
            pokemon.Name.Should().Be(name);
            pokemon.Type.Should().Be(pokemonType);
        }
    }
    public class PokemonAPI
    {
        public Pokemon GetPokemonById(int id)
        {
            switch (id)
            {
                case 1:
                    return new Pokemon { Name = "Charmander", Type = PokemonType.Fire };
                case 2:
                    return new Pokemon { Name = "Squirtle", Type = PokemonType.Water };
                default:
                    return null;
            }
        }
    }
}
