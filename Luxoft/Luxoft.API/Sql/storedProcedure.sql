USE [Luxoft]
GO

/****** Object:  StoredProcedure [dbo].[InsertUpdateAssetsFromFile]    Script Date: 7/15/2021 6:10:14 PM ******/
DROP PROCEDURE [dbo].[InsertUpdateAssetsFromFile]
GO

/****** Object:  StoredProcedure [dbo].[InsertUpdateAssetsFromFile]    Script Date: 7/15/2021 6:10:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[InsertUpdateAssetsFromFile]
	-- Add the parameters for the stored procedure here
	@AssetInputs [dbo].[AssetInput] READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	-- inserting new columns into the table
	INSERT INTO Assets (AssetId, IsFixIncome, IsConvertible, IsSwap, IsCash, IsFuture, TimeStamp)
	SELECT AI.AssetId, AI.IsFixIncome, AI.IsConvertible, AI.IsSwap, AI.IsCash, AI.IsFuture, AI.TimeStamp FROM @AssetInputs AI LEFT JOIN Assets A ON AI.AssetId = A.AssetId WHERE A.AssetId IS NULL;
	

	-- updating the existing asset Ids
	UPDATE A
		SET A.IsFixIncome = Ai.IsFixIncome,
		A.IsCash=Ai.IsCash,
		A.IsConvertible=Ai.IsConvertible,
		A.IsSwap=Ai.IsSwap,
		A.IsFuture = Ai.IsFuture,
		A.TimeStamp = Ai.TimeStamp
	from Assets A
		INNER JOIN @AssetInputs Ai
		ON A.AssetId=Ai.AssetId
		Where Ai.TimeStamp > A.TimeStamp;

END
GO


