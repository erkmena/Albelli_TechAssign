using Albelli_TechAssign.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Albelli_TechAssign.Helper
{

    public class ConfigureDB
    {
        private readonly IConfiguration _configuration;

        private readonly string CreateTablesQuery = "CREATE TABLE [ORDER](" +
       "OrderId int IDENTITY(1,1)," +
       "CardId int NOT NULL," +
       "ReqBinWidth decimal NULL," +
       "CustomerNameSurname NVARCHAR(200));" +
       "CREATE TABLE[CARD](" +
       "CardId int IDENTITY(1,1),);" +
       "        CREATE TABLE[CARDITEM](" +
       "CardItemId int IDENTITY(1,1)," +
       "CardId int NOT NULL," +
       "Quantity INT NULL," +
       "ProductTypeId INT)" +
       "CREATE TABLE[PRODUCTTYPE](" +
       "ProductTypeId int IDENTITY(1,1)," +
       "Width decimal NOT NULL," +
       "MaxStockSize int NOT NULL," +
       "ProductTypeName NVARCHAR(100))" +
       "ALTER TABLE [Order]" +
       "ADD CONSTRAINT PK_Order_OrderId PRIMARY KEY CLUSTERED(OrderId);" +
       "        ALTER TABLE[Card]" +
       "ADD CONSTRAINT PK_Card_CardId PRIMARY KEY CLUSTERED(CardId);" +
       "        ALTER TABLE[CardItem]" +
       "   ADD CONSTRAINT PK_CardItem_CardItemId PRIMARY KEY CLUSTERED(CardItemId);" +
       "        ALTER TABLE[ProductType]" +
       "   ADD CONSTRAINT PK_ProductType_ProductTypeId PRIMARY KEY CLUSTERED(ProductTypeId);        " +
       "ALTER TABLE[Order]" +
       "   ADD CONSTRAINT FK_Order_Card FOREIGN KEY(CardId)      " +
       "REFERENCES[Card] (CardId)" +
       "     ON DELETE CASCADE" +
       "      ON UPDATE CASCADE;" +
       "ALTER TABLE[CardItem]" +
       "  ADD CONSTRAINT FK_Card_CardItem FOREIGN KEY(CardId)" +
       "     REFERENCES[Card] (CardId)" +
       "     ON DELETE CASCADE" +
       "      ON UPDATE CASCADE;" +
       "ALTER TABLE[CardItem]" +
       "   ADD CONSTRAINT FK_CardItem_ProductType FOREIGN KEY(ProductTypeId)" +
       "      REFERENCES[ProductType] (ProductTypeId)" +
       "     ON DELETE CASCADE" +
       "     ON UPDATE CASCADE;";

        private readonly string CreateDbQuery = "CREATE DATABASE Albelli_Tech_Kadir";
        private readonly string CheckIsDbCreatedQuery = "SELECT * FROM sys.databases WHERE name = 'Albelli_Tech_Kadir'";


        public ConfigureDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure()
        {
            if (IsDbNotCreated())
            {
                CreateDb();
                CreateTables();
            };
        }

        private void CreateTables()
        {
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                connection.Execute(CreateTablesQuery);
            }
        }

        private void CreateDb()
        {
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString(true)))
            {
                connection.Execute(CreateDbQuery);
            }
        }

        private bool IsDbNotCreated()
        {
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString(true)))
            {
                bool isDbNotCreated = connection.Query<string>(CheckIsDbCreatedQuery).AsList().Count == 0;
                return isDbNotCreated;
            }
        }
    }
}
