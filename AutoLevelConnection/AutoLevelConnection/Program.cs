using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AutoLevelConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dataSet = new DataSet("books");
            dataSet.ExtendedProperties.Add("Owner", "ITStep");//это метаданные о наборе 

            #region books columns
            DataTable booksTable = new DataTable("Books");
            booksTable.Columns.Add(new DataColumn
            {
                AllowDBNull = false,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                ColumnName = "id",
                DataType = typeof(int),
                Unique = true
            });

            booksTable.Columns.Add("name", typeof(string));
            booksTable.Columns.Add("price", typeof(int));
            booksTable.Columns.Add("author", typeof(string));
            #endregion

            #region books rows
            booksTable.Rows.Add("Сказки", 1000, 1);

            DataRow row = booksTable.NewRow();

            row.BeginEdit();
            // редактирование строк
            // между BeginEdit
            // EndEdit
            row.ItemArray = new object[] { "Сказки ч.2", 1200, 1 };
            row.SetAdded();
            row.EndEdit();

            booksTable.Rows.Add(row);

            #endregion

            dataSet.Tables.Add(booksTable);


            #region books columns
            DataTable authorTable = new DataTable("Author");
            booksTable.Columns.Add(new DataColumn
            {
                AllowDBNull = false,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                ColumnName = "id",
                DataType = typeof(int),
                Unique = true
            });

            authorTable.Columns.Add("name", typeof(string));

            #endregion

            #region books rows
            authorTable.Rows.Add("Пушкин А.С.", 1000, 1);
            #endregion


            dataSet.Tables.Add(authorTable);


            DataRelation dataRelation = new DataRelation("author_books_fk", "authors", "books", new string[] { "Id" }, new string[] { "authorId" }, false);

            dataSet.Relations.Add(dataRelation);

            
        }
    }
}
