USE [PayrollDb]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetByEmployee]    Script Date: 1/30/2026 03:36:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Jan Eris Saluldo
-- Create date: 2026-01-29
-- Description: Get employee by Id or EmployeeNumber
-- =============================================
ALTER PROCEDURE [dbo].[usp_GetByEmployee]
    @Id UNIQUEIDENTIFIER = NULL,
    @EmployeeNumber NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
      
    IF @Id IS NULL AND @EmployeeNumber IS NULL
    BEGIN
        RAISERROR('Either Id or EmployeeNumber must be provided.', 16, 1);
        RETURN;
    END

    SELECT
        Id,
        EmployeeNumber,
        FirstName,
        MiddleName,
        LastName,
        DateOfBirth,
        DailyRate,
        WorkingDays,
        ModifiedAt,
        CreatedAt,
        DeletedAt
    FROM tbl_Employee
    WHERE DeletedAt IS NULL
      AND (
            (@Id IS NOT NULL AND Id = @Id)
         OR (@Id IS NULL AND EmployeeNumber = @EmployeeNumber)
      );
END
GO

