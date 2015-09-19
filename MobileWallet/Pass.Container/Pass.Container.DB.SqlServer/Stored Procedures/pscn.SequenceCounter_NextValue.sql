CREATE PROCEDURE [pscn].[SequenceCounter_NextValue]
	@Name VARCHAR(50),
    @NextValue INT OUTPUT
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM pscn.SequenceCounter WHERE Name = @Name)
    BEGIN  
        SET @NextValue = 1
        INSERT INTO pscn.SequenceCounter (Name, Counter) VALUES (@Name, @NextValue)
        RETURN
    END
    
    DECLARE @NextValueTbl TABLE(NextValue INT)
    UPDATE pscn.SequenceCounter SET [Counter] += 1 
    OUTPUT INSERTED.[Counter] INTO @NextValueTbl
    WHERE Name = @Name

    SET @NextValue = (SELECT TOP 1 NextValue FROM @NextValueTbl)
END
