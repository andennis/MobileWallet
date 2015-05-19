IF NOT EXISTS(SELECT * FROM pm.[User] WHERE UserName = 'admin')
INSERT INTO pm.[User] ( UserName, FirstName, [Password], [Version], CreatedDate, UpdatedDate)
VALUES  ('admin', 'admin', 'dd94709528bb1c83d08f3088d4043f4742891f4f', 1, GETDATE(), GETDATE())