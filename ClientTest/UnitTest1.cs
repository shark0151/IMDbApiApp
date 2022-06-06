using IMDbApiLib.Models;
using imdbClientButWorking;
using imdbClientButWorking.Models;

namespace ClientTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestGetMovie()
        {
            DatabaseController databaseController = new DatabaseController();

            TitleData movie =  new TitleData();
            movie = await databaseController.GetMovieFromImdbTask("tt1375666");

            Assert.IsNotNull(movie);
        }

        [TestMethod]
        public async Task TestSearchMovie()
        {
            DatabaseController databaseController = new DatabaseController();

            SearchData results = new SearchData();
            results = await databaseController.SearchImdbTask("Inception");

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task TestGetFavList()
        {
            DatabaseController databaseController = new DatabaseController();
            List<TitleData> favList = new List<TitleData>();
            favList = await databaseController.GetFavListAsync(1);

            Assert.IsNotNull(favList);
        }

        [TestMethod]
        public async Task CheckUserExist()
        {
            DatabaseController databaseController = new DatabaseController();
            bool userExists;
            userExists = await databaseController.CheckUserExists("test");

            Assert.IsTrue(userExists);
        }
        [TestMethod]
        public async Task CheckUserNotExist()
        {
            DatabaseController databaseController = new DatabaseController();
            bool userExists;
            userExists = await databaseController.CheckUserExists("test22");

            Assert.IsFalse(userExists);
        }
        [TestMethod]
        public async Task CheckCredentials()
        {
            DatabaseController databaseController = new DatabaseController();
            User user = new User();
            user.username = "test";
            user.password = "test123";
            bool loggedin;
            loggedin = await databaseController.CheckUserCredentials(user);

            Assert.IsTrue(loggedin);
        }

        [TestMethod]
        public async Task AddToFav()
        {
            DatabaseController databaseController = new DatabaseController();
            databaseController.UserId = 1;
            bool success = await databaseController.AddToFavListAsync("tt0126029");

            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task RemoveFromFav()
        {
            DatabaseController databaseController = new DatabaseController();
            databaseController.UserId = 1;
            bool success = await databaseController.DeleteFromFavListAsync("tt0126029");

            Assert.IsTrue(success);
        }

    }
}