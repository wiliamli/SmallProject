use master 
go 
--创建数据库
create database RuanmouData
on primary 
(
    name = 'RuanmouData_data',--数据库文件的逻辑名
    filename='D:\DB\RuanmouData_data.mdf',--逻辑名+mdf 主数据文件  数据库物理文件名（绝对路径）
    size=10MB,--数据库文件初始大小
    filegrowth=5MB --数据文件增长量
)
--创建日志文件
log on
(
    name = 'RuanmouData_log',
    filename = 'D:\DB\RuanmouData_log.ldf', --ldf表示日志文件
    size = 5MB,
    filegrowth = 2MB
)
go