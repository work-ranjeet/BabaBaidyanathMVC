USE BabaBaidyaNathMVC
GO

--------------------------------------------------User Table type------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'RegisterUser')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE RegisterUser
GO

IF EXISTS (
		SELECT *
		FROM sys.types st
		INNER JOIN sys.schemas ss
			ON st.schema_id = ss.schema_id
		WHERE st.NAME = N'UserTableType'
			AND ss.NAME = N'dbo'
		)
	DROP TYPE UserTableType
GO

CREATE TYPE UserTableType AS TABLE (UserID NVARCHAR(300), GroupType NVARCHAR(200), Email NVARCHAR(100) NULL, FName NVARCHAR(100) NULL, MName NVARCHAR(100) NULL, LName NVARCHAR(100) NULL, [Password] NVARCHAR(200) NULL, Gender VARCHAR(1) NULL, Dob DATETIME NULL, MariedSatus SMALLINT NULL, MobileNo NVARCHAR(20) NULL)
GO

-------------------------------------------------- Register User------------------------------------------------
CREATE PROCEDURE RegisterUser (@UserObject UserTableType READONLY)
AS
BEGIN
	BEGIN TRANSACTION

	BEGIN TRY
		DECLARE @MobileNo NVARCHAR(20)
		DECLARE @UserID NVARCHAR(300)
		DECLARE @GroupID NVARCHAR(300)
		DECLARE @GroupType INT

		SELECT @MobileNo = MobileNo
		FROM @UserObject

		SELECT @GroupType = GroupsTypeID
		FROM GroupsTypes
		WHERE GroupType = (
				SELECT GroupType
				FROM @UserObject
				)

		INSERT INTO Users (UserID,Email, FName, MName, LName, Password, Gender, Dob, Age, MariedSatus)
		SELECT UserID, Email, FName, MName, LName, Password, Gender, Dob, FLOOR(DATEDIFF(day, Dob, getDate()) / 365.25), MariedSatus
		FROM @UserObject

		SELECT @UserID = UserID
		FROM @UserObject
		--WHERE Email = (
		--		SELECT Email
		--		FROM @UserObject
		--		)

		INSERT INTO [Address] (UserID, MobileNo)
		VALUES (@UserID, @MobileNo)

		SELECT @GroupID = GroupID
		FROM Groups
		WHERE GroupsTypeID = @GroupType

		INSERT INTO UserGroups (UserID, GroupID)
		VALUES (@UserID, @GroupID)

		INSERT INTO UserLoginDetails (UserID, LoginDateTime, LogOutDateTime, IsActive)
		VALUES (@UserID, getdate(), getdate(), 1)

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'RegisterUser', 'Error from RegisterUser Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())

		ROLLBACK TRANSACTION
	END CATCH
END
GO

-------------------------------------------------- GetServices------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'GetServices')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE GetServices
GO

CREATE PROCEDURE GetServices (@ServiceType NVARCHAR(500), @IsActive SMALLINT)
AS
BEGIN
	BEGIN TRY
		SELECT SV.ServiceId, COALESCE(SV.ServiceParentID, '0') AS ServiceParentID, SVT.ServiceType, SV.ServiceTitle, SV.ServiceInformation, SV.ServiceTitleInd, SV.ServiceInformationInd, SV.ServiceIcon, SV.ServiceLikeCount, SV.ServiceOrder
		FROM Services SV
		LEFT JOIN ServiceTypes SVT
			ON SV.ServiceTypeID = SVT.ServiceTypeID
		WHERE SVT.ServiceType = @ServiceType
			AND SV.IsActive = @IsActive
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'GetServices', 'Error from GetServices Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
	--exec GetServices 'services' ,1
GO

-------------------------------------------------- Function [CountMantraChild]------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'CountMantraChild')
			AND type IN (
				N'FN'
				, N'IF'
				, N'TF'
				, N'FS'
				, N'FT'
				)
		)
	DROP FUNCTION CountMantraChild
GO

CREATE FUNCTION CountMantraChild (@MantraID INT, @MantraParentID INT)
RETURNS INT
	WITH EXECUTE AS CALLER
AS
BEGIN
	DECLARE @ChildCount INT

	SET @ChildCount = 0

	SELECT @ChildCount = ChildCount
	FROM (
		SELECT M.Mantraid, M.mantraParentID, COUNT(CH.MantraID) AS ChildCount
		FROM Mantra M
		LEFT JOIN Mantra Ch
			ON Ch.mantraParentID = M.Mantraid
		WHERE M.mantraParentID = @MantraParentID
			AND M.MantraID = @MantraID
		GROUP BY M.Mantraid, M.MantraParentID
		) AS ChildTable

	RETURN (@ChildCount)
END
GO

-----------------------------------------------------GetMantra------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'GetMantra')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE GetMantra
GO

USE [BabaBaidyaNathMVC]
GO

/****** Object:  StoredProcedure [dbo].[GetMantra]    Script Date: 08/26/2014 15:13:39 ******/
CREATE PROCEDURE GetMantra (@MantraID BIGINT = - 1, @MantraParentID BIGINT = - 1, @IsActive SMALLINT = 1)
AS
BEGIN
	BEGIN TRY
		IF @MantraID = - 1
		BEGIN
			IF @MantraParentID = - 1
			BEGIN
				SELECT MN.MantraId, COALESCE(MN.MantraParentID, '0') AS MantraParentID, MN.MantraTitle, MN.MantraInformation, MN.MantraTitleInd, MN.MantraInformationInd, MN.MantraIcon, MN.MantraLikeCount, MN.MantraOrder, DBO.CountMantraChild(MN.MantraId, 0) AS ChildCount
				FROM Mantra MN
				WHERE MN.IsActive = @IsActive
				ORDER BY MN.MantraOrder ASC
			END
			ELSE
				SELECT MN.MantraId, COALESCE(MN.MantraParentID, '0') AS MantraParentID, MN.MantraTitle, MN.MantraInformation, MN.MantraTitleInd, MN.MantraInformationInd, MN.MantraIcon, MN.MantraLikeCount, MN.MantraOrder, DBO.CountMantraChild(MN.MantraId, @MantraParentID) AS ChildCount
				FROM Mantra MN
				WHERE MN.IsActive = @IsActive
					AND MN.MantraParentID = @MantraParentID
				ORDER BY MN.MantraOrder ASC
		END
		ELSE
			SELECT MN.MantraId, COALESCE(MN.MantraParentID, '0') AS MantraParentID, MN.MantraTitle, MN.MantraInformation, MN.MantraTitleInd, MN.MantraInformationInd, MN.MantraIcon, MN.MantraLikeCount, MN.MantraOrder, DBO.CountMantraChild(MN.MantraId, 0) AS ChildCount
			FROM Mantra MN
			WHERE MN.IsActive = @IsActive
				AND MN.MantraId = @MantraID
			ORDER BY MN.MantraOrder ASC
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'GetMantras', 'Error from GetMantras Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
	--exec GetMantra 2, -1, 1
GO

-----------------------------------------------InsTotalVistors--------------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'InsTotalVistors')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE InsTotalVistors
GO

CREATE PROCEDURE InsTotalVistors
AS
BEGIN
	UPDATE TotslVistors
	SET totalHit = totalHit + 1
	WHERE TotslVistorsID = 1
END
GO

-----------------------------------------------SelectTotalVistors--------------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'SelectTotalVistors')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE SelectTotalVistors
GO

CREATE PROCEDURE SelectTotalVistors
AS
BEGIN
	SELECT TOP 1 totalHit
	FROM TotslVistors
	ORDER BY TotslVistorsID DESC
END
GO

-----------------------------------------------SelectUserViewLatestPost--------------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'SelectUserViewLatestPost')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE SelectUserViewLatestPost
GO

CREATE PROCEDURE SelectUserViewLatestPost (@InfTypeID BIGINT = 2, @IsActive SMALLINT = 1)
AS
BEGIN
	BEGIN TRY
		SELECT TOP 5 InformationId, InfTitle, Information, InfTitleind, InformationInd, InfOrder
		FROM Informations
		WHERE InfTypeID = @InfTypeID
			AND IsActive = @IsActive
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'SelectUserViewLatestPost', 'Error from SelectUserViewLatestPost Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
	--exec SelectUserViewLatestPost 2
GO

-----------------------------------------------SelectLatestPostDetail-------------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'SelectLatestPostDetail')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE SelectLatestPostDetail
GO

CREATE PROCEDURE SelectLatestPostDetail (@InformationID BIGINT, @IsActive SMALLINT = 1)
AS
BEGIN
	BEGIN TRY
		SELECT InformationId, InfTitle, Information, InfTitleind, InformationInd, InfOrder
		FROM Informations
		WHERE InformationId = @InformationID
			AND IsActive = @IsActive
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'SelectLatestPostDetail', 'Error from SelectLatestPostDetail Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
GO

--exec SelectLatestPostDetail 1
-----------------------------------------------GetUserProfileHeader-------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'GetUserProfileHeaderData')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
BEGIN
	DROP PROCEDURE GetUserProfileHeaderData
END
GO

CREATE PROCEDURE GetUserProfileHeaderData (@UserEmail NVARCHAR(100))
AS
BEGIN
	BEGIN TRY
		SELECT USR.UserID, USR.Email, USR.DttmCreate AS ProfileCreatedOn, '0.6' AS ProfileCompletionPercent, USRL.LogOutDateTime AS LastLoginDate, 'Leo' AS Horoscope
		FROM Users USR
		LEFT JOIN UserLoginDetails USRL
			ON USRL.UserID = USR.UserID
		WHERE USR.UserID = @UserEmail
			AND USR.IsActive = 1
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'GetUserProfileHeaderData', 'Error from GetUserProfileHeaderData Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END

GO

-----------------------------------------------GetUserProfileBasicData-------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'GetUserProfileBasicData')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE GetUserProfileBasicData
GO

CREATE PROCEDURE GetUserProfileBasicData (@UserEmail NVARCHAR(100))
AS
BEGIN
	BEGIN TRY
		SELECT USR.UserID, USR.FName, USR.MName, USR.LName, USR.dob AS DateOfBirth, USR.Age, USR.Gender, USR.MariedSatus AS MariedStatus, USR.SunShine
		FROM Users USR
		WHERE USR.UserID = @UserEmail
			AND USR.IsActive = 1
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'GetUserProfileBasicData', 'Error from GetUserProfileBasicData Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
GO

--exec GetUserProfileBasicData 's@s.com' 
-----------------------------------------------GetUserProfileLocationData-------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'GetUserProfileLocationData')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
BEGIN
	DROP PROCEDURE GetUserProfileLocationData
END
GO

CREATE PROCEDURE GetUserProfileLocationData (@UserEmail NVARCHAR(100))
AS
BEGIN
	BEGIN TRY
		SELECT USR.UserID, AD.Address1, AD.Address2, AD.City, AD.STATE, AD.Country, AD.MobileNo AS Mobile, USR.Email
		FROM Users USR
		LEFT JOIN Address AD
			ON AD.UserID = USR.UserID
		WHERE USR.UserID = @UserEmail
			AND USR.IsActive = 1
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'GetUserProfileLocationData', 'Error from GetUserProfileLocationData Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
GO

--exec GetUserProfileLocationData 's@s.com' 
---------------------------------------UpdateProfileBasic------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.types st
		INNER JOIN sys.schemas ss
			ON st.schema_id = ss.schema_id
		WHERE st.NAME = N'BasicProfileTableType'
			AND ss.NAME = N'dbo'
		)
BEGIN
	DROP TYPE BasicProfileTableType
END
GO

CREATE TYPE BasicProfileTableType AS TABLE (UserID NVARCHAR(300) NOT NULL, FName NVARCHAR(100) NULL, MName NVARCHAR(100) NULL, LName NVARCHAR(100) NULL, Gender VARCHAR(1) NULL, Dob DATETIME NULL, MariedSatus SMALLINT NULL)
GO

---------------------------------------UpdateProfileLocation---------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'UpdateProfileLocation')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE UpdateProfileLocation
GO

-------------------------------------------------- Albums ------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'InsertAlbums')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE InsertAlbums
GO

IF EXISTS (
		SELECT *
		FROM sys.types st
		INNER JOIN sys.schemas ss
			ON st.schema_id = ss.schema_id
		WHERE st.NAME = N'AlbumTableType'
			AND ss.NAME = N'dbo'
		)
	DROP TYPE AlbumTableType
GO

CREATE TYPE AlbumTableType AS TABLE (AlbumType NVARCHAR(150), AlbumTitle NVARCHAR(250), AlbumInformation NVARCHAR NULL, CoverUrl NVARCHAR(300) NULL, AlbumLikeCount INT, AlbumOrder INT, IsActive INT)
GO

CREATE PROCEDURE InsertAlbums (@AlbumTable AlbumTableType READONLY)
AS
BEGIN
	BEGIN TRANSACTION

	BEGIN TRY
		DECLARE @AlbumTypeID INT

		SELECT @AlbumTypeID = AlbumTypeID
		FROM AlbumTypes
		WHERE AlbumType = (
				SELECT AlbumType
				FROM @AlbumTable
				)

		INSERT INTO Albums (AlbumTypeID, AlbumTitle, AlbumInformation, CoverUrl, AlbumLikeCount, AlbumOrder, IsActive)
		SELECT @AlbumTypeID, AlbumTitle, AlbumInformation, CoverUrl, AlbumLikeCount, AlbumOrder, IsActive
		FROM @AlbumTable

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'InsertAlbums', 'Error from InsertAlbums Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())

		ROLLBACK TRANSACTION
	END CATCH
END
GO

-----------------------------------------------GetAlbums--------------------------------------------------------------------
IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'GetAlbums')
			AND type IN (
				N'P'
				, N'PC'
				)
		)
	DROP PROCEDURE GetAlbums
GO

CREATE PROCEDURE GetAlbums (@AlbumTypeID INT = NULL, @IsActive SMALLINT = 1)
AS
BEGIN
	BEGIN TRY
		IF @AlbumTypeID = NULL
		BEGIN
			SELECT AlbumID, AlbumTypeID, AlbumTitle, AlbumInformation, CoverUrl, AlbumLikeCount, AlbumOrder, IsActive
			FROM Albums
			WHERE IsActive = @IsActive
		END
		ELSE
		BEGIN
			SELECT AlbumID, AlbumTypeID, AlbumTitle, AlbumInformation, CoverUrl, AlbumLikeCount, AlbumOrder, IsActive
			FROM Albums
			WHERE IsActive = @IsActive
				AND AlbumTypeID = @AlbumTypeID
		END
	END TRY

	BEGIN CATCH
		INSERT INTO ErrorLog (ErrorType, ProcedureName, CustomMesage, ErrorNumber, ErrorMessage)
		VALUES ('Database Error', 'GetAlbums', 'Error from GetAlbums Store Procedure', ERROR_NUMBER(), ERROR_MESSAGE())
	END CATCH
END
	--exec GetAlbums 1
GO


