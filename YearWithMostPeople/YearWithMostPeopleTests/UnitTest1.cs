using Xunit;
using static YearWithMostPeople.Program;

namespace YearWithMostPeopleTests
{
    public class UnitTest
    {
        [Fact]
        public void FindsCorrectYear()
        {
            var People_1 = new TupleList<int, int>
            {
                { 1900,2000 },
                { 1950,2000 },
                { 1900,1950 },
                { 1925,1975 }
            };
            var People_2 = new TupleList<int, int>
            {
                { 1911,1951 },
                { 1935,1952 },
                { 1985,1999 },
                { 1965,1992 },
                { 1947,1966 }
            };
            var actual_1 = FindYearWithMost(People_1);
            int expected_1 = 1950;
            var actual_2 = FindYearWithMost(People_2);
            int expected_2 = 1951;
            Assert.Equal(actual_1, expected_1);
            Assert.Equal(actual_2, expected_2);
        }
    }
}
