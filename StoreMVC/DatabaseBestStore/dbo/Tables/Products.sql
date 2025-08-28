CREATE TABLE [dbo].[Products] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX)  NOT NULL,
    [Price]      DECIMAL (18, 2) NOT NULL,
    [ImageUrl]   NVARCHAR (MAX)  NULL,
    [IsFeatured] BIT             NOT NULL,
    [CategoryId] INT             NOT NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Products_dbo.Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE
);

