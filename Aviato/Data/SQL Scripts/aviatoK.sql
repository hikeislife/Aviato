INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'266685aa-48eb-48da-9f2a-6c5f1fdedc14', N'Mehaničar', N'ApplicationRole')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'2bd9341e-6dad-47e9-adc4-f9e46f28b76a', N'SuperUser', N'ApplicationRole')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'530508fc-e468-4e52-8bcb-039e6451f30f', N'Pilot', N'ApplicationRole')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'637e8dde-06db-43b4-9f76-c966bd205fce', N'Admin', N'ApplicationRole')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'ba340c98-92ad-4d87-b9f2-6743c4494477', N'Stjuard', N'ApplicationRole')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'073db073-62a5-49d0-9e4c-ce44ed5a5a3b', N'637e8dde-06db-43b4-9f76-c966bd205fce')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'073db073-62a5-49d0-9e4c-ce44ed5a5a3b', N'kat.niki@gmail.com', 0, N'AOvl0sBSeJjblka0/fL8eCj494AiFZWLOl3PhMkQHbuR0pEwaz5BSTGV285eIjHS/w==', N'36d35fd7-05e5-49f4-8e20-0ece83fd46c7', NULL, 0, 0, NULL, 1, 0, N'kat.niki@gmail.com')
SET IDENTITY_INSERT [dbo].[Avion] ON
INSERT INTO [dbo].[Avion] ([AvionId], [GodinaProizvodnje], [ServisniStatus], [TipAviona], [SifraAviona]) VALUES (1, 2017, 0, 1, N'A380-1')
SET IDENTITY_INSERT [dbo].[Avion] OFF
SET IDENTITY_INSERT [dbo].[Destinacija] ON
INSERT INTO [dbo].[Destinacija] ([DestinacijaId], [Naziv], [TrajanjeLeta], [Jezik]) VALUES (1, N'Split', 1, 5)
SET IDENTITY_INSERT [dbo].[Destinacija] OFF
SET IDENTITY_INSERT [dbo].[Jezik] ON
INSERT INTO [dbo].[Jezik] ([JezikId], [Jezik]) VALUES (1, N'Engleski')
INSERT INTO [dbo].[Jezik] ([JezikId], [Jezik]) VALUES (2, N'Kineski')
INSERT INTO [dbo].[Jezik] ([JezikId], [Jezik]) VALUES (3, N'Španski')
INSERT INTO [dbo].[Jezik] ([JezikId], [Jezik]) VALUES (4, N'Arapski')
INSERT INTO [dbo].[Jezik] ([JezikId], [Jezik]) VALUES (5, N'Srpski')
INSERT INTO [dbo].[Jezik] ([JezikId], [Jezik]) VALUES (6, N'Ruski')
SET IDENTITY_INSERT [dbo].[Jezik] OFF
SET IDENTITY_INSERT [dbo].[Tip] ON
INSERT INTO [dbo].[Tip] ([TipId], [NazivTipa]) VALUES (1, N'Airbus A380')
INSERT INTO [dbo].[Tip] ([TipId], [NazivTipa]) VALUES (2, N' Boeing 707')
INSERT INTO [dbo].[Tip] ([TipId], [NazivTipa]) VALUES (3, N'Airbus A320')
INSERT INTO [dbo].[Tip] ([TipId], [NazivTipa]) VALUES (4, N'Boeing 727')
INSERT INTO [dbo].[Tip] ([TipId], [NazivTipa]) VALUES (5, N'Boeing 767')
SET IDENTITY_INSERT [dbo].[Tip] OFF
SET IDENTITY_INSERT [dbo].[Zaposleni] ON
INSERT INTO [dbo].[Zaposleni] ([ZaposleniId], [JMBG], [Ime], [Prezime], [GodinaRodjenja], [IdentityId]) VALUES (1, N'1411979787816', N'Katarina', N'Nikić', 1979, N'073db073-62a5-49d0-9e4c-ce44ed5a5a3b')
SET IDENTITY_INSERT [dbo].[Zaposleni] OFF