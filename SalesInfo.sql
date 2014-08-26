CREATE TABLE [dbo].[Sales] (
    [SalesRecordId] INT IDENTITY (1, 1) NOT NULL,
    [AgentId]       INT NOT NULL,
    [TerritoryId]   INT NOT NULL,
    [ProductId]     INT NOT NULL,
	[Quantity]		INT	Not Null,
    PRIMARY KEY CLUSTERED ([SalesRecordId] ASC),
    FOREIGN KEY ([AgentId]) REFERENCES [dbo].[Agent] ([AgentId]),
    FOREIGN KEY ([TerritoryId]) REFERENCES [dbo].[Territory] ([TerritoryId]),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId])
);

