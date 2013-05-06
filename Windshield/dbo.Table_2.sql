CREATE TABLE [dbo].[GameRatings]
(
	[idGame] INT NOT NULL , 
    [idUser] INT NOT NULL, 
    [rating] INT NOT NULL, 
    PRIMARY KEY ([idGame],[idUser]), 
    CONSTRAINT [FK_GameRatings_ToTable] FOREIGN KEY ([idGame]) REFERENCES [Games]([id]), 
    CONSTRAINT [FK_GameRatings_ToTable_1] FOREIGN KEY ([idUser]) REFERENCES [Users]([id])

)
