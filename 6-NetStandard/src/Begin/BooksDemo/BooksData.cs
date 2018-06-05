using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BooksDemo
{
    public static class BooksData
    {
        private static DataSet GetDataSet()
        {
            DataSet dataSet = new DataSet();

            dataSet.ReadXml(@"..\..\..\DataSource\Books.xml");

            return dataSet;
        }

        public static List<String> GetBooksAsList()
        {
            var result = new List<string>();

            var booksTable = GetDataSet().Tables[0];

            foreach (DataRow row in booksTable.Rows)
            {
                var title = Convert.ToString(row["title"]);
                var publisher = Convert.ToString(row["publisher"]);

                result.Add($"{title} - {publisher}");
            }
            return result;
        }

        public static String GetBooks()
        {
            StringBuilder result = new StringBuilder();

            var booksTable = GetDataSet().Tables[0];

            foreach (DataRow row in booksTable.Rows)
            {
                var title = Convert.ToString(row["title"]);
                var publisher = Convert.ToString(row["publisher"]);

                result.AppendLine($"{title} - {publisher}");
            }
            return result.ToString();
        }
    }
}