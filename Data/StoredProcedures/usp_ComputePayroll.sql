USE [PayrollDb]
GO

/****** Object:  StoredProcedure [dbo].[usp_ComputePayroll]    Script Date: 1/30/2026 03:36:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jan Eris Saludo
-- Create date: 2026-01-29
-- Description:	Compute Payroll
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[usp_ComputePayroll]
    @EmployeeId UNIQUEIDENTIFIER,
    @StartingDate DATE,
    @EndingDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    SET DATEFIRST 1; 

    DECLARE 
        @DailyRate DECIMAL(18,2),
        @BirthMonth INT,
        @BirthDay INT,
        @WorkingDays VARCHAR(10),
        @TotalPay DECIMAL(18,2) = 0,
        @CurrentDate DATE;

    SELECT 
        @DailyRate = DailyRate,
        @BirthMonth = MONTH(DateOfBirth),
        @BirthDay = DAY(DateOfBirth),
        @WorkingDays = WorkingDays
    FROM tbl_Employee
    WHERE Id = @EmployeeId
      AND DeletedAt IS NULL;

   
    IF @DailyRate IS NULL
    BEGIN
        RAISERROR('Employee not found.', 16, 1);
        RETURN;
    END

 
    IF @StartingDate > @EndingDate
    BEGIN
        RAISERROR('StartingDate cannot be later than EndingDate.', 16, 1);
        RETURN;
    END

    SET @CurrentDate = @StartingDate;

    WHILE @CurrentDate <= @EndingDate
    BEGIN
     
        IF MONTH(@CurrentDate) = @BirthMonth
           AND DAY(@CurrentDate) = @BirthDay
        BEGIN
            SET @TotalPay += @DailyRate;
        END
        
        IF (
            (@WorkingDays = 1 AND DATEPART(WEEKDAY, @CurrentDate) IN (1,3,5))
            OR
            (@WorkingDays = 2 AND DATEPART(WEEKDAY, @CurrentDate) IN (2,4,6))
        )
        BEGIN
            SET @TotalPay += (@DailyRate * 2);
        END

        SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
    END

    
    SELECT @TotalPay AS TakeHomePay;
END
GO

