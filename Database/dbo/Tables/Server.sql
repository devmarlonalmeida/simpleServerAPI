﻿CREATE TABLE [dbo].[Server]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Ip] NVARCHAR(50) NOT NULL, 
    [Port] INT NOT NULL
)