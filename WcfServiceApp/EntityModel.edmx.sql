
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/06/2017 11:20:52
-- Generated from EDMX file: C:\Users\Kristhel Domingo\Documents\Visual Studio 2015\Projects\MvcWcfEF\WcfServiceApp\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DirectoryDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PhysicianContactInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactInfoes] DROP CONSTRAINT [FK_PhysicianContactInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PhysicianSpecialization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Specializations] DROP CONSTRAINT [FK_PhysicianSpecialization];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ContactInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactInfoes];
GO
IF OBJECT_ID(N'[dbo].[Physicians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Physicians];
GO
IF OBJECT_ID(N'[dbo].[Specializations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specializations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ContactInfoes'
CREATE TABLE [dbo].[ContactInfoes] (
    [Id] int  NOT NULL,
    [HomeAddress] nvarchar(max)  NOT NULL,
    [HomePhone] bigint  NULL,
    [OfficeAddress] nvarchar(max)  NOT NULL,
    [OfficePhone] bigint  NOT NULL,
    [EmailAdd] nvarchar(max)  NOT NULL,
    [CellphoneNumber] bigint  NULL
);
GO

-- Creating table 'Physicians'
CREATE TABLE [dbo].[Physicians] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Weight] int  NULL,
    [Height] int  NULL
);
GO

-- Creating table 'Specializations'
CREATE TABLE [dbo].[Specializations] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ContactInfoes'
ALTER TABLE [dbo].[ContactInfoes]
ADD CONSTRAINT [PK_ContactInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Physicians'
ALTER TABLE [dbo].[Physicians]
ADD CONSTRAINT [PK_Physicians]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Specializations'
ALTER TABLE [dbo].[Specializations]
ADD CONSTRAINT [PK_Specializations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'ContactInfoes'
ALTER TABLE [dbo].[ContactInfoes]
ADD CONSTRAINT [FK_PhysicianContactInfo]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Physicians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Specializations'
ALTER TABLE [dbo].[Specializations]
ADD CONSTRAINT [FK_PhysicianSpecialization]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Physicians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------