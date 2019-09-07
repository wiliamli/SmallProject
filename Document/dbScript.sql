if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysUser')
            and   type = 'U')
   drop table dbo.SysUser
go
/*==============================================================*/
/* Table: SysUser                                               */
/*==============================================================*/
create table dbo.SysUser (
   Id                   int                  identity(1, 1) primary key,
   Name                 nvarchar(20)         collate Chinese_PRC_CI_AS not null,
   Password             varchar(128)         collate Chinese_PRC_CI_AS not null,
   Status               bit                  not null DEFAULT 1,
   Phone                varchar(20)          collate Chinese_PRC_CI_AS null,
   Mobile               BIGINT               null,
   Address              nvarchar(512)        collate Chinese_PRC_CI_AS null,
   Email                varchar(100)         collate Chinese_PRC_CI_AS null,
   QQ                   BIGINT               null,
   WeChat               varchar(50)          collate Chinese_PRC_CI_AS null,
   Sex                  nchar(2)             collate Chinese_PRC_CI_AS not null,
   LastLoginTime        datetime             null,
   CreateTime           datetime             DEFAULT GETDATE(),
   CreateId             int                  not null,
   LastModifyTime       datetime             DEFAULT GETDATE(),
   LastModifyId         int                  null
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysMenu')
            and   type = 'U')
   drop table dbo.SysMenu
go
/*==============================================================*/
/* Table: SysMenu                                               */
/*==============================================================*/
create table dbo.SysMenu (
   Id                   int                  identity(1, 1) primary key,
   ParentId             int                  not null,
   Text                 nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   Url                  varchar(500)         collate Chinese_PRC_CI_AS null,
   MenuLevel            tinyint              not null default (1),
   MenuType             tinyint              not null default (1),
   MenuIcon             nvarchar(20)         collate Chinese_PRC_CI_AS null,
   Description          varchar(100)         collate Chinese_PRC_CI_AS null,
   SourcePath           varchar(1000)        collate Chinese_PRC_CI_AS null,
   Sort                 int                  not null constraint DF__SysMenu__Sort__1FCDBCEB default (0),
   Status               bit                  not null default (1),
   CreateTime           datetime             not null DEFAULT GETDATE(),
   CreatorId            int                  not null,
   LastModifyTime       datetime             null DEFAULT GETDATE(),
   LastModifierId       int                  null
)
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysMenuOperation')
            and   type = 'U')
   drop table dbo.SysMenuOperation
go
/*==============================================================*/
/* Table: SysMenuOperation                                      */
/*==============================================================*/
create table dbo.SysMenuOperation (
   Id                   int                  identity(1, 1) primary key,
   OperateName          nvarchar(50)         not null,
   OperateType          varchar(50)          null,
   Status               bit                  not null default (1),
   CreateTime           datetime             not null DEFAULT GETDATE(),
   CreateId             int                  not null,
   LastModifyTime       datetime             null DEFAULT GETDATE(),
   LastModifierId       int                  null
)
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysUserMenuOperationMapping')
            and   type = 'U')
   drop table dbo.SysUserMenuOperationMapping
go

/*==============================================================*/
/* Table: SysUserMenuOperationMapping                           */
/*==============================================================*/
create table dbo.SysUserMenuOperationMapping (
   Id                   int                identity(1,1) PRIMARY KEY,
   SysOperationId       int                  not null,
   SysMenuId            int                  not null,
   SysUserId            int                  not null
)
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysRoleMenuOperationMapping')
            and   type = 'U')
   drop table dbo.SysRoleMenuOperationMapping
go
/*==============================================================*/
/* Table: SysRoleMenuOperationMapping                           */
/*==============================================================*/
create table dbo.SysRoleMenuOperationMapping (
   Id                   int                  identity(1,1) PRIMARY KEY,
   SysOperationId       int                  not null,
   SysMenuId            int                  not null,
   SysRoleId            int                  not null
)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysUserRoleMapping')
            and   type = 'U')
   drop table dbo.SysUserRoleMapping
go
/*==============================================================*/
/* Table: SysUserRoleMapping                                    */
/*==============================================================*/
create table dbo.SysUserRoleMapping (
   Id                   int                  IDENTITY(1,1) PRIMARY KEY,
   SysUserId            int                  not null,
   SysRoleId            int                  not null
)
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysUserMenuMapping')
            and   type = 'U')
   drop table dbo.SysUserMenuMapping
go
/*==============================================================*/
/* Table: SysUserMenuMapping                                    */
/*==============================================================*/
create table dbo.SysUserMenuMapping (
   Id                   int                  IDENTITY(1,1) PRIMARY KEY,
   SysUserId            int                  not null,
   SysMenuId            int                  not null
)
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SysRole')
            and   type = 'U')
   drop table dbo.SysRole
go

/*==============================================================*/
/* Table: SysRole                                               */
/*==============================================================*/
create table dbo.SysRole (
   Id                   int                  identity(1, 1) PRIMARY KEY,
   Text                 nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   Status               bit                  not null default (1),
   CreateTime           datetime             not null DEFAULT GETDATE(),
   CreateId             int                  not null,
   LastModifyTime       datetime             null DEFAULT GETDATE(),
   LastModifierId       int                  null
)
go



/*==============================================================*/
/* 论坛START                                                    */
/*==============================================================*/

if exists (select 1
            from  sysobjects
           where  id = object_id('ForumRoleChannel')
            and   type = 'U')
   drop table ForumRoleChannel
go
/*==============================================================*/
/* Table: ForumRoleChannel                                     */
/*==============================================================*/
create table ForumRoleChannel (
   Id                   int  IDENTITY(1,1) PRIMARY KEY,
   SysRoleId            int                  not null,
   ChannelId            int                  not null,
   CreatedId            int          null,
   CreatedDate          datetime     null DEFAULT GETDATE(),
   ModifiedId           int          null,
   ModifiedDate         datetime     null DEFAULT GETDATE()
)
go


IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id( 'ForumChannel' ) AND type = 'U' ) DROP TABLE ForumChannel 
GO
/*==============================================================*/
/* Table: ForumChannel                                         */
/*==============================================================*/
	CREATE TABLE ForumChannel (
	Id INT IDENTITY ( 1, 1 ) PRIMARY KEY,
	Name VARCHAR ( 16 ) NOT NULL,
	Status bit not null default (1),
	CreatedId INT NULL,
	CreatedDate datetime NULL DEFAULT GETDATE(),
	ModifiedId INT NULL,
	ModifiedDate datetime NULL DEFAULT GETDATE() 
	) 
GO

IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id( 'ForumAttachment' ) AND type = 'U' ) DROP TABLE ForumAttachment 
GO
/*==============================================================*/
/* Table: ForumAttachment                                      */
/*==============================================================*/
	CREATE TABLE ForumAttachment (
	Id INT IDENTITY ( 1, 1 ) PRIMARY KEY,
	TopicId INT NOT NULL,
	FileName VARCHAR ( 128 ) NOT NULL,
	FilePath VARCHAR ( 256 ) NOT NULL,
	FileExtension VARCHAR ( 8 ) NOT NULL,
	Status  bit  not null default (1),
	CreatedId INT NULL,
	CreatedDate datetime NULL DEFAULT GETDATE(),
	ModifiedId INT NULL,
	ModifiedDate datetime NULL DEFAULT GETDATE() 
	) 
GO

IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id( 'ForumTopic' ) AND type = 'U' ) DROP TABLE ForumTopic 
GO
/*==============================================================*/
/* Table: ForumTopic                                           */
/*==============================================================*/
	CREATE TABLE ForumTopic (
	Id INT IDENTITY ( 1, 1 ) PRIMARY KEY,
	ChannelId INT NOT NULL,
	For_Id INT NULL,
	Name VARCHAR ( 256 ) NOT NULL,
	Content text NULL,
	PV INT NOT NULL,
	Conssensus INT NOT NULL,
	Oppose INT NOT NULL,
	Status BIT NOT NULL DEFAULT ( 1 ),
	CreatedId INT NULL,
	CreatedDate datetime NULL DEFAULT GETDATE(),
	ModifiedId INT NULL,
	ModifiedDate datetime NULL DEFAULT GETDATE() 
	) 
GO


IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id( 'ForumConcern' ) AND type = 'U' ) DROP TABLE ForumConcern 
GO
/*==============================================================*/
/* Table: ForumConcern                                         */
/*==============================================================*/
	CREATE TABLE ForumConcern (
	Id INT IDENTITY ( 1, 1 ) PRIMARY KEY,
	ConcernUserId INT NOT NULL,
	AttentionUserId INT NOT NULL,
	CreatedId INT NULL,
	CreatedDate datetime NULL DEFAULT GETDATE(),
	ModifiedId INT NULL,
	ModifiedDate datetime NULL DEFAULT GETDATE() 
	) 
GO


IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id( 'ForumPersonal' ) AND type = 'U' ) DROP TABLE ForumPersonal 
GO
/*==============================================================*/
/* Table: ForumPersonal                                        */
/*==============================================================*/
	CREATE TABLE ForumPersonal (
	Id INT IDENTITY ( 1, 1 ) PRIMARY KEY,
	Username VARCHAR ( 128 ) NOT NULL,
	Password VARCHAR ( 128 ) NOT NULL,
	CreatedId INT NULL,
	CreatedDate datetime NULL DEFAULT GETDATE(),
	ModifiedId INT NULL,
	ModifiedDate datetime NULL DEFAULT GETDATE() 
	) 
GO

IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id ( 'ForumCheckIn' ) AND type = 'U' ) DROP TABLE ForumCheckIn
/*==============================================================*/
/* Table: ForumCheckIn                                         */
/*==============================================================*/
CREATE TABLE ForumCheckIn ( 
Id INT IDENTITY ( 1, 1 ) PRIMARY KEY, 
UserId INT NOT NULL, 
CheckDate datetime NOT NULL DEFAULT GETDATE()
) 
GO


IF EXISTS ( SELECT 1 FROM sysobjects WHERE id = object_id( 'ForumInvitation' ) AND type = 'U' ) DROP TABLE ForumInvitation 
GO
/*==============================================================*/
/* Table: ForumInvitation                                      */
/*==============================================================*/
	CREATE TABLE ForumInvitation (
	Id INT NOT NULL PRIMARY KEY IDENTITY ( 1, 1 ),
	TopicId INT NOT NULL,
	ParentId INT NOT NULL,
	Content text NULL,
	Accept BIT NOT NULL,
	Conssensus INT NOT NULL,
	Oppose INT NOT NULL,
	CreatedId INT NULL,
	CreatedDate datetime NULL DEFAULT GETDATE(),
	ModifiedId INT NULL,
	ModifiedDate datetime NULL DEFAULT GETDATE() 
	) 
GO