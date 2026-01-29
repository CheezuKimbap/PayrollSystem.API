USE [PayrollDb]
GO

/****** Object:  StoredProcedure [dbo].[usp_DeleteEmployee]    Script Date: 1/30/2026 03:36:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Jan Eris Saluldo
-- Create date: 2026-01-29
-- Description: Soft delete employee by Id
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[usp_DeleteEmployee]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Validation
    IF @Id IS NULL
    BEGIN
        RAISERROR('Id must be provided.', 16, 1);
        RETURN;
    END

    UPDATE tbl_Employee
    SET
        DeletedAt  = SYSUTCDATETIME(),
        ModifiedAt = SYSUTCDATETIME()
    WHERE Id = @Id
      AND DeletedAt IS NULL;
END
GO

