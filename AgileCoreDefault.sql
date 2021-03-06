/***********************************************************  
USUARIO
***********************************************************/
CREATE TABLE USUARIO
(
    COD_USUARIO		  			INT NOT NULL 	AUTO_INCREMENT,
	NOME     					VARCHAR(200) 	NOT NULL,
	CPF_CNPJ					VARCHAR(30) 	NULL,
    LOGIN     					VARCHAR(20) 	NOT NULL,
    SENHA    		        	VARCHAR(100) 	NULL,
	EMAIL     					VARCHAR(100) 	NOT NULL,
    DTH_CRIACAO              	DATETIME        DEFAULT CURRENT_TIMESTAMP, 
	DTH_CANCELAMENTO          	DATETIME        NULL,    
    ENDERECO					VARCHAR(200) 	NOT NULL,
	NUMERO 						NUMERIC 		DEFAULT 0,
    CIDADE						VARCHAR(100) 	NOT NULL,
    UF							CHAR(2) 		NOT NULL,
    BAIRRO						VARCHAR(100) 	NOT NULL,
	PAIS						VARCHAR(100) 	NOT NULL,
    CEP							VARCHAR(10) 	NULL,
    COMPLEMENTO					VARCHAR(100) 	NULL,
    TELEFONE					VARCHAR(15) 	NULL,
    CELULAR						VARCHAR(15) 	NULL,
	SEXO     					CHAR(1) 	   	NOT NULL,
	FLAG_ADM 					CHAR(1) 	   	NOT NULL,
	FOTO 						LONGTEXT 		NULL,   
	STATUS 					    CHAR(1) 	   	NOT NULL,
    PRIMARY KEY (COD_USUARIO)
);

ALTER TABLE USUARIO AUTO_INCREMENT = 1000;

/***********************************************************  
ACESSO
***********************************************************/
CREATE TABLE ACESSO
(
    COD_ACESSO	  				INT NOT NULL 	AUTO_INCREMENT,
	COD_USUARIO		  			INT 			DEFAULT NULL,  
	TIPO_USUARIO				CHAR(1)         NULL,
    IP    		        		VARCHAR(100) 	NULL,
	BROWSER	        			VARCHAR(100) 	NULL,
	PLATAFORMA        			VARCHAR(100) 	NULL,
	RESOLUCAO   				VARCHAR(100) 	NULL,
	DTH_ACESSO   				DATETIME        DEFAULT CURRENT_TIMESTAMP,
    DIA							INT				DEFAULT 0,
	MES							INT				DEFAULT 0,
	ANO							INT				DEFAULT 0,
    HORA						VARCHAR(5) 		NULL,
	URL_ACESSO					VARCHAR(100) 	NULL,
    DISPOSITIVO					VARCHAR(100) 	NULL,
    DOMINIO						VARCHAR(100) 	NULL,
	SUPORTA_ACTIVEX             CHAR(1)			NULL,
	SUPORTA_COOKIES             CHAR(1)         NULL,
    SUPORTA_JAVA_APPLET			CHAR(1)         NULL,
    COORD_LATITUDE				VARCHAR(50)     NULL,
	COORD_LONGITUDE				VARCHAR(50)     NULL,
	CIDADE						VARCHAR(100)    NULL,
    PRIMARY KEY (COD_ACESSO)
);

ALTER TABLE ACESSO AUTO_INCREMENT = 0;

/***********************************************************  
MENU
***********************************************************/
CREATE TABLE MENU
(
    COD_MENU		  			INT NOT NULL 	AUTO_INCREMENT,
	COD_MENU_PAI	  			INT 			DEFAULT 0,
	NOME    		    		VARCHAR(100) 	NOT NULL,
	CONTROLLER					VARCHAR(100) 	NULL,
	ACTION						VARCHAR(100) 	NULL,
	DESCRICAO    		        TEXT 			NULL,
	DTH_CADASTRO   				DATETIME        DEFAULT CURRENT_TIMESTAMP,  
	ICONE						VARCHAR(100) 	NULL,
	STATUS						CHAR(1)         NOT NULL,
    PRIMARY KEY (COD_MENU)
);

ALTER TABLE MENU AUTO_INCREMENT = 1000;

-- MENU PADRAO
INSERT INTO MENU (COD_MENU, COD_MENU_PAI, NOME, CONTROLLER, ACTION, DESCRICAO, DTH_CADASTRO, ICONE, STATUS) VALUES (1000, 0,    'Administração', NULL, NULL, NULL, NULL, 'fa-cube', 'A');
INSERT INTO MENU (COD_MENU, COD_MENU_PAI, NOME, CONTROLLER, ACTION, DESCRICAO, DTH_CADASTRO, ICONE, STATUS) VALUES (1001, 1000, 'Usuarios', '/Usuario', '/Index', NULL, NULL, 'fa-stack-overflow', 'A');
INSERT INTO MENU (COD_MENU, COD_MENU_PAI, NOME, CONTROLLER, ACTION, DESCRICAO, DTH_CADASTRO, ICONE, STATUS) VALUES (1002, 1000, 'Menus', '/Menu', '/Index', NULL, '2015-09-15T01:51:39.000000', 'fa-cube', 'A');
INSERT INTO MENU (COD_MENU, COD_MENU_PAI, NOME, CONTROLLER, ACTION, DESCRICAO, DTH_CADASTRO, ICONE, STATUS) VALUES (1003, 1000, 'Históricos', '/Historico', '/Index', NULL, '2015-09-15T01:51:39.000000', 'fa-pencil', 'A');
INSERT INTO MENU (COD_MENU, COD_MENU_PAI, NOME, CONTROLLER, ACTION, DESCRICAO, DTH_CADASTRO, ICONE, STATUS) VALUES (1004, 1000, 'Bugs', '/Bugs', '/Index', NULL, '2015-09-15T01:51:39.000000', 'fa-bug', 'A');
COMMIT;

/***********************************************************  
MENU_USUARIO
***********************************************************/
CREATE TABLE MENU_USUARIO
(
	COD_RELACAO		  			INT NOT NULL 	AUTO_INCREMENT,
    COD_MENU		  			INT NOT NULL,
    COD_USUARIO					INT NOT NULL,
    PRIMARY KEY (COD_RELACAO),
    FOREIGN KEY (COD_MENU) REFERENCES MENU(COD_MENU),
    FOREIGN KEY (COD_USUARIO) REFERENCES USUARIO(COD_USUARIO)
);

ALTER TABLE MENU_USUARIO AUTO_INCREMENT = 1000;

/***********************************************************  
ASPNET_APPLICATIONS
***********************************************************/
CREATE TABLE ASPNET_APPLICATIONS (
  APPLICATIONNAME TEXT,
  APPLICATIONID INT(11) NOT NULL AUTO_INCREMENT,
  DESCRIPTION TEXT,
  PRIMARY KEY (APPLICATIONID)
);

/***********************************************************  
ASPNET_PROFILE
***********************************************************/
CREATE TABLE ASPNET_PROFILE (
  USERID BIGINT(20) DEFAULT NULL,
  PROPERTYNAMES TEXT,
  PROPERTYVALUESSTRING TEXT,
  LASTUPDATEDDATE DATETIME DEFAULT NULL
);
 
/***********************************************************  
MY_ASPNET_APPLICATIONS
***********************************************************/
CREATE TABLE MY_ASPNET_APPLICATIONS (
  ID INT(11) NOT NULL AUTO_INCREMENT,
  NAME VARCHAR(256) DEFAULT NULL,
  DESCRIPTION VARCHAR(256) DEFAULT NULL,
  PRIMARY KEY (ID)
);

/***********************************************************  
MY_ASPNET_MEMBERSHIP
***********************************************************/
CREATE TABLE MY_ASPNET_MEMBERSHIP (
  USERID INT(11) NOT NULL DEFAULT '0',
  EMAIL VARCHAR(128) DEFAULT NULL,
  COMMENT VARCHAR(255) DEFAULT NULL,
  PASSWORD VARCHAR(128) NOT NULL,
  PASSWORDKEY CHAR(32) DEFAULT NULL,
  PASSWORDFORMAT TINYINT(4) DEFAULT NULL,
  PASSWORDQUESTION VARCHAR(255) DEFAULT NULL,
  PASSWORDANSWER VARCHAR(255) DEFAULT NULL,
  ISAPPROVED TINYINT(1) DEFAULT NULL,
  LASTACTIVITYDATE DATETIME DEFAULT NULL,
  LASTLOGINDATE DATETIME DEFAULT NULL,
  LASTPASSWORDCHANGEDDATE DATETIME DEFAULT NULL,
  CREATIONDATE DATETIME DEFAULT NULL,
  ISLOCKEDOUT TINYINT(1) DEFAULT NULL,
  LASTLOCKEDOUTDATE DATETIME DEFAULT NULL,
  FAILEDPASSWORDATTEMPTCOUNT INT(10) UNSIGNED DEFAULT NULL,
  FAILEDPASSWORDATTEMPTWINDOWSTART DATETIME DEFAULT NULL,
  FAILEDPASSWORDANSWERATTEMPTCOUNT INT(10) UNSIGNED DEFAULT NULL,
  FAILEDPASSWORDANSWERATTEMPTWINDOWSTART DATETIME DEFAULT NULL,
  PRIMARY KEY (USERID)
);

/***********************************************************  
MY_ASPNET_PROFILES
***********************************************************/
CREATE TABLE MY_ASPNET_PROFILES (
  USERID INT(11) NOT NULL,
  VALUEINDEX LONGTEXT,
  STRINGDATA LONGTEXT,
  BINARYDATA LONGBLOB,
  LASTUPDATEDDATE TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (USERID)
);

/***********************************************************  
MY_ASPNET_ROLES
***********************************************************/
CREATE TABLE MY_ASPNET_ROLES (
  ID INT(11) NOT NULL AUTO_INCREMENT,
  APPLICATIONID INT(11) NOT NULL,
  NAME VARCHAR(255) NOT NULL,
  PRIMARY KEY (ID)
);

/***********************************************************  
MY_ASPNET_SCHEMAVERSION
***********************************************************/
CREATE TABLE MY_ASPNET_SCHEMAVERSION (
  VERSION INT(11) DEFAULT NULL
);

/***********************************************************  
MY_ASPNET_SESSIONCLEANUP
***********************************************************/
CREATE TABLE MY_ASPNET_SESSIONCLEANUP (
  LASTRUN DATETIME NOT NULL,
  INTERVALMINUTES INT(11) NOT NULL
);

/***********************************************************  
MY_ASPNET_SESSIONS
***********************************************************/
CREATE TABLE MY_ASPNET_SESSIONS (
  SESSIONID VARCHAR(255) NOT NULL,
  APPLICATIONID INT(11) NOT NULL,
  CREATED DATETIME NOT NULL,
  EXPIRES DATETIME NOT NULL,
  LOCKDATE DATETIME NOT NULL,
  LOCKID INT(11) NOT NULL,
  TIMEOUT INT(11) NOT NULL,
  LOCKED TINYINT(1) NOT NULL,
  SESSIONITEMS LONGBLOB,
  FLAGS INT(11) NOT NULL,
  PRIMARY KEY (SESSIONID,APPLICATIONID)
);

/***********************************************************  
MY_ASPNET_USERS
***********************************************************/
CREATE TABLE MY_ASPNET_USERS (
  ID INT(11) NOT NULL AUTO_INCREMENT,
  APPLICATIONID INT(11) NOT NULL,
  NAME VARCHAR(256) NOT NULL,
  ISANONYMOUS TINYINT(1) NOT NULL DEFAULT '1',
  LASTACTIVITYDATE DATETIME DEFAULT NULL,
  PRIMARY KEY (ID)
);

/***********************************************************  
MY_ASPNET_USERSINROLES
***********************************************************/
CREATE TABLE MY_ASPNET_USERSINROLES (
  USERID INT(11) NOT NULL DEFAULT '0',
  ROLEID INT(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (USERID,ROLEID)
);

/***********************************************************  
TABELA
***********************************************************/
CREATE TABLE TABELA (
  ID INT(11) NOT NULL,
  NOME VARCHAR(20) NOT NULL,
  DATA DATE NOT NULL,
  STATUS CHAR(1) NOT NULL,
  SALARIO DECIMAL(11,0) DEFAULT NULL
);

INSERT INTO MY_ASPNET_APPLICATIONS (ID, NAME, DESCRIPTION) VALUES 
  (1,'/','MYSQL PROFILE PROVIDER');
COMMIT;

INSERT INTO MY_ASPNET_PROFILES (USERID, VALUEINDEX, STRINGDATA, BINARYDATA, LASTUPDATEDDATE) VALUES 
  (1,'NOME/0/0/3:','AAA','','2011-01-24 20:54:10');
COMMIT;

INSERT INTO MY_ASPNET_SCHEMAVERSION (VERSION) VALUES 
  (6);
COMMIT;

INSERT INTO MY_ASPNET_SESSIONCLEANUP (LASTRUN, INTERVALMINUTES) VALUES 
  ('2011-01-24 20:52:30',10);
COMMIT;

INSERT INTO MY_ASPNET_USERS (ID, APPLICATIONID, NAME, ISANONYMOUS, LASTACTIVITYDATE) VALUES 
  (1,1,'CASA-PC\\JUNIOR',0,'2011-01-24 20:52:31');
COMMIT;
