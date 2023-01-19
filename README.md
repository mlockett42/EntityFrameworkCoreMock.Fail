# EntityFrameworkCoreMock.Fail

Demonstrate a bug with EntityFrameworkCoreMock.Moq when using EF Core 7.

Open the project in Visual Studio 2022 and run the unit tests. The test name FailingTest1 fails, it should pass. There is discussion in a comment about what I think is happening here.

This code is extracted from one of our internal projects, the same code works with EF Core 6 and 5 in other projects.

