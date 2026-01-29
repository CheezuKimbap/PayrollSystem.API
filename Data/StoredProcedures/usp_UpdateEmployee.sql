USE [PayrollDb]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateEmployee]    Script Date: 1/30/2026 03:37:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Jan Eris Saluldo
-- Create date: 2026-01-29
-- Description: Update Employee
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[usp_UpdateEmployee]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(50),
    @MiddleName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50),
    @DateOfBirth DATE,
    @DailyRate DECIMAL(18,2),
    @WorkingDays INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewEmployeeNumber NVARCHAR(50);
    DECLARE @RandomDigits NVARCHAR(5);

    IF EXISTS (
        SELECT 1 FROM tbl_Employee 
            WHERE Id = @Id 
                AND DeletedAt IS NULL 
                and (LastName <> @LastName OR DateOfBirth <> @DateOfBirth)
    )
    BEGIN
      SET @RandomDigits = RIGHT('00000' + CAST(ABS(CHECKSUM(NEWID())) % 100000 AS VARCHAR(5)), 5);
	  SET @NewEmployeeNumber = LEFT(UPPER(@LastName + 'XXX'), 3) + '-'  + @RandomDigits + '-' + UPPER(FORMAT(@DateOfBirth, 'ddMMMyyyy'));
    END

   UPDATE tbl_Employee
    SET
        FirstName   = @FirstName,
        MiddleName  = @MiddleName,
        LastName    = @LastName,
        DateOfBirth = @DateOfBirth,
        DailyRate   = @DailyRate,
        WorkingDays = @WorkingDays,
        EmployeeNumber = COALESCE(@NewEmployeeNumber, EmployeeNumber),
        ModifiedAt  = SYSUTCDATETIME()
    OUTPUT
        INSERTED.Id,
        INSERTED.EmployeeNumber
    WHERE Id = @Id
      AND DeletedAt IS NULL;
END
GO

