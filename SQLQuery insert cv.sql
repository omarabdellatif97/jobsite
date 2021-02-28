USE [MVC]
GO

INSERT INTO [dbo].[CV]
           ([Title]
           ,[Content]
           ,[Extension])
      SELECT 'Moamen CV' AS [Title], 
      '.pdf' AS [Extension], 
      * FROM OPENROWSET(BULK N'C:\tempcv\Moamen CV.pdf', SINGLE_BLOB) AS Document;
GO


