using IMDbApiLib.Models;
using imdbClientButWorking;

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

    }
}