# TTHDotNetCore


C# .NET

C# Language
.NET 

Console App
Windows Forms
ASP.NET Core Web API
ASP.NET Core Web MVC
Blazor Web Assembly
Blazor Web Server

.NET framework (1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8) windows
.NET Core (1, 2, 2.2, 3, 3.1) vs2019, vs2022 - windows, linux, macos
.NET (5 - vs2019, 6 - vs2022, 7, 8 - windows, linux, macos

vscode
visual studio 2022 

windows

UI + Business Logic + Data Access => Database

Kpay

Mobile No => Transfer 

Mobile No Check
10000

SLH => Collin

10000 => 0

-5000 => +5000

Bank + 5000

TTHDotNetCore

```sql

SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]

GO

select * from Tbl_Blog where DeleteFlag = 0

update Tbl_Blog set BlogTitle = 'heehee2' where BlogId = 1
update Tbl_Blog set DeleteFlag = 1 where BlogId = 2

delete from Tbl_Blog where BlogId = 1





-- Product Apple 1000, Orange 1000
-- Staff Apple 2, Orange 1
-- 3000, 2000, 1000

```


select * from tbl_blog with (nolock)

commit data / uncommit data

insert into
commit

update tbl_blog
commit

1 - mg mg 1

2 - mg mg 2

3 - mg mg 3

4 - mg mg 4

5 - mg mg 5


1 - mg mg 1

2 - mg mg 2

3 - mg mg 6

4 - mg mg 4

5 - mg mg 5


efcore database first (manual, auto) / code first

dotnet tool install --global dotnet-ef --version 7 (write in cmd)


dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f (wtite in vs cmd)

API
HttpMethod
HttpStatusCode
Request / Response



-----------------------------
5 weeks

Visual Studio 2022 Installation

Microsoft SQL Server 2022

SSMS (SQL Server Management System)

C# Basic

SQL Basic

Console App (Create Project)

DTO (data transfer object)

Nuget Package

ADO.NET

Dapper

- ORM

- Data Model

- AsNoTracking

EFCore

- AppDbContext

- Database First

REST API (ASP.NET Core Web API)

- Swagger

- Postman

- Http Method

- Http Status Code

-----------------------------



Backend API

data model (data access, database) 10 columns

view model (frontend return data) 2 columns


CREATE TABLE ToDoList (
    TaskID INT PRIMARY KEY IDENTITY(1,1),  -- Unique task identifier
    TaskTitle VARCHAR(255) NOT NULL,       -- Title of the task
    TaskDescription TEXT,                  -- Detailed description of the task
    CategoryID INT,                        -- Foreign key to Category table (optional)
    Status VARCHAR(50) CHECK (Status IN ('Pending', 'In Progress', 'Completed', 'Overdue')),  -- Task status
    PriorityLevel TINYINT CHECK (PriorityLevel BETWEEN 1 AND 5), -- Task priority (1 = Low, 5 = High)
    DueDate DATE,                          -- Task due date
    CreatedDate DATETIME DEFAULT GETDATE(),-- When the task was created
    CompletedDate DATETIME,                -- When the task was marked as completed
    FOREIGN KEY (CategoryID) REFERENCES TaskCategory(CategoryID) -- Optional category reference
);

CREATE TABLE TaskCategory (
    CategoryID INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for each category
    CategoryName VARCHAR(100) NOT NULL        -- Name of the category (e.g., Work, Personal)
);
