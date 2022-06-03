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
        public async Task TestGetFavList()
        {
            DatabaseController databaseController = new DatabaseController();
            List<TitleData> favList = new List<TitleData>();
            favList = await databaseController.GetFavListAsync(1);

            Assert.IsNotNull(favList);
        }


    }
}