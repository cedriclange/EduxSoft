using CommonTest;
using Xunit;

namespace SecurityTest
{
    /// <summary>
    /// This class has no code, and is never created. Its purpose is simply
    /// to be the place to apply [CollectionDefinition] and all the
    /// ICollectionFixture<> interfaces.
    /// /!\ It should be defined in the same assembly than tests that uses it...
    /// </summary>
    /// <typeparam name="DatabaseFixture"></typeparam>
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}