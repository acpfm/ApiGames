using System;

namespace ApiCatalogGames.Exceptions
{
    public class ExistingGameException : Exception
    {
        public ExistingGameException()
            : base("This is game existing")
        { }
    }
}
