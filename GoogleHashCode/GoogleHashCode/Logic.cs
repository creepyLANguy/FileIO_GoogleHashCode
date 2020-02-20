using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode
{
    class Book
    {
        public int id;
        public int score;
    }

    class Library
    {
        public int signupDays;
        public int booksPerDay;

        public long maxScore;

        public SortedList<int, Book> books = new SortedList<int, Book>();
        //public List<KeyValuePair<int, Book>> books_plain = new List<KeyValuePair<int, Book>>();
    }

    class Logic
    {
        public static long GetMaxScore(int maxDays, Library lib)
        {
            if (lib.signupDays >= maxDays)
            {
                return -1;
            }

            int usableDays = maxDays - lib.signupDays;

            int sum = 0;

            //for(int i = 0; i < usableDays && i < lib.books.Count; ++ lib.booksPerDay)
            //{
            //    for(int j = 0; j < lib.booksPerDay; ++j)
            //    {
            //        sum += lib.books[j].score;
            //    }
            //}
            int booksToGet = usableDays / lib.booksPerDay + usableDays % lib.booksPerDay;
            for (int j = 0; j < booksToGet && j < lib.books.Count; j++)
            {
                sum += lib.books_plain[j].Value.score;
            }

            return sum;
        }

        public static string Run(List<int> properties, string bookScores, List<string> librariesList)
        {

            string buffer = "";

            int bookCount = properties[0];
            int libraryCount = properties[1];
            int maxDays = properties[2];

            List<int> bookScoresList = Helpers.StringToList<int>(bookScores);

            SortedList<long, Library> libraryList = new SortedList<long,Library>();

            for(int i = 0; i < librariesList.Count; i+=2)
            {

                List<int> libProps = Helpers.StringToList<int>(librariesList[i]);

                List<int> bookProps = Helpers.StringToList<int>(librariesList[i + 1]);

                SortedList<int, Book> booksList = new SortedList<int, Book>();
                for(int bookIndex = 0; bookIndex < bookProps.Count; bookIndex++)
                {
                    int currentBookId = bookProps[bookIndex];
                    int currentScore = bookScoresList[currentBookId];
                    booksList.Add(currentScore, new Book() { id = currentBookId, score = currentScore });
                }

                Library lib = new Library()
                {
                    signupDays = libProps[1],
                    booksPerDay = libProps[2],
                    books = booksList,
                    books_plain = booksList.Reverse().ToList()
                };

                lib.maxScore = GetMaxScore(maxDays, lib);

                libraryList.Add(lib.maxScore, lib);
            }



            //List<KeyValuePair<long, Library>> libraries_plain = libraryList.Reverse().ToList();





            return buffer;
        }

    }
}
