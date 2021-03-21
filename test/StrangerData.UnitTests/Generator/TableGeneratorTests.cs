﻿using Moq;
using StrangerData.Generator;
using Xunit;

namespace StrangerData.UnitTests.Generator
{
    public class TableGeneratorTests
    {
        [Fact]
        public void Constructor_TablesFromDifferentDatabasesWithSameName_GetTableSchemaInfoFromBothDatabases()
        {
            // Arrange
            Mock<IDbDialect> databaseOneDialectMock = new Mock<IDbDialect>();
            databaseOneDialectMock.Setup(d => d.ConnectionString)
                .Returns(Any.String());

            Mock<IDbDialect> databaseTwoDialectMock = new Mock<IDbDialect>();
            databaseTwoDialectMock.Setup(d => d.ConnectionString)
                .Returns(Any.String());

            string tableName = Any.String();

            TableGenerator databaseOneTableGenerator = new TableGenerator(databaseOneDialectMock.Object, tableName);

            // Act
            TableGenerator databaseTwoTableGenerator = new TableGenerator(databaseTwoDialectMock.Object, tableName);

            // Assert
            databaseOneDialectMock.Verify(d => d.GetTableSchemaInfo(tableName), Times.Once);
            databaseTwoDialectMock.Verify(d => d.GetTableSchemaInfo(tableName), Times.Once);
        }
    }
}
