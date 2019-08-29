/*  功能： 软媒官网客户端Portal 
	表名前缀： Pt
	设计说明： 
		1 普通表的主键固定使用Kid, int 自增型；映射表主键 用Guid
		2 所有字段都带有默认值,免去NULL的查询麻烦。
		3 日期类型默认值 '1900-01-01',程序判断遇到'1900-01-01'时，认为没有设置过
表：
	会员(PtMember)：VIP学员
	课程分类(PtCourseCategory)：软媒课程分类
	课程(PtCourse)：软媒课程
	班级(PtClass)：一个课程对应多个不同学期的班级
	学员班级映射(PtMapMemberClass)：一个学员允许报名加入多个不同分类的班级
	网站内容(PtWebContent)：包含固定页面内容维护和 banner维护等等
	下载管理(PtDownload)：开发工具下载
	文章分类(PtNewsCategory)
	文章(PtNews)
	积分明细(PtMemberIntegrals)：学员积分流水
	积分兑换记录(PtIntegralExchange)
	网站系统参数(PtSysParams)
	会员文章权限(PtMapMemberNewsCategory)：学员权限控制，一个学员允许查看的文章权限。设置一个类别是"资料下载",配置学员允许看这个类别下的哪个文章
	省市区表，见文件：ProvinceAndCity.sql，另外读取都走xml固定的物理文件Address.xml，减少读取数据库
*/

--会员文章权限(PtMapMemberNews): 暂只针对 "资料下载" 这个分类使用
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMapMemberNews')
	DROP TABLE PtMapMemberNews
CREATE TABLE PtMapMemberNews
(
	Ukid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT(NEWID()),					--Guid主键
	MemberID INT NOT NULL DEFAULT(0),									--学员ID
	NewsID INT NOT NULL DEFAULT(0),										--文章ID
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE())						--创建日期
)
GO

-- 网站系统参数(PtSysParams)
IF EXISTS(SELECT * FROM sysobjects WHERE name='PtSysParams')
	DROP TABLE PtSysParams
CREATE TABLE PtSysParams
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键 
	ParamsName NVARCHAR(50) NOT NULL DEFAULT(''),						--参数名称，必须唯一
	ParamsValue NVARCHAR(255) NOT NULL DEFAULT(''),						--参数值(数字或文字)
	DateValue DATETIME NOT NULL DEFAULT('1900-01-01'),					--参数值(日期类型值) 
	IsVisible INT NOT NULL DEFAULT(0),									--可见：可见(1)，不可见(0)
	TabCode VARCHAR(50) NOT NULL DEFAULT(''),							--选项卡编号：后台设置的选项卡编号，管理后台使用
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--修改日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注
)
GO

-- 积分兑换记录(PtIntegralExchange)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtIntegralExchange')
	DROP TABLE PtIntegralExchange
CREATE TABLE PtIntegralExchange
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键 
	SN VARCHAR(50) NOT NULL DEFAULT(''),								--兑换记录流水号
	Category VARCHAR(50) NOT NULL DEFAULT(''),							--兑换分类，咱不启用，默认只有一种积分兑换
	MemberID INT NOT NULL DEFAULT(0),									--学员ID 
	Amount DECIMAL(18,2) NOT NULL DEFAULT(0),							--消耗积分
	ExchangeItems NVARCHAR(255) NOT NULL DEFAULT(''),					--兑换物品名称
	ExchangeDesc NVARCHAR(500) NOT NULL DEFAULT(''),					--兑换描述
	AuditUserID INT NOT NULL DEFAULT(0),								--审核管理员ID
	AuditDate DATETIME NOT NULL DEFAULT('1900-01-01'),					--审核日期
	AuditStatus INT NOT NULL DEFAULT(0),								--审核状态:0 未审核 -1审核不通过 1审核通过
	AuditResult NVARCHAR(1000) NOT NULL DEFAULT(''),					--审核结果 
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0不启用 -1删除
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注  
)
GO 

--	积分明细(PtMemberIntegrals)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMemberIntegrals')
	DROP TABLE PtMemberIntegrals
CREATE TABLE PtMemberIntegrals
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	SN VARCHAR(50) NOT NULL DEFAULT(''),								--流水号
	PtType VARCHAR(50) NOT NULL DEFAULT(''),							--积分类型 (G普通积分,L锁定积分 等)
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	MemberID INT NOT NULL DEFAULT(0),									--学员ID
	FundCategory VARCHAR(50) NOT NULL DEFAULT(''),						--积分的收支项目，赠送积分，评论积分，签到积分等
	Amount DECIMAL(18,2) NOT NULL DEFAULT(0),							--积分值
	Abstract NVARCHAR(500) NOT NULL DEFAULT(''),						--摘要说明
	RelatedTag NVARCHAR(50) NOT NULL DEFAULT(''),						--关联标签，(关联标签等属性记录积分的来源和去处，保证数据的可追溯性)
	RelatedSN NVARCHAR(50) NOT NULL DEFAULT(''),						--关联流水号(推荐用流水号)
	RelatedID INT NOT NULL DEFAULT(0),									--关联ID(没有编号用ID)
	Balance DECIMAL(18,2) NOT NULL DEFAULT(0),							--余额(最新余额和会员里(账户余额-冻结余额)要一致)
	PtStatus INT NOT NULL DEFAULT(0),									--状态 
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注 (数据理论上不可以修改，只能增加记录，因此无修改时间)
)
GO

-- 文章分类(PtNewsCategory)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtNewsCategory')
	DROP TABLE PtNewsCategory
CREATE TABLE PtNewsCategory
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	PtType VARCHAR(50) NOT NULL DEFAULT(''),							--类型(P新闻(paper) N公告(notice) M站内消息(message))
	Name NVARCHAR(50) NOT NULL DEFAULT(''),								--分类名称
	CateDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--分类描述
	ParentID INT NOT NULL DEFAULT(0),									--父级分类ID
	SeoKeys NVARCHAR(255) NOT NULL DEFAULT(''),							--SEO关键字
	SeoDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--SEO描述
	IsVisible INT NOT NULL DEFAULT(0),									--前台可见：可见(1)，不可见(0)
	Flag VARCHAR(10) NOT NULL DEFAULT(''),								--标识(S:系统对象,不可删除)
	IsLastNode INT NOT NULL DEFAULT(0),									--是最后节点
	ChildCount INT NOT NULL DEFAULT(0),									--子分类数量
	NewsCount INT NOT NULL DEFAULT(0),									--文章数量：暂不启用 
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0不启用 -1删除
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期 
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注 
)
GO 

--文章(PtNews)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtNews')
	DROP TABLE PtNews
CREATE TABLE PtNews
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	CategoryID INT NOT NULL DEFAULT(0),									--文章分类ID
	Title NVARCHAR(255) NOT NULL DEFAULT(''),							--标题
	SubTitle NVARCHAR(255) NOT NULL DEFAULT(''),						--副标题
	Picture VARCHAR(255) NOT NULL DEFAULT(''),							--封面图
	Author NVARCHAR(50) NOT NULL DEFAULT(''),							--作者
	Editor NVARCHAR(50) NOT NULL DEFAULT(''),							--责任编辑
	NewsSource NVARCHAR(255) NOT NULL DEFAULT(''),						--文章来源
	Brief NVARCHAR(500) NOT NULL DEFAULT(''),							--简介
	Content NTEXT NOT NULL DEFAULT(''),									--内容
	SeoKeys NVARCHAR(255) NOT NULL DEFAULT(''),							--SEO关键字
	SeoDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--SEO描述
	Links VARCHAR(255) NOT NULL DEFAULT(''),							--外部链接
	AuditUserID INT NOT NULL DEFAULT(0),								--审核管理员ID
	AuditDate DATETIME NOT NULL DEFAULT('1900-01-01'),					--审核日期
	AuditStatus INT NOT NULL DEFAULT(0),								--审核状态:0 未审核 -1审核不通过 1审核通过
	AuditResult NVARCHAR(1000) NOT NULL DEFAULT(''),					--审核结果 
	BrowseTimes INT NOT  NULL DEFAULT(0),								--浏览量
	IsTop INT NOT NULL DEFAULT(0),										--置顶
	NewsTags nvarchar(500) not null default(''),						--文章标签(用'|'分隔)：如 热门|头条
	PtStatus INT NOT NULL DEFAULT(0),									--状态
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注  
)
GO 

--下载管理(PtDownload)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtDownload')
	DROP TABLE PtDownload
CREATE TABLE PtDownload
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	Category VARCHAR(50) NOT NULL DEFAULT(''),							--分类,如 T:开发工具, V:课件视频, C:课件代码
	PtGroup VARCHAR(50) NOT NULL DEFAULT(''),							--分组。如 开发工具分: 前端，数据库,C# 等等
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--下载文件名称,物理文件名称
	Picture VARCHAR(50) NOT NULL DEFAULT(''),							--缩略图：保存图片名称
	DownloadUrl VARCHAR(255) NOT NULL DEFAULT(''),						--下载地址，百度云地址
	DownloadPwd VARCHAR(50) NOT NULL DEFAULT(''),						--下载密钥，百度云密码 
	FileDesc NVARCHAR(500) NOT NULL DEFAULT(''),						--文件简要描述 
	DownloadTimes INT NOT NULL DEFAULT(0),								--下载次数
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0不显示 -1删除
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildUserID INT NOT NULL DEFAULT(0),								--创建管理员ID
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyUserID INT NOT NULL DEFAULT(0),								--更新管理员ID
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注
)
GO

--网站内容
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtWebContent')
	DROP TABLE PtWebContent
CREATE TABLE PtWebContent
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	PtType VARCHAR(50) NOT NULL DEFAULT(''),							--类型：textarea(编辑器类型)、text(文本类型)、image(图片类型)、background-image(背景图)、html页面
	Category VARCHAR(50) NOT NULL DEFAULT(''),							--分类，如 banner, tag
	ContentLabel VARCHAR(50) NOT NULL DEFAULT(''),						--内容标签：唯一性
	ContentTitle NVARCHAR(50) NOT NULL DEFAULT(''),						--内容标题：后台维护时显示的标题
	ContentDesc NVARCHAR(500) NOT NULL DEFAULT(''),						--内容描述
	ContentMain NTEXT NOT NULL DEFAULT(''),								--详细内容
	ImageName VARCHAR(100) NOT NULL DEFAULT(''),						--图片名称，给图片类型使用
	ImageTitle NVARCHAR(100) NOT NULL DEFAULT(''),						--图片标题，给图片类型使用
	Links VARCHAR(255) NOT NULL DEFAULT(''),							--链接地址
	Tips NVARCHAR(255) NOT NULL DEFAULT(''),							--提示信息,如提示上传图片大小
	Location VARCHAR(100) NOT NULL DEFAULT(''),							--显示位置标签 
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0不显示 -1删除
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注
)
GO

--学员班级映射(PtMapMemberClass)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMapMemberClass')
	DROP TABLE PtMapMemberClass
CREATE TABLE PtMapMemberClass
(
	Ukid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT(NEWID()),					--Guid主键
	MemberID INT NOT NULL DEFAULT(0),									--学员ID
	ClassID INT NOT NULL DEFAULT(0),									--班级ID
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE())						--创建日期
)
GO

--班级(PtClass)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtClass')
	DROP TABLE PtClass
CREATE TABLE PtClass
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	CourseID INT NOT NULL DEFAULT(0),									--课程ID(关联显示课程分类)
	Semester INT NOT NULL DEFAULT(0),									--学期
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--班级名称
	DisplayName NVARCHAR(100) NOT NULL DEFAULT(''),						--班级显示名称
	HeadTeacher NVARCHAR(50) NOT NULL DEFAULT(''),						--班主任
	HeadTeacherQQ VARCHAR(20) NOT NULL DEFAULT(''),						--班主任QQ
	MonitorID INT NOT NULL DEFAULT(0),									--班长ID(关联学员)
	GroupQQ VARCHAR(50) NOT NULL DEFAULT(''),							--群QQ号 
	OpeningDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--开学日期
	GraduationDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--毕业日期
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0冻结 -1删除  
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--修改日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注
)
GO

-- 课程(PtCourse)
IF EXISTS(SELECT * FROM sysobjects WHERE name='PtCourse')
	DROP TABLE PtCourse
CREATE TABLE PtCourse
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	CourseCategoryID INT NOT NULL DEFAULT(0),							--课程分类
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--课程名称
	Price DECIMAL(18,2) NOT NULL DEFAULT(0),							--价格
	Picture VARCHAR(50) NOT NULL DEFAULT(''),							--缩略图[一张]：保存图片路径值
	TencentUrl VARCHAR(255) NOT NULL DEFAULT(''),						--腾讯详情地址：跳转到腾讯课堂的地址 
	CourseTime NVARCHAR(100) NOT NULL DEFAULT(''),						--课程上课时间
	Lecturer NVARCHAR(50) NOT NULL DEFAULT(''),							--主讲老师
	Brief NVARCHAR(1000) NOT NULL DEFAULT(''),							--课程概述
	Details NTEXT NOT NULL DEFAULT(''),									--详细描述 
	IsShelvesOn INT NOT NULL DEFAULT(0),								--是否上架：0否 1是
	ShelvesOnDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--上架日期
	ShelvesOffDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--下架日期 
	AuditUserID INT NOT NULL DEFAULT(0),								--审核管理员ID
	AuditDate DATETIME NOT NULL DEFAULT('1900-01-01'),					--审核日期
	AuditStatus INT NOT NULL DEFAULT(0),								--审核状态:0 未审核 -1审核不通过 1审核通过
	AuditResult NVARCHAR(1000) NOT NULL DEFAULT(''),					--审核结果 
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0冻结 -1删除 
	IsHot INT NOT NULL DEFAULT(0),										--热门推荐
	CourseTags nvarchar(500) not null default(''),						--课程标签(用'|'分隔)：如 促销|免费|推荐
	BrowseCount INT NOT NULL DEFAULT(0),								--浏览量、点击量
	CommentsCount INT NOT NULL DEFAULT(0),								--累计评价 
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--修改日期
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注
)
GO

--课程分类(PtCourseCategory)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtCourseCategory')
	DROP TABLE PtCourseCategory
CREATE TABLE PtCourseCategory
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	Name NVARCHAR(50) NOT NULL DEFAULT(''),								--分类名称
	CateDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--分类描述
	ParentID INT NOT NULL DEFAULT(0),									--父级分类ID
	Icon VARCHAR(255) NOT NULL DEFAULT(''),								--分类图标 
	SeoKeys NVARCHAR(255) NOT NULL DEFAULT(''),							--SEO关键字：暂不启用
	SeoDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--SEO描述：暂不启用
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--排序
	IsVisible INT NOT NULL DEFAULT(0),									--前台可见：可见(1)，不可见(0)
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0冻结 -1删除
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期
	CategoryPath VARCHAR(500) NOT NULL DEFAULT(''),						--分类路径(存放ID路径)
	Flag VARCHAR(10) NOT NULL DEFAULT(''),								--标识(S:系统对象,不可删除)
	IsLastNode INT NOT NULL DEFAULT(0),									--是最后节点
	ChildCount INT NOT NULL DEFAULT(0),									--子分类数量
	CourseCount INT NOT NULL DEFAULT(0),								--课程数量：暂不启用
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--备注 
)
GO

--会员(PtMember)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMember')
	DROP TABLE PtMember
CREATE TABLE PtMember
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--自增主键
	Code VARCHAR(50) NOT NULL DEFAULT(''),								--学员编号(学号)：不同于主键ID
	Account VARCHAR(50) NOT NULL DEFAULT(''),							--学员账号：登录账号
	Pwd VARCHAR(100) NOT NULL DEFAULT(''),								--学员密码：字母、数字组合，6-12位
	NickName NVARCHAR(50) NOT NULL DEFAULT(''),							--昵称
	SpecialTitle NVARCHAR(50) NOT NULL DEFAULT(''),						--特殊称号(老师给的封号)
	Gender VARCHAR(2) NOT NULL DEFAULT('U'),							--性别: 男(M),女(F),未知(U)
	Face VARCHAR(255) NOT NULL DEFAULT(''),								--头像
	LvCode VARCHAR(50) NOT NULL DEFAULT(''),							--会员级别编号：暂不启用。
	RealName NVARCHAR(50) NOT NULL DEFAULT(''),							--姓名
	SocialId VARCHAR(50) NOT NULL DEFAULT(''),							--身份证号
	Email VARCHAR(255) NOT NULL DEFAULT(''),							--电子邮箱
	Mobile VARCHAR(20) NOT NULL DEFAULT(''),							--手机号码
	TEL VARCHAR(50) NOT NULL DEFAULT(''),								--固定电话
	QQ VARCHAR(20) NOT NULL DEFAULT(''),								--QQ
	WeChat VARCHAR(100) NOT NULL DEFAULT(''),							--微信账号：暂不启用
	RegDate DATETIME NOT NULL DEFAULT(GETDATE()),						--注册日期
	RegIp VARCHAR(50) NOT NULL DEFAULT(''),								--注册IP：可以忽略
	PtStatus INT NOT NULL DEFAULT(0),									--状态：1有效 0冻结 -1删除
	ProvinceCode VARCHAR(50) NOT NULL DEFAULT(''),						--省份编号
	CityCode VARCHAR(50) NOT NULL DEFAULT(''),							--城市编号
	DistrictCode VARCHAR(50) NOT NULL DEFAULT(''),						--区域编号
	StreetCode VARCHAR(50) NOT NULL DEFAULT(''),						--街道编号
	DetailedAddress NVARCHAR(150) NOT NULL DEFAULT(''),					--详细地址
	FullAddress NVARCHAR(255) NOT NULL DEFAULT(''),						--完整地址
	PayPwd VARCHAR(100) NOT NULL DEFAULT(''),							--支付密码
	Flag VARCHAR(10) NOT NULL DEFAULT(''),								--标识(S:系统对象,不可删除)
	PwdProblem NVARCHAR(255) NOT NULL DEFAULT(''),						--密码保护问题
	PwdAnswer NVARCHAR(255) NOT NULL DEFAULT(''),						--密码保护答案
	AvailableBalance DECIMAL(18,2) NOT NULL DEFAULT(0),					--可用余额：暂不启用
	LockedBalance DECIMAL(18,2) NOT NULL DEFAULT(0),					--锁定金额：暂不启用
	AvailableIntegral DECIMAL(18,2) NOT NULL DEFAULT(0),				--可用余额
	LockedIntegral DECIMAL(18,2) NOT NULL DEFAULT(0),					--锁定金额
	LoginTimes INT NOT NULL DEFAULT(0),									--登录次数
	LastLoginDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--上次登录日期
	LastLoginIp VARCHAR(50) NOT NULL DEFAULT(''),						--上次登录IP
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--创建日期
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--更新日期
	Remarks nvarchar(255) not null default('')							--备注      
)
GO 