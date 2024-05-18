CREATE TABLE [dbo].[MemberTbl] (
    [MId]     INT          IDENTITY (1, 1) NOT NULL,
    [MName]   VARCHAR (50) NOT NULL,
    [MPhone]  VARCHAR (50) NOT NULL,
    [MGen]    VARCHAR (6)  NOT NULL,
    [MAge]    INT          NOT NULL,
    [MAmount] INT          NOT NULL,
    [MTiming] VARCHAR (50) NOT NULL,
    [MEmail] VARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([MId] ASC)
);

