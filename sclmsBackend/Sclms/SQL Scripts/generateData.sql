USE [OperationsDatabase]
GO
SET IDENTITY_INSERT [dbo].[Events] ON 
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (1, N'Stuck Pipe', 50, 150, N'rgba(255,0,0)')
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (2, N'Mud Loss', 100, 200, N'rgba(128, 128, 128)')
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (3, N'Circulation Loss', 150, 200, N'rgba(0,0,0)')
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (4, N'Stuck Pipe', 400, 450, N'rgba(255,0,0)')
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (5, N'Mud loss', 120, 500, N'rgba(128, 128, 128)')
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (17, N'Circulation Loss', 500, 600, N'rgba(0,0,0)')
GO
INSERT [dbo].[Events] ([ID], [EventTitle], [Start], [End], [BackgroundColor]) VALUES (18, N'Circulation Loss', 550, 700, N'rgba(0,0,0)')
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
