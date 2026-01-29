USE [PayrollDb]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetEmployees]    Script Date: 1/30/2026 03:37:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Jan Eris Saluldo
-- Create date: 2026-01-29
-- Description: Get Employees
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[usp_GetEmployees]
AS
BEGIN
    SET NOCOUNT ON;

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
        DeletedAt,
        CreatedAt
    FROM tbl_Employee
    WHERE DeletedAt IS NULL;

END
GO

