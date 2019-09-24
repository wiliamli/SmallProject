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
 