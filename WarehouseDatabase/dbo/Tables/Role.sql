﻿CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]