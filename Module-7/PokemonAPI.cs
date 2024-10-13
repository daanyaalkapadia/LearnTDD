using FluentAssertions;

namespace LearnTDD.Module_7
{
    public class PokemonAPIShould
    {
        private PokemonAPI _pokemonAPI;
        public PokemonAPIShould()
        {
            _pokemonAPI = new PokemonAPI();
        }
        [Theory]
        [InlineData(1, "Charmander", PokemonType.Fire)]
        [InlineData(2, "Squirtle", PokemonType.Water)]
        public void ReturnPokemonById(int id, string name, PokemonType pokemonType)
        {
            var pokemon = _pokemonAPI.GetPokemonById(id);
            pokemon.Name.Should().Be(name);
            pokemon.Type.Should().Be(pokemonType);
        }
    }
    public interface IPokemonAPI
    {
        Pokemon GetPokemonById(int id);
    }
    public class PokemonAPI : IPokemonAPI
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
