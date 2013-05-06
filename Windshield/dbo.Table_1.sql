CREATE TABLE [dbo].[UserRatings]
(
	[idRater] INT NOT NULL , 
    [idUser] INT NOT NULL, 
    [date] DATETIME NOT NULL, 
    [positive] INT NOT NULL , 
    [review] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY ([idRater],[idUser]), 
    CONSTRAINT [FK_UserRatings_ToTable] FOREIGN KEY ([idRater]) REFERENCES [Users]([id]), 
    CONSTRAINT [FK_UserRatings_ToTable_1] FOREIGN KEY ([idUser]) REFERENCES [Users]([id]), 
    CONSTRAINT [CK_UserRatings_Column] CHECK (idRater != idUser), 

 )


    