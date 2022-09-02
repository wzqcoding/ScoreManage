INSERT INTO SCOTT.EDU_ACTION (ID,NAME,DESCRIPTION,ADDTIME,ISENABLE,CONFIG) VALUES
	 (1,'试卷管理','创建、修改试卷',TIMESTAMP'2022-08-30 15:44:36.0','1','{"ExamManageMenu":[{"Url":"/ExaminPaper/ExaminPaperList","Title":"试卷列表","Label":"试卷列表"}]}'),
	 (2,'阅卷','考试阅卷',TIMESTAMP'2022-08-30 15:47:20.0','1','{"ExamManageMenu":[{"Url":"/Exam/MarkPaperList","Title":"考试阅卷","Label":"考试阅卷"}]}'),
	 (3,'查看成绩','查看班级成绩',TIMESTAMP'2022-08-30 15:49:41.0','1','{"ExamManageMenu":[{"Url":"/ClassExamin/ClassExaminList","Title":"班级成绩","Label":"班级成绩"}]}'),
	 (4,'考试管理','创建、修改考试信息',TIMESTAMP'2022-08-30 15:53:10.0','1','{"ExamManageMenu":[{"Url":"/Exam/ExamList","Title":"考试列表","Label":"考试列表"}]}'),
	 (5,'处理申诉','学科老师处理学生的申诉',TIMESTAMP'2022-08-30 15:56:58.0','1','{"ExamManageMenu":[{"Url":"/Appeal/AppealList","Title":"申诉处理","Label":"申诉处理"}]}');
INSERT INTO SCOTT.EDU_APPEAL (ID,SCOREDETAILID,REASON,"RESULT",HANDLETEACHERID) VALUES
	 (2,1,'给分太低','给你加两分',5);
INSERT INTO SCOTT.EDU_CLASS (ID,NAME,ISENABLE,ADDTIME) VALUES
	 (3,'高二一班','1',TIMESTAMP'2022-08-30 20:55:20.0'),
	 (2,'高一一班','1',TIMESTAMP'2022-08-30 20:55:00.0');
INSERT INTO SCOTT.EDU_CLASS_TEACHER (TEACHERID,CLASSID,ROLEID) VALUES
	 (3,3,4),
	 (5,3,2),
	 (3,3,2),
	 (5,2,4),
	 (2,2,2),
	 (6,2,2);
INSERT INTO SCOTT.EDU_EXAM (ID,SUBJECTID,NAME,EDU_TEACHERID,ISPUB,STARTTIME,ENDTIME,CLASSID,MARKPAPERTEACHERID) VALUES
	 (3,2,'语文考试',2,'1',TIMESTAMP'2022-08-31 09:00:00.0',TIMESTAMP'2022-08-31 12:00:00.0',2,4),
	 (4,3,'数学考试',6,'1',TIMESTAMP'2022-08-30 12:00:00.0',TIMESTAMP'2022-09-03 12:00:00.0',3,7);
INSERT INTO SCOTT.EDU_EXAMDETIAL (ID,STUDENTID,EXAMID,SCORE,STATUS) VALUES
	 (1,2,3,0,0),
	 (2,3,3,0,0),
	 (3,4,3,0,0),
	 (4,5,4,0,0),
	 (5,6,4,0,0),
	 (6,7,4,13,2);
INSERT INTO SCOTT.EDU_EXAMINE_PAPER (ID,EXAMINEID,DESCRIPTION,NAME,ISENABLE) VALUES
	 (2,3,'语文考试试卷','语文试卷','1'),
	 (3,4,'数学考试试卷','数学试卷','1'),
	 (4,0,'英语考试试卷','英语考试','0');
INSERT INTO SCOTT.EDU_EXAMQUESTIONS (ID,PAPERID,ORDERINDEX,DESCRIPTION,"RESULT",SCORE) VALUES
	 (1,2,1,'伟大的诗人杜甫是哪里人','河南巩义人',5),
	 (2,2,2,'诗仙李白是哪个朝代的人','唐朝',10),
	 (3,2,3,'唐朝接下来是哪个朝代','宋朝',15),
	 (4,3,1,'请计算sin 30度 的值是多少','1/2',10),
	 (5,3,2,'请说出3个质数，并且大于10','11，13，17',5),
	 (9,4,1,'what is your name','i am tom',5),
	 (10,4,2,'how are you','i am fine',10),
	 (11,4,3,'how old are you','ten',20);
INSERT INTO SCOTT.EDU_ROLE (ID,DESCRIPTION,NAME,ADDTIME,ISENABLE) VALUES
	 (4,NULL,'班主任',TIMESTAMP'1900-01-01 00:00:00.0','1'),
	 (2,'教学生一门课程的老师','学科老师',TIMESTAMP'1900-01-01 00:00:00.0','1'),
	 (3,'发布考试','教务老师',TIMESTAMP'1900-01-01 00:00:00.0','1'),
	 (5,'阅卷','阅卷老师',TIMESTAMP'1900-01-01 00:00:00.0','1');
INSERT INTO SCOTT.EDU_ROLE_ACTION (ROLEID,ACTIONID) VALUES
	 (4,3),
	 (3,1),
	 (3,4),
	 (2,5),
	 (5,2);
INSERT INTO SCOTT.EDU_SCOREDETAIL (ID,EXAMDETAILID,QUESTIONID,SCORE,ANSWER) VALUES
	 (1,6,4,8,'1/2'),
	 (2,6,5,5,'11,13,19');
INSERT INTO SCOTT.EDU_STUDENT (ID,CLASSID,NAME,USERID,ISDELETE,STUDYCODE) VALUES
	 (2,2,'杨过',8,'1','00001'),
	 (3,2,'小龙女',9,'1','00002'),
	 (4,2,'郭襄',10,'1','00003'),
	 (5,3,'令狐冲',11,'1','00004'),
	 (6,3,'任盈盈',12,'1','00005'),
	 (7,3,'仪琳',13,'1','00006');
INSERT INTO SCOTT.EDU_SUBJECT (ID,NAME,DESCRIPTION,ISENABLE,ADDTIME) VALUES
	 (3,'数学','数学','1',TIMESTAMP'2022-08-30 20:32:04.0'),
	 (8,'化学',NULL,'1',TIMESTAMP'2022-08-30 20:32:44.0'),
	 (10,'物理',NULL,'1',TIMESTAMP'2022-08-30 20:33:20.0'),
	 (6,'地理',NULL,'1',TIMESTAMP'2022-08-30 20:32:29.0'),
	 (4,'英语','英语','1',TIMESTAMP'2022-08-30 20:32:14.0'),
	 (9,'生物',NULL,'1',TIMESTAMP'2022-08-30 20:33:01.0'),
	 (2,'语文',NULL,'1',TIMESTAMP'2022-08-30 20:31:55.0'),
	 (7,'政治',NULL,'1',TIMESTAMP'2022-08-30 20:32:37.0'),
	 (5,'历史',NULL,'1',TIMESTAMP'2022-08-30 20:32:24.0');
INSERT INTO SCOTT.EDU_TEACHER (ID,EMAIL_ADDRESS,PHONE_NUMBER,NAME,USERID,SUBJECTID,ISDELETE) VALUES
	 (2,'123@163.com','13300000000','风清扬',2,2,'1'),
	 (5,'ouyang@163.com','13300000003','欧阳锋',5,10,'1'),
	 (6,'zhou@126.com','13300000004','周伯通',6,8,'1'),
	 (3,'567@qq.com','13300000001','洪七公',3,3,'1'),
	 (4,'huang@126.com','13300000002','黄药师',4,4,'1'),
	 (7,'yideng@126.com','13300000005','一灯大师',7,6,'1');
INSERT INTO SCOTT.EDU_TEACHER_ROLE (TEACHERID,ROLEID) VALUES
	 (2,2),
	 (2,3),
	 (5,4),
	 (5,2),
	 (5,5),
	 (6,2),
	 (6,3),
	 (3,4),
	 (3,2),
	 (4,2);
INSERT INTO SCOTT.EDU_TEACHER_ROLE (TEACHERID,ROLEID) VALUES
	 (4,5),
	 (7,2),
	 (7,5);
INSERT INTO SCOTT.EDU_USER (ID,USERNAME,PASSWORD,"TYPE",ISENABLE) VALUES
	 (2,'fengqingyang','123456',1,'1'),
	 (8,'yangguo','123456',2,'1'),
	 (5,'ouyangfeng','123456',1,'1'),
	 (9,'xiaolongnv','123456',2,'1'),
	 (6,'zhoubotong','123456',1,'1'),
	 (1,'admin','123456',0,'1'),
	 (3,'hongqi','123456',1,'1'),
	 (4,'huangyaoshi','123456',1,'1'),
	 (7,'yideng','123456',1,'1'),
	 (10,'guoxiang','123456',2,'1');
INSERT INTO SCOTT.EDU_USER (ID,USERNAME,PASSWORD,"TYPE",ISENABLE) VALUES
	 (11,'linghuchong','123456',2,'1'),
	 (12,'renyingying','123456',2,'1'),
	 (13,'yilin','123456',2,'1');
