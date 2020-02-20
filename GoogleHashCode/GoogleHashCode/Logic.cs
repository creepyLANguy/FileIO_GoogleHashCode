using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleHashCode
{
    public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : System.IComparable
    {
        #region IComparer<TKey> Members
        public int Compare(TKey x, TKey y)
        {
            int result = x.CompareTo(y);
            if (result == 0)
                return 1;   // Handle equality as beeing greater
            else
                return result;
        }
        #endregion
    }

    class Book
    {
        public int id;
        public int score;
    }

    class Library
    {
        public int id;
        public int signupDays;
        public int booksPerDay;

        public long maxScore;

        //ublic SortedList<int, Book> books = new SortedList<int, Book>(new DuplicateKeyComparer<int>());
        public List<KeyValuePair<int, Book>> books_plain = new List<KeyValuePair<int, Book>>();

        public List<int> booksChosen = new List<int>();
    }

    class Logic
    {
        public static long GetMaxScore(int maxDays, Library lib)
        {
            lib.booksChosen = new List<int>();

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
            for (int j = 0; j < booksToGet && j < lib.books_plain.Count; j++)
            {
                Book book = lib.books_plain[j].Value;
                int val = book.score;
                sum += val;
                lib.booksChosen.Add(book.id);
            }

            return sum;
        }

        public static string Run(List<int> properties, string bookScores, List<string> inputLibrariesList)
        {

            StringBuilder buffer = new StringBuilder();

            List<Library> selectedLibraries = new List<Library>();

            int bookCount = properties[0];
            int libraryCount = properties[1];
            int maxDays = properties[2];

            List<int> bookScoresList = Helpers.StringToList<int>(bookScores);

            SortedList<long, Library> libraryList = new SortedList<long,Library>(new DuplicateKeyComparer<long>());

            for (int i = 0; i < inputLibrariesList.Count; i += 2)
            {
                if (inputLibrariesList[i] == "")
                {
                    continue;
                }

                List<int> libProps = Helpers.StringToList<int>(inputLibrariesList[i]);

                List<int> bookProps = Helpers.StringToList<int>(inputLibrariesList[i + 1]);

                SortedList<int, Book> booksList = new SortedList<int, Book>(new DuplicateKeyComparer<int>());
                for (int bookIndex = 0; bookIndex < bookProps.Count; bookIndex++)
                {
                    int currentBookId = bookProps[bookIndex];
                    int currentScore = bookScoresList[currentBookId];
                    booksList.Add(currentScore, new Book() { id = currentBookId, score = currentScore });
                }

                Library lib = new Library()
                {
                    id = i/2,
                    signupDays = libProps[1],
                    booksPerDay = libProps[2],
                    //books = booksList,
                    books_plain = booksList.Reverse().ToList()
                };

                lib.maxScore = GetMaxScore(maxDays, lib);
                libraryList.Add(lib.maxScore, lib);
            }

            Library chosen = libraryList.ElementAt(libraryList.Count - 1).Value;

            selectedLibraries.Add(chosen);

            maxDays -= chosen.id;

            libraryList.RemoveAt(libraryList.Count - 1);


            for (int i = 0; i < maxDays && libraryList.Count > 0; i++)
            {

                SortedList<long, Library> libraryListReplacer = new SortedList<long, Library>(new DuplicateKeyComparer<long>());

                foreach (var libPair in libraryList)
                {
                    Library l = libPair.Value;
                    l.maxScore = GetMaxScore(maxDays, l);
                    libraryListReplacer.Add(l.maxScore, l);
                }

                libraryList = libraryListReplacer;

                chosen = libraryList.ElementAt(libraryList.Count - 1).Value;

                selectedLibraries.Add(chosen);

                maxDays -= chosen.signupDays;

                libraryList.RemoveAt(libraryList.Count - 1);
            }

            buffer.Append(selectedLibraries.Count + "\n");

            foreach (var s in selectedLibraries)
            {
                buffer.Append(s.id + " ");
                buffer.Append(s.booksChosen.Count + "\n");
                foreach(var b in s.booksChosen)
                {
                    buffer.Append(b + " ");
                }
                buffer.Append("\n");
            }

            return buffer.ToString();
        }

    }
}
