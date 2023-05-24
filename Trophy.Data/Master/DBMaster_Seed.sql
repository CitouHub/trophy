DELETE FROM PlayerResult
DELETE FROM Player
DELETE FROM Game

DBCC CHECKIDENT ('dbo.PlayerResult', RESEED, 0)
DBCC CHECKIDENT ('dbo.Player', RESEED, 0)
DBCC CHECKIDENT ('dbo.Game', RESEED, 0)

DECLARE @Players INT = 6
DECLARE @Games INT = 1000

DECLARE @N TABLE(I INT NOT NULL, R INT NOT NULL)
INSERT INTO @N
SELECT 1 + (ones.n + 10*tens.n + 100*hundreds.n + 1000*thousands.n), 
	ROUND(RAND(CONVERT(INT, CAST(NEWID() AS VARBINARY)))*2000, 0)
FROM (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) ones(n),
        (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) tens(n),
        (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) hundreds(n),
        (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) thousands(n)
ORDER BY 1

INSERT INTO Player(Name) 
SELECT 'Player ' + CAST(N.I AS NVARCHAR(10)) 
FROM @N AS N WHERE N.I <= @Players

INSERT INTO Game(MatchDate, Location) 
SELECT DATEADD(MINUTE, N.R, DATEADD(DAY, N.R, DATEADD(DAY, -2000, GETUTCDATE()))), 'Sample' 
FROM @N AS N WHERE N.I <= @Games

DECLARE @Matches TABLE(GameId INT NOT NULL, P1 INT NOT NULL, P2 INT NOT NULL, P1S INT NOT NULL, P2S INT NOT NULL)
INSERT INTO @Matches SELECT Game.Id, 
	CAST(RIGHT(LEFT(CAST(CONVERT(INT, CAST(MatchDate AS VARBINARY)) AS NVARCHAR(20)), 5), 1) AS INT) + 1,
	CAST(RIGHT(LEFT(CAST(CONVERT(INT, CAST(MatchDate AS VARBINARY)) AS NVARCHAR(20)), 6), 1) AS INT) + 1,
	CAST(RIGHT(LEFT(CAST(CONVERT(INT, CAST(MatchDate AS VARBINARY)) AS NVARCHAR(20)), 7), 1) AS INT),
	CAST(RIGHT(LEFT(CAST(CONVERT(INT, CAST(MatchDate AS VARBINARY)) AS NVARCHAR(20)), 8), 1) AS INT)
FROM Game

INSERT INTO PlayerResult(GameId, PlayerId, Score, Win)
SELECT M.GameId, (M.P1 % @Players) + 1, (M.P1S % @Players) + 1, CASE WHEN M.P1S > M.P2S THEN 1 ELSE 0 END
FROM @Matches AS M

INSERT INTO PlayerResult(GameId, PlayerId, Score, Win)
SELECT M.GameId, (M.P2 % @Players) + 1, (M.P2S % @Players) + 1, CASE WHEN M.P2S > M.P1S THEN 1 ELSE 0 END
FROM @Matches AS M

DELETE Game WHERE (SELECT COUNT(DISTINCT PlayerId) FROM PlayerResult WHERE GameId = Game.Id) = 1
DELETE Game WHERE (SELECT COUNT(*) FROM PlayerResult WHERE GameId = Game.Id AND Win = 1) <> 1