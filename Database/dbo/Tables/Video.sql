CREATE TABLE [dbo].[Video] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Description]  NVARCHAR (50)    NOT NULL,
    [ServerId]     UNIQUEIDENTIFIER NOT NULL,
    [RegisterDate] DATETIME         DEFAULT (getdate()) NOT NULL
);

