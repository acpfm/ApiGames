using System;

namespace ApiCatalogGames.Exceptions
{
    public class NotExistingGame: Exception
    {
        public NotExistingGame()
            :base("This is game not existing")
        {}
    }
}
