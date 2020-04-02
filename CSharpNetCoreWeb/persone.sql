USE [mp_test]
GO

/****** Object:  Table [dbo].[Persone]    Script Date: 02/04/2020 09:23:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Persone](
	[PersonaId] [int] IDENTITY(1,1) NOT NULL,
	[PersonaEmail] [nvarchar](100) NOT NULL,
	[PersonaAttivo] [bit] NOT NULL
) ON [PRIMARY]
GO

