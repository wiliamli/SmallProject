/*  ���ܣ� ��ý�����ͻ���Portal 
	����ǰ׺�� Pt
	���˵���� 
		1 ��ͨ��������̶�ʹ��Kid, int �����ͣ�ӳ������� ��Guid
		2 �����ֶζ�����Ĭ��ֵ,��ȥNULL�Ĳ�ѯ�鷳��
		3 ��������Ĭ��ֵ '1900-01-01',�����ж�����'1900-01-01'ʱ����Ϊû�����ù�
��
	��Ա(PtMember)��VIPѧԱ
	�γ̷���(PtCourseCategory)����ý�γ̷���
	�γ�(PtCourse)����ý�γ�
	�༶(PtClass)��һ���γ̶�Ӧ�����ͬѧ�ڵİ༶
	ѧԱ�༶ӳ��(PtMapMemberClass)��һ��ѧԱ��������������ͬ����İ༶
	��վ����(PtWebContent)�������̶�ҳ������ά���� bannerά���ȵ�
	���ع���(PtDownload)��������������
	���·���(PtNewsCategory)
	����(PtNews)
	������ϸ(PtMemberIntegrals)��ѧԱ������ˮ
	���ֶһ���¼(PtIntegralExchange)
	��վϵͳ����(PtSysParams)
	��Ա����Ȩ��(PtMapMemberNewsCategory)��ѧԱȨ�޿��ƣ�һ��ѧԱ����鿴������Ȩ�ޡ�����һ�������"��������",����ѧԱ�����������µ��ĸ�����
	ʡ���������ļ���ProvinceAndCity.sql�������ȡ����xml�̶��������ļ�Address.xml�����ٶ�ȡ���ݿ�
*/

--��Ա����Ȩ��(PtMapMemberNews): ��ֻ��� "��������" �������ʹ��
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMapMemberNews')
	DROP TABLE PtMapMemberNews
CREATE TABLE PtMapMemberNews
(
	Ukid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT(NEWID()),					--Guid����
	MemberID INT NOT NULL DEFAULT(0),									--ѧԱID
	NewsID INT NOT NULL DEFAULT(0),										--����ID
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE())						--��������
)
GO

-- ��վϵͳ����(PtSysParams)
IF EXISTS(SELECT * FROM sysobjects WHERE name='PtSysParams')
	DROP TABLE PtSysParams
CREATE TABLE PtSysParams
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--�������� 
	ParamsName NVARCHAR(50) NOT NULL DEFAULT(''),						--�������ƣ�����Ψһ
	ParamsValue NVARCHAR(255) NOT NULL DEFAULT(''),						--����ֵ(���ֻ�����)
	DateValue DATETIME NOT NULL DEFAULT('1900-01-01'),					--����ֵ(��������ֵ) 
	IsVisible INT NOT NULL DEFAULT(0),									--�ɼ����ɼ�(1)�����ɼ�(0)
	TabCode VARCHAR(50) NOT NULL DEFAULT(''),							--ѡ���ţ���̨���õ�ѡ���ţ������̨ʹ��
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--�޸�����
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע
)
GO

-- ���ֶһ���¼(PtIntegralExchange)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtIntegralExchange')
	DROP TABLE PtIntegralExchange
CREATE TABLE PtIntegralExchange
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--�������� 
	SN VARCHAR(50) NOT NULL DEFAULT(''),								--�һ���¼��ˮ��
	Category VARCHAR(50) NOT NULL DEFAULT(''),							--�һ����࣬�۲����ã�Ĭ��ֻ��һ�ֻ��ֶһ�
	MemberID INT NOT NULL DEFAULT(0),									--ѧԱID 
	Amount DECIMAL(18,2) NOT NULL DEFAULT(0),							--���Ļ���
	ExchangeItems NVARCHAR(255) NOT NULL DEFAULT(''),					--�һ���Ʒ����
	ExchangeDesc NVARCHAR(500) NOT NULL DEFAULT(''),					--�һ�����
	AuditUserID INT NOT NULL DEFAULT(0),								--��˹���ԱID
	AuditDate DATETIME NOT NULL DEFAULT('1900-01-01'),					--�������
	AuditStatus INT NOT NULL DEFAULT(0),								--���״̬:0 δ��� -1��˲�ͨ�� 1���ͨ��
	AuditResult NVARCHAR(1000) NOT NULL DEFAULT(''),					--��˽�� 
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0������ -1ɾ��
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--��������
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע  
)
GO 

--	������ϸ(PtMemberIntegrals)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMemberIntegrals')
	DROP TABLE PtMemberIntegrals
CREATE TABLE PtMemberIntegrals
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	SN VARCHAR(50) NOT NULL DEFAULT(''),								--��ˮ��
	PtType VARCHAR(50) NOT NULL DEFAULT(''),							--�������� (G��ͨ����,L�������� ��)
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	MemberID INT NOT NULL DEFAULT(0),									--ѧԱID
	FundCategory VARCHAR(50) NOT NULL DEFAULT(''),						--���ֵ���֧��Ŀ�����ͻ��֣����ۻ��֣�ǩ�����ֵ�
	Amount DECIMAL(18,2) NOT NULL DEFAULT(0),							--����ֵ
	Abstract NVARCHAR(500) NOT NULL DEFAULT(''),						--ժҪ˵��
	RelatedTag NVARCHAR(50) NOT NULL DEFAULT(''),						--������ǩ��(������ǩ�����Լ�¼���ֵ���Դ��ȥ������֤���ݵĿ�׷����)
	RelatedSN NVARCHAR(50) NOT NULL DEFAULT(''),						--������ˮ��(�Ƽ�����ˮ��)
	RelatedID INT NOT NULL DEFAULT(0),									--����ID(û�б����ID)
	Balance DECIMAL(18,2) NOT NULL DEFAULT(0),							--���(�������ͻ�Ա��(�˻����-�������)Ҫһ��)
	PtStatus INT NOT NULL DEFAULT(0),									--״̬ 
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע (���������ϲ������޸ģ�ֻ�����Ӽ�¼��������޸�ʱ��)
)
GO

-- ���·���(PtNewsCategory)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtNewsCategory')
	DROP TABLE PtNewsCategory
CREATE TABLE PtNewsCategory
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	PtType VARCHAR(50) NOT NULL DEFAULT(''),							--����(P����(paper) N����(notice) Mվ����Ϣ(message))
	Name NVARCHAR(50) NOT NULL DEFAULT(''),								--��������
	CateDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--��������
	ParentID INT NOT NULL DEFAULT(0),									--��������ID
	SeoKeys NVARCHAR(255) NOT NULL DEFAULT(''),							--SEO�ؼ���
	SeoDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--SEO����
	IsVisible INT NOT NULL DEFAULT(0),									--ǰ̨�ɼ����ɼ�(1)�����ɼ�(0)
	Flag VARCHAR(10) NOT NULL DEFAULT(''),								--��ʶ(S:ϵͳ����,����ɾ��)
	IsLastNode INT NOT NULL DEFAULT(0),									--�����ڵ�
	ChildCount INT NOT NULL DEFAULT(0),									--�ӷ�������
	NewsCount INT NOT NULL DEFAULT(0),									--�����������ݲ����� 
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0������ -1ɾ��
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--�������� 
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע 
)
GO 

--����(PtNews)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtNews')
	DROP TABLE PtNews
CREATE TABLE PtNews
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	CategoryID INT NOT NULL DEFAULT(0),									--���·���ID
	Title NVARCHAR(255) NOT NULL DEFAULT(''),							--����
	SubTitle NVARCHAR(255) NOT NULL DEFAULT(''),						--������
	Picture VARCHAR(255) NOT NULL DEFAULT(''),							--����ͼ
	Author NVARCHAR(50) NOT NULL DEFAULT(''),							--����
	Editor NVARCHAR(50) NOT NULL DEFAULT(''),							--���α༭
	NewsSource NVARCHAR(255) NOT NULL DEFAULT(''),						--������Դ
	Brief NVARCHAR(500) NOT NULL DEFAULT(''),							--���
	Content NTEXT NOT NULL DEFAULT(''),									--����
	SeoKeys NVARCHAR(255) NOT NULL DEFAULT(''),							--SEO�ؼ���
	SeoDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--SEO����
	Links VARCHAR(255) NOT NULL DEFAULT(''),							--�ⲿ����
	AuditUserID INT NOT NULL DEFAULT(0),								--��˹���ԱID
	AuditDate DATETIME NOT NULL DEFAULT('1900-01-01'),					--�������
	AuditStatus INT NOT NULL DEFAULT(0),								--���״̬:0 δ��� -1��˲�ͨ�� 1���ͨ��
	AuditResult NVARCHAR(1000) NOT NULL DEFAULT(''),					--��˽�� 
	BrowseTimes INT NOT  NULL DEFAULT(0),								--�����
	IsTop INT NOT NULL DEFAULT(0),										--�ö�
	NewsTags nvarchar(500) not null default(''),						--���±�ǩ(��'|'�ָ�)���� ����|ͷ��
	PtStatus INT NOT NULL DEFAULT(0),									--״̬
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--��������
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע  
)
GO 

--���ع���(PtDownload)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtDownload')
	DROP TABLE PtDownload
CREATE TABLE PtDownload
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	Category VARCHAR(50) NOT NULL DEFAULT(''),							--����,�� T:��������, V:�μ���Ƶ, C:�μ�����
	PtGroup VARCHAR(50) NOT NULL DEFAULT(''),							--���顣�� �������߷�: ǰ�ˣ����ݿ�,C# �ȵ�
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--�����ļ�����,�����ļ�����
	Picture VARCHAR(50) NOT NULL DEFAULT(''),							--����ͼ������ͼƬ����
	DownloadUrl VARCHAR(255) NOT NULL DEFAULT(''),						--���ص�ַ���ٶ��Ƶ�ַ
	DownloadPwd VARCHAR(50) NOT NULL DEFAULT(''),						--������Կ���ٶ������� 
	FileDesc NVARCHAR(500) NOT NULL DEFAULT(''),						--�ļ���Ҫ���� 
	DownloadTimes INT NOT NULL DEFAULT(0),								--���ش���
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0����ʾ -1ɾ��
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildUserID INT NOT NULL DEFAULT(0),								--��������ԱID
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyUserID INT NOT NULL DEFAULT(0),								--���¹���ԱID
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--��������
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע
)
GO

--��վ����
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtWebContent')
	DROP TABLE PtWebContent
CREATE TABLE PtWebContent
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	PtType VARCHAR(50) NOT NULL DEFAULT(''),							--���ͣ�textarea(�༭������)��text(�ı�����)��image(ͼƬ����)��background-image(����ͼ)��htmlҳ��
	Category VARCHAR(50) NOT NULL DEFAULT(''),							--���࣬�� banner, tag
	ContentLabel VARCHAR(50) NOT NULL DEFAULT(''),						--���ݱ�ǩ��Ψһ��
	ContentTitle NVARCHAR(50) NOT NULL DEFAULT(''),						--���ݱ��⣺��̨ά��ʱ��ʾ�ı���
	ContentDesc NVARCHAR(500) NOT NULL DEFAULT(''),						--��������
	ContentMain NTEXT NOT NULL DEFAULT(''),								--��ϸ����
	ImageName VARCHAR(100) NOT NULL DEFAULT(''),						--ͼƬ���ƣ���ͼƬ����ʹ��
	ImageTitle NVARCHAR(100) NOT NULL DEFAULT(''),						--ͼƬ���⣬��ͼƬ����ʹ��
	Links VARCHAR(255) NOT NULL DEFAULT(''),							--���ӵ�ַ
	Tips NVARCHAR(255) NOT NULL DEFAULT(''),							--��ʾ��Ϣ,����ʾ�ϴ�ͼƬ��С
	Location VARCHAR(100) NOT NULL DEFAULT(''),							--��ʾλ�ñ�ǩ 
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0����ʾ -1ɾ��
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--��������
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע
)
GO

--ѧԱ�༶ӳ��(PtMapMemberClass)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMapMemberClass')
	DROP TABLE PtMapMemberClass
CREATE TABLE PtMapMemberClass
(
	Ukid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT(NEWID()),					--Guid����
	MemberID INT NOT NULL DEFAULT(0),									--ѧԱID
	ClassID INT NOT NULL DEFAULT(0),									--�༶ID
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE())						--��������
)
GO

--�༶(PtClass)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtClass')
	DROP TABLE PtClass
CREATE TABLE PtClass
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	CourseID INT NOT NULL DEFAULT(0),									--�γ�ID(������ʾ�γ̷���)
	Semester INT NOT NULL DEFAULT(0),									--ѧ��
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--�༶����
	DisplayName NVARCHAR(100) NOT NULL DEFAULT(''),						--�༶��ʾ����
	HeadTeacher NVARCHAR(50) NOT NULL DEFAULT(''),						--������
	HeadTeacherQQ VARCHAR(20) NOT NULL DEFAULT(''),						--������QQ
	MonitorID INT NOT NULL DEFAULT(0),									--�೤ID(����ѧԱ)
	GroupQQ VARCHAR(50) NOT NULL DEFAULT(''),							--ȺQQ�� 
	OpeningDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--��ѧ����
	GraduationDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--��ҵ����
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0���� -1ɾ��  
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--�޸�����
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע
)
GO

-- �γ�(PtCourse)
IF EXISTS(SELECT * FROM sysobjects WHERE name='PtCourse')
	DROP TABLE PtCourse
CREATE TABLE PtCourse
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	CourseCategoryID INT NOT NULL DEFAULT(0),							--�γ̷���
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--�γ�����
	Price DECIMAL(18,2) NOT NULL DEFAULT(0),							--�۸�
	Picture VARCHAR(50) NOT NULL DEFAULT(''),							--����ͼ[һ��]������ͼƬ·��ֵ
	TencentUrl VARCHAR(255) NOT NULL DEFAULT(''),						--��Ѷ�����ַ����ת����Ѷ���õĵ�ַ 
	CourseTime NVARCHAR(100) NOT NULL DEFAULT(''),						--�γ��Ͽ�ʱ��
	Lecturer NVARCHAR(50) NOT NULL DEFAULT(''),							--������ʦ
	Brief NVARCHAR(1000) NOT NULL DEFAULT(''),							--�γ̸���
	Details NTEXT NOT NULL DEFAULT(''),									--��ϸ���� 
	IsShelvesOn INT NOT NULL DEFAULT(0),								--�Ƿ��ϼܣ�0�� 1��
	ShelvesOnDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--�ϼ�����
	ShelvesOffDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--�¼����� 
	AuditUserID INT NOT NULL DEFAULT(0),								--��˹���ԱID
	AuditDate DATETIME NOT NULL DEFAULT('1900-01-01'),					--�������
	AuditStatus INT NOT NULL DEFAULT(0),								--���״̬:0 δ��� -1��˲�ͨ�� 1���ͨ��
	AuditResult NVARCHAR(1000) NOT NULL DEFAULT(''),					--��˽�� 
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0���� -1ɾ�� 
	IsHot INT NOT NULL DEFAULT(0),										--�����Ƽ�
	CourseTags nvarchar(500) not null default(''),						--�γ̱�ǩ(��'|'�ָ�)���� ����|���|�Ƽ�
	BrowseCount INT NOT NULL DEFAULT(0),								--������������
	CommentsCount INT NOT NULL DEFAULT(0),								--�ۼ����� 
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--�޸�����
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע
)
GO

--�γ̷���(PtCourseCategory)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtCourseCategory')
	DROP TABLE PtCourseCategory
CREATE TABLE PtCourseCategory
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	Name NVARCHAR(50) NOT NULL DEFAULT(''),								--��������
	CateDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--��������
	ParentID INT NOT NULL DEFAULT(0),									--��������ID
	Icon VARCHAR(255) NOT NULL DEFAULT(''),								--����ͼ�� 
	SeoKeys NVARCHAR(255) NOT NULL DEFAULT(''),							--SEO�ؼ��֣��ݲ�����
	SeoDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--SEO�������ݲ�����
	Sort DECIMAL(18,2) NOT NULL DEFAULT(0),								--����
	IsVisible INT NOT NULL DEFAULT(0),									--ǰ̨�ɼ����ɼ�(1)�����ɼ�(0)
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0���� -1ɾ��
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--��������
	CategoryPath VARCHAR(500) NOT NULL DEFAULT(''),						--����·��(���ID·��)
	Flag VARCHAR(10) NOT NULL DEFAULT(''),								--��ʶ(S:ϵͳ����,����ɾ��)
	IsLastNode INT NOT NULL DEFAULT(0),									--�����ڵ�
	ChildCount INT NOT NULL DEFAULT(0),									--�ӷ�������
	CourseCount INT NOT NULL DEFAULT(0),								--�γ��������ݲ�����
	Remarks NVARCHAR(255) NOT NULL DEFAULT('')							--��ע 
)
GO

--��Ա(PtMember)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='PtMember')
	DROP TABLE PtMember
CREATE TABLE PtMember
(
	Kid INT IDENTITY(1,1) PRIMARY KEY,									--��������
	Code VARCHAR(50) NOT NULL DEFAULT(''),								--ѧԱ���(ѧ��)����ͬ������ID
	Account VARCHAR(50) NOT NULL DEFAULT(''),							--ѧԱ�˺ţ���¼�˺�
	Pwd VARCHAR(100) NOT NULL DEFAULT(''),								--ѧԱ���룺��ĸ��������ϣ�6-12λ
	NickName NVARCHAR(50) NOT NULL DEFAULT(''),							--�ǳ�
	SpecialTitle NVARCHAR(50) NOT NULL DEFAULT(''),						--����ƺ�(��ʦ���ķ��)
	Gender VARCHAR(2) NOT NULL DEFAULT('U'),							--�Ա�: ��(M),Ů(F),δ֪(U)
	Face VARCHAR(255) NOT NULL DEFAULT(''),								--ͷ��
	LvCode VARCHAR(50) NOT NULL DEFAULT(''),							--��Ա�����ţ��ݲ����á�
	RealName NVARCHAR(50) NOT NULL DEFAULT(''),							--����
	SocialId VARCHAR(50) NOT NULL DEFAULT(''),							--���֤��
	Email VARCHAR(255) NOT NULL DEFAULT(''),							--��������
	Mobile VARCHAR(20) NOT NULL DEFAULT(''),							--�ֻ�����
	TEL VARCHAR(50) NOT NULL DEFAULT(''),								--�̶��绰
	QQ VARCHAR(20) NOT NULL DEFAULT(''),								--QQ
	WeChat VARCHAR(100) NOT NULL DEFAULT(''),							--΢���˺ţ��ݲ�����
	RegDate DATETIME NOT NULL DEFAULT(GETDATE()),						--ע������
	RegIp VARCHAR(50) NOT NULL DEFAULT(''),								--ע��IP�����Ժ���
	PtStatus INT NOT NULL DEFAULT(0),									--״̬��1��Ч 0���� -1ɾ��
	ProvinceCode VARCHAR(50) NOT NULL DEFAULT(''),						--ʡ�ݱ��
	CityCode VARCHAR(50) NOT NULL DEFAULT(''),							--���б��
	DistrictCode VARCHAR(50) NOT NULL DEFAULT(''),						--������
	StreetCode VARCHAR(50) NOT NULL DEFAULT(''),						--�ֵ����
	DetailedAddress NVARCHAR(150) NOT NULL DEFAULT(''),					--��ϸ��ַ
	FullAddress NVARCHAR(255) NOT NULL DEFAULT(''),						--������ַ
	PayPwd VARCHAR(100) NOT NULL DEFAULT(''),							--֧������
	Flag VARCHAR(10) NOT NULL DEFAULT(''),								--��ʶ(S:ϵͳ����,����ɾ��)
	PwdProblem NVARCHAR(255) NOT NULL DEFAULT(''),						--���뱣������
	PwdAnswer NVARCHAR(255) NOT NULL DEFAULT(''),						--���뱣����
	AvailableBalance DECIMAL(18,2) NOT NULL DEFAULT(0),					--�������ݲ�����
	LockedBalance DECIMAL(18,2) NOT NULL DEFAULT(0),					--�������ݲ�����
	AvailableIntegral DECIMAL(18,2) NOT NULL DEFAULT(0),				--�������
	LockedIntegral DECIMAL(18,2) NOT NULL DEFAULT(0),					--�������
	LoginTimes INT NOT NULL DEFAULT(0),									--��¼����
	LastLoginDate DATETIME NOT NULL DEFAULT('1900-01-01'),				--�ϴε�¼����
	LastLoginIp VARCHAR(50) NOT NULL DEFAULT(''),						--�ϴε�¼IP
	BuildDate DATETIME NOT NULL DEFAULT(GETDATE()),						--��������
	ModifyDate DATETIME NOT NULL DEFAULT(GETDATE()),					--��������
	Remarks nvarchar(255) not null default('')							--��ע      
)
GO 