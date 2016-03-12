--CREATE DATABASE BabaBaidyaNathMVC;
GO

USE BabaBaidyaNathMVC
GO

------------------------------------------------Users------------------------------------------------
CREATE TABLE Users (
	UserID NVARCHAR(300) NOT NULL PRIMARY KEY,
	Email NVARCHAR(100) NOT NULL UNIQUE,
	FName NVARCHAR(100) NOT NULL,
	MName NVARCHAR(100) NOT NULL,
	LName NVARCHAR(100) NOT NULL,
	[Password] NVARCHAR(200) NOT NULL,
	IsPwdReset SMALLINT NOT NULL DEFAULT 0,
	Gender VARCHAR(1) NOT NULL,
	Dob DATETIME NOT NULL,
	Age INT NULL,
	MariedSatus SMALLINT NOT NULL DEFAULT 0,
	SunShine NVARCHAR(100) NULL,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	IsNew SMALLINT NOT NULL DEFAULT 1,
	DttmCreate DATETIME DEFAULT GETDATE(),
	DttmModified DATETIME DEFAULT GETDATE()
	)
GO

------------------------------------------------[Address]------------------------------------------------
CREATE TABLE [Address] (
	AddressID NVARCHAR(300) NOT NULL PRIMARY KEY DEFAULT NEWID(),
	UserID NVARCHAR(300),
	CareOf NVARCHAR(30),
	Address1 NVARCHAR(100),
	Address2 NVARCHAR(100),
	City NVARCHAR(20),
	[State] NVARCHAR(20),
	Country NVARCHAR(20) NOT NULL DEFAULT('Ind'),
	MobileNo NVARCHAR(10) NOT NULL,
	PhoneNo NVARCHAR(10),
	DttmCreated DATETIME DEFAULT GETDATE(),
	DttmModified DATETIME DEFAULT GETDATE() FOREIGN KEY (USERID) REFERENCES [Users](userID)
	)
GO

------------------------------------------------UserLoginDetails------------------------------------------------
CREATE TABLE UserLoginDetails (
	UserLoginDetailsID NVARCHAR(300) NOT NULL PRIMARY KEY DEFAULT NEWID(),
	UserID NVARCHAR(300),
	LoginDateTime DATETIME DEFAULT GETDATE(),
	LogOutDateTime DATETIME DEFAULT GETDATE(),
	IsActive SMALLINT DEFAULT 0 FOREIGN KEY (USERID) REFERENCES [Users](userID)
	)
GO

------------------------------------------------GroupsTypes------------------------------------------------
CREATE TABLE GroupsTypes (
	GroupsTypeID BIGINT NOT NULL PRIMARY KEY,
	GroupType NVARCHAR(250) NOT NULL,
	GroupDetails NVARCHAR(250),
	IsActive SMALLINT NOT NULL DEFAULT 1
	)
GO

------------------------------------------------Groups------------------------------------------------
CREATE TABLE Groups (
	GroupID NVARCHAR(300) NOT NULL PRIMARY KEY DEFAULT NEWID(),
	GroupsTypeID BIGINT NULL,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreate DATETIME DEFAULT GETDATE(),
	DttmModified DATETIME DEFAULT GETDATE() FOREIGN KEY (GroupsTypeID) REFERENCES GroupsTypes(GroupsTypeID)
	)
GO

------------------------------------------------UserGroups------------------------------------------------
CREATE TABLE UserGroups (
	UserID NVARCHAR(300),
	GroupID NVARCHAR(300)
	FOREIGN KEY (UserID) REFERENCES [Users](UserID),
	FOREIGN KEY (GroupID) REFERENCES Groups(GroupID)
	)
GO

------------------------------------------------Wishes------------------------------------------------
CREATE TABLE Wishes (
	WisheID NVARCHAR(300) NOT NULL PRIMARY KEY DEFAULT NEWID(),
	UserID NVARCHAR(300),
	Title NVARCHAR(50),
	WishDetail NVARCHAR(max),
	DttmCreated DATETIME DEFAULT GETDATE(),
	DttmModified DATETIME DEFAULT GETDATE() FOREIGN KEY (USERID) REFERENCES [Users](userID)
	)
GO

------------------------------------------------InformationType------------------------------------------------
CREATE TABLE InformationType (
	InfTypeID INT PRIMARY KEY,
	InfType NVARCHAR(250) NOT NULL UNIQUE,
	Infdetail NVARCHAR(500),
	IsActive SMALLINT NOT NULL DEFAULT 1,
	IsDeleted SMALLINT NOT NULL DEFAULT 0
	)
GO

------------------------------------------------Informations------------------------------------------------
CREATE TABLE Informations (
	InformationID BIGINT PRIMARY KEY,
	InfParentID BIGINT NULL,
	InfTypeID INT,
	InfTitle NVARCHAR(250) NOT NULL,
	Information NVARCHAR(MAX) NULL,
	InfTitleInd NVARCHAR(250) NOT NULL,
	InformationInd NVARCHAR(MAX) NULL,
	InfOrder INT NOT NULL,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	IsDeleted SMALLINT NOT NULL DEFAULT 0,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE() FOREIGN KEY (InfTypeID) REFERENCES InformationType(InfTypeID),
	FOREIGN KEY (InfParentID) REFERENCES Informations(InformationID)
	)
GO

------------------------------------------------ImageType------------------------------------------------
CREATE TABLE ImageType (
	ImageTypeID INT PRIMARY KEY,
	ImageType NVARCHAR(250),
	ImageDesp NVARCHAR(500),
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE()
	)
GO

------------------------------------------------AlbumTypes------------------------------------------------
CREATE TABLE AlbumTypes (
	AlbumTypeID INT PRIMARY KEY,
	AlbumType NVARCHAR(250),
	AlbumDesp NVARCHAR(500),
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE()
	)
GO

------------------------------------------------Albums------------------------------------------------
CREATE TABLE Albums (
	AlbumID NVARCHAR(300) PRIMARY KEY DEFAULT NEWID(),
	AlbumTypeID INT,
	AlbumTitle NVARCHAR(250) NOT NULL,
	AlbumInformation NVARCHAR(MAX) NOT NULL,
	CoverUrl NVARCHAR(500) NULL,
	AlbumLikeCount INT NOT NULL DEFAULT 0,
	AlbumOrder INT NOT NULL,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE() FOREIGN KEY (AlbumTypeID) REFERENCES AlbumTypes(AlbumTypeID)
	)
GO

------------------------------------------------Videos------------------------------------------------
CREATE TABLE Videos (
	VideoID NVARCHAR(300) PRIMARY KEY DEFAULT NEWID(),
	AlbumID NVARCHAR(300),
	VideoTitle NVARCHAR(250) NOT NULL,
	VideoInformation NVARCHAR(MAX) NOT NULL,
	VideoUrl NVARCHAR(500) NOT NULL,
	NavigateUrl NVARCHAR(500) NOT NULL,
	VideoOrder INT NOT NULL,
	VideoLikeCount INT NOT NULL DEFAULT 0,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE() FOREIGN KEY (AlbumID) REFERENCES Albums(AlbumID)
	)
GO

------------------------------------------------Images------------------------------------------------	 
CREATE TABLE Images (
	ImageID NVARCHAR(300) PRIMARY KEY DEFAULT NEWID(),
	ImageTypeID INT,
	AlbumID NVARCHAR(300),
	ImageTitle NVARCHAR(250) NOT NULL,
	ImageInformation NVARCHAR(MAX) NOT NULL,
	ImageUrl NVARCHAR(500) NOT NULL,
	NavigateUrl NVARCHAR(500) NOT NULL,
	ImageOrder INT NOT NULL,
	ImageLikeCount INT NOT NULL DEFAULT 0,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE() FOREIGN KEY (ImageTypeID) REFERENCES ImageType(ImageTypeID),
	FOREIGN KEY (AlbumID) REFERENCES Albums(AlbumID)
	)
GO

------------------------------------------------ServiceTypes------------------------------------------------
CREATE TABLE ServiceTypes (
	ServiceTypeID INT PRIMARY KEY,
	ServiceType NVARCHAR(250),
	ServiceDesp NVARCHAR(500),
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE()
	)
GO

------------------------------------------------Services------------------------------------------------
CREATE TABLE Services (
	ServiceID BIGINT PRIMARY KEY,
	ServiceParentID BIGINT,
	ServiceTypeID INT,
	ServiceTitle NVARCHAR(350) NULL,
	ServiceInformation NVARCHAR(MAX) NULL,
	ServiceTitleInd NVARCHAR(350) NULL,
	ServiceInformationInd NVARCHAR(MAX) NULL,
	ServiceIcon NVARCHAR(200),
	ServiceLikeCount INT NOT NULL DEFAULT 0,
	ServiceOrder INT NOT NULL,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE() FOREIGN KEY (ServiceTypeID) REFERENCES ServiceTypes(ServiceTypeID),
	FOREIGN KEY (ServiceParentID) REFERENCES Services(ServiceID)
	)
GO

------------------------------------------------ErrorLog------------------------------------------------
CREATE TABLE ErrorLog (
	ErrorLogID NVARCHAR(300) PRIMARY KEY DEFAULT NEWID(),
	ErrorType NVARCHAR(200) NOT NULL,
	ProcedureName NVARCHAR(200) NOT NULL,
	CustomMesage NVARCHAR(200) NOT NULL,
	ErrorNumber NVARCHAR(MAX) NOT NULL,
	ErrorMessage NVARCHAR(MAX) NOT NULL
	)
GO

------------------------------------------------Mantra------------------------------------------------	
CREATE TABLE Mantra (
	MantraID BIGINT PRIMARY KEY,
	MantraParentID BIGINT,
	MantraTitle NVARCHAR(350) NULL,
	MantraInformation NVARCHAR(MAX) NULL,
	MantraTitleInd NVARCHAR(350) NULL,
	MantraInformationInd NVARCHAR(MAX) NULL,
	MantraIcon NVARCHAR(200),
	MantraLikeCount INT NOT NULL DEFAULT 0,
	MantraOrder INT NOT NULL,
	IsActive SMALLINT NOT NULL DEFAULT 1,
	DttmCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DttmModified DATETIME NOT NULL DEFAULT GETDATE()
	)
GO

------------------------------------------------TotslVistors------------------------------------------------
CREATE TABLE TotslVistors (
	TotslVistorsID INT,
	totalHit BIGINT
	)
GO


