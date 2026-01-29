USE [PayrollDb]
GO

/****** Object:  StoredProcedure [dbo].[usp_CreateEmployee]    Script Date: 1/30/2026 03:36:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jan Eris Saluldo
-- Create date: 2026-01-29
-- Description:	Create Employee 
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[usp_CreateEmployee]
	@FirstName NVARCHAR(50),
	@MiddleName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50),
	@DateOfBirth DATE,
	@DailyRate DECIMAL(18,2),
	@WorkingDays INT	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Id UNIQUEIDENTIFIER = NEWID();

	DECLARE @EmployeeNumber NVARCHAR(50);
	DECLARE @RandomDigits NVARCHAR(5);

	SET @RandomDigits = RIGHT('00000' + CAST(ABS(CHECKSUM(NEWID())) % 100000 AS VARCHAR(5)), 5);
	SET @EmployeeNumber = LEFT(UPPER(@LastName + '***'), 3) + '-'  + @RandomDigits + '-' + UPPER(FORMAT(@DateOfBirth, 'ddMMMyyyy'));
		
	INSERT INTO tbl_Employee
	(
		Id,
		EmployeeNumber,
		FirstName,
		MiddleName,
		LastName,
		DateOfBirth,
		DailyRate,
		WorkingDays,
		CreatedAt
	)
	OUTPUT
		INSERTED.Id,
		INSERTED.EmployeeNumber
	VALUES
	(
		@Id,
		@EmployeeNumber,
		@FirstName,
		@MiddleName,
		@LastName,
		@DateOfBirth,
		@DailyRate,
		@WorkingDays,
		GETDATE()
	)   
END
GO

