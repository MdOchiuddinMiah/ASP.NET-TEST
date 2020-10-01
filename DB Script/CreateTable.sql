CREATE DATABASE AppSurvey;
CREATE TABLE Users (
    Id int not null IDENTITY(1,1),
    [Name] nvarchar(255) NOT NULL,
    [Token] nvarchar(100) NOT NULL,
	CONSTRAINT PK_Users PRIMARY KEY (Id)
);

CREATE TABLE [dbo].[Post](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[PostTitle] nvarchar(100) NOT NULL,
	[PostedBy] [int] NOT NULL,
	[PostedTime] [datetime] NOT NULL,
	CONSTRAINT PK_Post PRIMARY KEY (Id),
	CONSTRAINT FK_PostUser FOREIGN KEY (PostedBy)
    REFERENCES Users(Id)
);

CREATE TABLE [dbo].[Comment](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[PostId] int NOT NULL,
	[CommentTitle] nvarchar(100) NOT NULL,
	[CommentedBy] [int] NOT NULL,
	[CommentedTime] [datetime] NOT NULL,
	CONSTRAINT PK_Comment PRIMARY KEY (Id),
	CONSTRAINT FK_CommentUser FOREIGN KEY (CommentedBy)
    REFERENCES Users(Id),
	CONSTRAINT FK_CommentPost FOREIGN KEY (PostId)
    REFERENCES Post(Id)
);

CREATE TABLE [dbo].[UserFeedback](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[CommentId] int NOT NULL,
	[UserId] [int] NOT NULL,
	[IsLike] [bit] NOT NULL,
	[IsDislike] [bit] NOT NULL,
	CONSTRAINT PK_UserFeedback PRIMARY KEY (Id),
	CONSTRAINT FK_UserFeedbackComment FOREIGN KEY (CommentId)
    REFERENCES Comment(Id),
	CONSTRAINT FK_UserFeedbackUser FOREIGN KEY (UserId)
    REFERENCES Users(Id)
);

insert into Users values ('Admin','@21');
insert into Users values ('User1','@21');
insert into Users values ('User2','@21');
insert into Users values ('User3','@21');
insert into Users values ('User4','@21');
insert into Users values ('User5','@21');

insert into Post values ('Post1',1,'12-12-2010');
insert into Post values ('Post2',1,'12-12-2010');
insert into Post values ('Post3',1,'12-12-2010');
insert into Post values ('Post4',1,'12-12-2010');

insert into Comment values (1,'Comment1',2,'12-12-2010');
insert into Comment values (1,'Comment2',3,'12-12-2010');
insert into Comment values (1,'Comment3',4,'12-12-2010');
insert into Comment values (2,'Comment4',2,'12-12-2010');
insert into Comment values (2,'Comment5',3,'12-12-2010');
insert into Comment values (2,'Comment5',4,'12-12-2010');
insert into Comment values (3,'Comment6',2,'12-12-2010');
insert into Comment values (3,'Comment7',3,'12-12-2010');
insert into Comment values (3,'Comment8',4,'12-12-2010');

insert into UserFeedback values (1,2,'1','0');
insert into UserFeedback values (2,2,'1','0');
insert into UserFeedback values (3,2,'0','1');
insert into UserFeedback values (4,2,'0','1');
insert into UserFeedback values (5,2,'1','0');
