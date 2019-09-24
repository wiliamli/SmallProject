/*  ���ܣ� ��ý�����ͻ���Portal 
	����ǰ׺�� Sys 
�� 
	�γ̷���(SysCourseCategory)����ý�γ̷���
	�γ�(SysCourse)����ý�γ� 
*/ 
-- �γ�(SysCourse)
IF EXISTS(SELECT * FROM sysobjects WHERE name='SysCourse')
	DROP TABLE SysCourse
CREATE TABLE SysCourse
(
	Id INT IDENTITY(1,1) PRIMARY KEY,									--��������
	CourseCategoryID INT NOT NULL DEFAULT(0),							--�γ̷���
	Name NVARCHAR(100) NOT NULL DEFAULT(''),							--�γ�����
	Price DECIMAL(18,2) NOT NULL DEFAULT(0),							--�۸�
	Picture VARCHAR(50) NOT NULL DEFAULT(''),							--����ͼ[һ��]������ͼƬ·��ֵ
	TencentUrl VARCHAR(255) NOT NULL DEFAULT(''),						--��Ѷ�����ַ����ת����Ѷ���õĵ�ַ 
	CourseTime NVARCHAR(100) NOT NULL DEFAULT(''),						--�γ��Ͽ�ʱ��
	Lecturer NVARCHAR(50) NOT NULL DEFAULT(''),							--������ʦ
	Brief NVARCHAR(1000) NOT NULL DEFAULT(''),							--�γ̸���
	Details NTEXT NOT NULL DEFAULT(''),									--��ϸ���� 
	IsHot INT NOT NULL DEFAULT(0),										--�����Ƽ�
	CourseTags nvarchar(500) not null default(''),						--�γ̱�ǩ(��'|'�ָ�)���� ����|���|�Ƽ�
	BrowseCount INT NOT NULL DEFAULT(0),								--������������
	CommentsCount INT NOT NULL DEFAULT(0),								--�ۼ����� 
	Sort INT NOT NULL DEFAULT(0),										--����
	Remarks NVARCHAR(255) NOT NULL DEFAULT(''),							--��ע
	[Status] [bit] NOT NULL,											--״̬
	[CreateTime] [datetime] NOT NULL,									--��������
	[CreateId] [int] NOT NULL,											--������ID
	[LastModifyTime] [datetime] NULL,									--�޸�����
	[LastModifierId] [int] NULL											--�޸���ID
)
GO

--�γ̷���(SysCourseCategory)
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME='SysCourseCategory')
	DROP TABLE SysCourseCategory
CREATE TABLE SysCourseCategory
(
	Id INT IDENTITY(1,1) PRIMARY KEY,									--��������
	Name NVARCHAR(50) NOT NULL DEFAULT(''),								--��������
	CateDescription NVARCHAR(500) NOT NULL DEFAULT(''),					--��������
	ParentID INT NOT NULL DEFAULT(0),									--��������ID
	Icon VARCHAR(255) NOT NULL DEFAULT(''),								--����ͼ��  
	IsVisible INT NOT NULL DEFAULT(0),									--ǰ̨�ɼ����ɼ�(1)�����ɼ�(0)
	CategoryPath VARCHAR(500) NOT NULL DEFAULT(''),						--����·��(���ID·��)
	IsLastNode INT NOT NULL DEFAULT(0),									--�����ڵ�
	ChildCount INT NOT NULL DEFAULT(0),									--�ӷ�������
	CourseCount INT NOT NULL DEFAULT(0),								--�γ��������ݲ�����
	Remarks NVARCHAR(255) NOT NULL DEFAULT(''),							--��ע 
	Sort INT NOT NULL DEFAULT(0),										--����
	[Status] [bit] NOT NULL,											--״̬
	[CreateTime] [datetime] NOT NULL,									--��������
	[CreateId] [int] NOT NULL,											--������ID
	[LastModifyTime] [datetime] NULL,									--�޸�����
	[LastModifierId] [int] NULL											--�޸���ID
)
GO
 