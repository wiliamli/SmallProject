use RuanmouData
go

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
	ModifiedDate datetime NULL DEFAULT GETDATE(),
	CreatedBy varchar(32) NULL,
	ModifiedBy varchar(32) NULL
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
	ModifiedDate datetime NULL DEFAULT GETDATE(),
	CreatedBy varchar(32) NULL,
	ModifiedBy varchar(32) NULL
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
	ModifiedDate datetime NULL DEFAULT GETDATE(),
	CreatedBy varchar(32) NULL,
	ModifiedBy varchar(32) NULL
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
	ModifiedDate datetime NULL DEFAULT GETDATE(),
	CreatedBy varchar(32) NULL,
	ModifiedBy varchar(32) NULL
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
	ModifiedDate datetime NULL DEFAULT GETDATE(),
	CreatedBy varchar(32) NULL,
	ModifiedBy varchar(32) NULL
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
	ModifiedDate datetime NULL DEFAULT GETDATE(),
	CreatedBy varchar(32) NULL,
	ModifiedBy varchar(32) NULL
	) 
GO



/*  功能： 软媒官网客户端Portal 
	表名前缀： Sys 
表： 
	课程分类(SysCourseCategory)：软媒课程分类
	课程(SysCourse)：软媒课程 
*/ 
-- 课程(SysCourse)
IF EXISTS(SELECT * FROM sysobjects WHERE name='SysCourse')
	DROP TABLE SysCourse
CREATE TABLE SysCourse
(
	Id INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	CourseCategoryID INT NOT NULL DEFAULT(0),							--课程分类
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--课程名称
	Price DECIMAL(18,2) NOT NULL DEFAULT(0),							--价格
	Picture VARCHAR(50) NOT NULL DEFAULT(''),							--缩略图[一张]：保存图片路径值
	TencentUrl VARCHAR(255) NOT NULL DEFAULT(''),						--腾讯详情地址：跳转到腾讯课堂的地址 
	CourseTime NVARCHAR(100) NOT NULL DEFAULT(''),						--课程上课时间
	Lecturer NVARCHAR(50) NOT NULL DEFAULT(''),							--主讲老师
	Brief NVARCHAR(1000) NOT NULL DEFAULT(''),							--课程概述
	Details NTEXT NOT NULL DEFAULT(''),									--详细描述 
	IsHot INT NOT NULL DEFAULT(0),										--热门推荐
	CourseTags nvarchar(500) not null default(''),						--课程标签(用'|'分隔)：如 促销|免费|推荐
	BrowseCount INT NOT NULL DEFAULT(0),								--浏览量、点击量
	CommentsCount INT NOT NULL DEFAULT(0),								--累计评价 
	Sort INT NOT NULL DEFAULT(0),										--排序
	Remarks NVARCHAR(255) NOT NULL DEFAULT(''),							--备注
	[Status] [bit] NOT NULL,											--状态
	[CreateTime] [datetime] NOT NULL,									--创建日期
	[CreateId] [int] NOT NULL,											--创建人ID
	[LastModifyTime] [datetime] NULL,									--修改日期
	[LastModifierId] [int] NULL											--修改人ID
)
GO

--课程分类(SysCourseCategory)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='SysCourseCategory')
	DROP TABLE SysCourseCategory
CREATE TABLE SysCourseCategory
(
	Id INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	Name NVARCHAR(50) NOT NULL DEFAULT(''),								--分类名称
	CateDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--分类描述
	ParentID INT NOT NULL DEFAULT(0),									--父级分类ID
	Icon VARCHAR(255) NOT NULL DEFAULT(''),								--分类图标  
	IsVisible INT NOT NULL DEFAULT(0),									--前台可见：可见(1)，不可见(0)
	CategoryPath VARCHAR(500) NOT NULL DEFAULT(''),						--分类路径(存放ID路径)
	IsLastNode INT NOT NULL DEFAULT(0),									--是最后节点
	ChildCount INT NOT NULL DEFAULT(0),									--子分类数量
	CourseCount INT NOT NULL DEFAULT(0),								--课程数量：暂不启用
	Remarks NVARCHAR(255) NOT NULL DEFAULT(''),							--备注 
	Sort INT NOT NULL DEFAULT(0),										--排序
	[Status] [bit] NOT NULL,											--状态
	[CreateTime] [datetime] NOT NULL,									--创建日期
	[CreateId] [int] NOT NULL,											--创建人ID
	[LastModifyTime] [datetime] NULL,									--修改日期
	[LastModifierId] [int] NULL											--修改人ID
)
GO