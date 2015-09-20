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
	FLAG_ADM 					CHAR(1) 	   	NOT NULL
	FOTO 						LONGTEXT 		NULL,   
	STATUS 					    CHAR(1) 	   	NOT NULL
    PRIMARY KEY (COD_USUARIO)
);

ALTER TABLE USUARIO AUTO_INCREMENT = 1000;

/*
ALTER TABLE USUARIO ADD COLUMN ENDERECO	VARCHAR(200) NOT NULL;
ALTER TABLE USUARIO ADD COLUMN CIDADE	VARCHAR(100) NOT NULL;
ALTER TABLE USUARIO ADD COLUMN UF	VARCHAR(2) NOT NULL;
ALTER TABLE USUARIO ADD COLUMN BAIRRO varchar(100) NULL;
ALTER TABLE USUARIO ADD COLUMN CEP	VARCHAR(10) NULL;
ALTER TABLE USUARIO ADD COLUMN TELEFONE	VARCHAR(15) NULL;
ALTER TABLE USUARIO ADD COLUMN CELULAR	VARCHAR(15) NULL;
ALTER TABLE USUARIO ADD COLUMN NUMERO NUMERIC DEFAULT 0;
ALTER TABLE USUARIO ADD COLUMN CPF_CNPJ	VARCHAR(30) NULL;
ALTER TABLE USUARIO ADD COLUMN PAIS	VARCHAR(100) NOT NULL;
ALTER TABLE USUARIO ADD COLUMN FLAG_ADM CHAR(1) NOT NULL;
ALTER TABLE USUARIO ADD COLUMN FOTO	LONGTEXT NULL;
ALTER TABLE USUARIO ADD COLUMN COMPLEMENTO	VARCHAR(100) NULL;
*/

/***********************************************************  
TIPO_NEWSLETTER
***********************************************************/
CREATE TABLE TIPO_NEWSLETTER
(
    COD_TIPO		  			INT NOT NULL 	AUTO_INCREMENT,
    NOME    		        	VARCHAR(100) 	NOT NULL,
    DTH_CRIACAO               	DATETIME        DEFAULT CURRENT_TIMESTAMP,   
    STATUS 					    CHAR(1) 	   	NOT NULL, 
    PRIMARY KEY (COD_TIPO)
);

ALTER TABLE TIPO_NEWSLETTER AUTO_INCREMENT = 1000;

/***********************************************************  
NEWSLETTER_EMAIL
***********************************************************/
CREATE TABLE EMAIL_NEWSLETTER
(
    COD_EMAIL		  			INT NOT NULL 	AUTO_INCREMENT,
    COD_TIPO     				INT             NOT NULL,
    NOME    		        	VARCHAR(100) 	NOT NULL,
    DTH_CRIACAO              	DATETIME        DEFAULT CURRENT_TIMESTAMP,   
    STATUS 					    CHAR(1) 	   	NOT NULL, 
    PRIMARY KEY (COD_EMAIL),
	FOREIGN KEY (COD_TIPO) REFERENCES TIPO_NEWSLETTER(COD_TIPO)
);

ALTER TABLE EMAIL_NEWSLETTER AUTO_INCREMENT = 1000;

/***********************************************************  
NEWSLETTER_ENVIO
***********************************************************/
CREATE TABLE ENVIO_NEWSLETTER
(
    COD_ENVIO		  			INT NOT NULL 	AUTO_INCREMENT,
    COD_TIPO					INT             NOT NULL,
    NOME    		    	   	VARCHAR(100) 	NOT NULL,
	HTML_BODY    		        TEXT 			NULL,
    HTML_LINK              		VARCHAR(1000) 	NULL,
	DTH_ENVIO               	DATETIME     	NULL, 
	DTH_CRIACAO              	DATETIME        DEFAULT CURRENT_TIMESTAMP,   
	EXI_DESCADASTRAR           	CHAR(1)     	NULL, 
    TOTAL_ENVIADOS   			INT             DEFAULT 0,
    STATUS 					    CHAR(1) 	   	NOT NULL, 
    PRIMARY KEY (COD_ENVIO),
	FOREIGN KEY (COD_TIPO) REFERENCES TIPO_NEWSLETTER(COD_TIPO)
);

ALTER TABLE ENVIO_NEWSLETTER AUTO_INCREMENT = 1000;

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
TIPO_CONTATO
***********************************************************/
CREATE TABLE TIPO_CONTATO
(
    COD_TIPO_CONTATO  				INT NOT NULL 	AUTO_INCREMENT,
	NOME    		    			VARCHAR(100) 	NOT NULL,
	DESCRICAO    		        	VARCHAR(200) 	NULL,
	STATUS							CHAR(1)         NOT NULL,
    PRIMARY KEY (COD_TIPO_CONTATO)
);

ALTER TABLE TIPO_CONTATO AUTO_INCREMENT = 1000;

/***********************************************************  
CONTATO
***********************************************************/
CREATE TABLE CONTATO
(
    COD_CONTATO		  			INT NOT NULL 	AUTO_INCREMENT,
	COD_TIPO_CONTATO  			INT 			DEFAULT NULL,  
	NOME    		    		VARCHAR(100) 	NOT NULL,
    EMAIL    		    		VARCHAR(100) 	NOT NULL,
	DESCRICAO    		        TEXT 			NULL,
	DTH_CONTATO   				DATETIME        DEFAULT CURRENT_TIMESTAMP,  
	DTH_LEITURA   				DATETIME        DEFAULT NULL, 
	STATUS						CHAR(1)         NOT NULL,
    PRIMARY KEY (COD_CONTATO)
);

ALTER TABLE CONTATO AUTO_INCREMENT = 1000;

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
INBOX
***********************************************************/
CREATE TABLE INBOX
(
	COD_INBOX		  			INT NOT NULL 	AUTO_INCREMENT,
    COD_USUARIO		  			INT NOT NULL,
    COD_USUARIO_REMETENTE		INT NOT NULL,
    COD_LOCAL					INT NOT NULL,   
    TITULO						VARCHAR(100) 	NULL,
    MENSAGEM					TEXT 			NULL,
	DTH_ENVIO   				DATETIME        DEFAULT CURRENT_TIMESTAMP, 
	DTH_LEITURA   				DATETIME        DEFAULT CURRENT_TIMESTAMP, 
    PRIMARY KEY (COD_INBOX),
    FOREIGN KEY (COD_USUARIO) REFERENCES USUARIO(COD_USUARIO)
);

ALTER TABLE INBOX AUTO_INCREMENT = 1000;

/***********************************************************  
PAIS
***********************************************************/
CREATE TABLE PAIS
(
	COD_PAIS		  			INT NOT NULL 	AUTO_INCREMENT,
    NOME	  					VARCHAR(100) 	NULL,
    PRIMARY KEY (COD_PAIS)
);

ALTER TABLE PAIS AUTO_INCREMENT = 1000;

INSERT INTO Pais (NOME) VALUES ('Afeganistão');
INSERT INTO Pais (NOME) VALUES ('África do Sul');
INSERT INTO Pais (NOME) VALUES ('Akrotiri');
INSERT INTO Pais (NOME) VALUES ('Albânia');
INSERT INTO Pais (NOME) VALUES ('Alemanha');
INSERT INTO Pais (NOME) VALUES ('Andorra');
INSERT INTO Pais (NOME) VALUES ('Angola');
INSERT INTO Pais (NOME) VALUES ('Anguila');
INSERT INTO Pais (NOME) VALUES ('Antárctida');
INSERT INTO Pais (NOME) VALUES ('Antígua e Barbuda');
INSERT INTO Pais (NOME) VALUES ('Antilhas Neerlandesas');
INSERT INTO Pais (NOME) VALUES ('Arábia Saudita');
INSERT INTO Pais (NOME) VALUES ('Arctic Ocean');
INSERT INTO Pais (NOME) VALUES ('Argélia');
INSERT INTO Pais (NOME) VALUES ('Argentina');
INSERT INTO Pais (NOME) VALUES ('Arménia');
INSERT INTO Pais (NOME) VALUES ('Aruba');
INSERT INTO Pais (NOME) VALUES ('Ashmore and Cartier Islands');
INSERT INTO Pais (NOME) VALUES ('Atlantic Ocean');
INSERT INTO Pais (NOME) VALUES ('Austrália');
INSERT INTO Pais (NOME) VALUES ('Áustria');
INSERT INTO Pais (NOME) VALUES ('Azerbaijão');
INSERT INTO Pais (NOME) VALUES ('Baamas');
INSERT INTO Pais (NOME) VALUES ('Bangladeche');
INSERT INTO Pais (NOME) VALUES ('Barbados');
INSERT INTO Pais (NOME) VALUES ('Barém');
INSERT INTO Pais (NOME) VALUES ('Bélgica');
INSERT INTO Pais (NOME) VALUES ('Belize');
INSERT INTO Pais (NOME) VALUES ('Benim');
INSERT INTO Pais (NOME) VALUES ('Bermudas');
INSERT INTO Pais (NOME) VALUES ('Bielorrússia');
INSERT INTO Pais (NOME) VALUES ('Birmânia');
INSERT INTO Pais (NOME) VALUES ('Bolívia');
INSERT INTO Pais (NOME) VALUES ('Bósnia e Herzegovina');
INSERT INTO Pais (NOME) VALUES ('Botsuana');
INSERT INTO Pais (NOME) VALUES ('Brasil');
INSERT INTO Pais (NOME) VALUES ('Brunei');
INSERT INTO Pais (NOME) VALUES ('Bulgária');
INSERT INTO Pais (NOME) VALUES ('Burquina Faso');
INSERT INTO Pais (NOME) VALUES ('Burúndi');
INSERT INTO Pais (NOME) VALUES ('Butão');
INSERT INTO Pais (NOME) VALUES ('Cabo Verde');
INSERT INTO Pais (NOME) VALUES ('Camarões');
INSERT INTO Pais (NOME) VALUES ('Camboja');
INSERT INTO Pais (NOME) VALUES ('Canadá');
INSERT INTO Pais (NOME) VALUES ('Catar');
INSERT INTO Pais (NOME) VALUES ('Cazaquistão');
INSERT INTO Pais (NOME) VALUES ('Chade');
INSERT INTO Pais (NOME) VALUES ('Chile');
INSERT INTO Pais (NOME) VALUES ('China');
INSERT INTO Pais (NOME) VALUES ('Chipre');
INSERT INTO Pais (NOME) VALUES ('Clipperton Island');
INSERT INTO Pais (NOME) VALUES ('Colômbia');
INSERT INTO Pais (NOME) VALUES ('Comores');
INSERT INTO Pais (NOME) VALUES ('Congo-Brazzaville');
INSERT INTO Pais (NOME) VALUES ('Congo-Kinshasa');
INSERT INTO Pais (NOME) VALUES ('Coral Sea Islands');
INSERT INTO Pais (NOME) VALUES ('Coreia do Norte');
INSERT INTO Pais (NOME) VALUES ('Coreia do Sul');
INSERT INTO Pais (NOME) VALUES ('Costa do Marfim');
INSERT INTO Pais (NOME) VALUES ('Costa Rica');
INSERT INTO Pais (NOME) VALUES ('Croácia');
INSERT INTO Pais (NOME) VALUES ('Cuba');
INSERT INTO Pais (NOME) VALUES ('Dhekelia');
INSERT INTO Pais (NOME) VALUES ('Dinamarca');
INSERT INTO Pais (NOME) VALUES ('Domínica');
INSERT INTO Pais (NOME) VALUES ('Egipto');
INSERT INTO Pais (NOME) VALUES ('Emiratos Árabes Unidos');
INSERT INTO Pais (NOME) VALUES ('Equador');
INSERT INTO Pais (NOME) VALUES ('Eritreia');
INSERT INTO Pais (NOME) VALUES ('Eslováquia');
INSERT INTO Pais (NOME) VALUES ('Eslovénia');
INSERT INTO Pais (NOME) VALUES ('Espanha');
INSERT INTO Pais (NOME) VALUES ('Estados Unidos');
INSERT INTO Pais (NOME) VALUES ('Estónia');
INSERT INTO Pais (NOME) VALUES ('Etiópia');
INSERT INTO Pais (NOME) VALUES ('Faroé');
INSERT INTO Pais (NOME) VALUES ('Fiji');
INSERT INTO Pais (NOME) VALUES ('Filipinas');
INSERT INTO Pais (NOME) VALUES ('Finlândia');
INSERT INTO Pais (NOME) VALUES ('França');
INSERT INTO Pais (NOME) VALUES ('Gabão');
INSERT INTO Pais (NOME) VALUES ('Gâmbia');
INSERT INTO Pais (NOME) VALUES ('Gana');
INSERT INTO Pais (NOME) VALUES ('Gaza Strip');
INSERT INTO Pais (NOME) VALUES ('Geórgia');
INSERT INTO Pais (NOME) VALUES ('Geórgia do Sul e Sandwich do Sul');
INSERT INTO Pais (NOME) VALUES ('Gibraltar');
INSERT INTO Pais (NOME) VALUES ('Granada');
INSERT INTO Pais (NOME) VALUES ('Grécia');
INSERT INTO Pais (NOME) VALUES ('Gronelândia');
INSERT INTO Pais (NOME) VALUES ('Guame');
INSERT INTO Pais (NOME) VALUES ('Guatemala');
INSERT INTO Pais (NOME) VALUES ('Guernsey');
INSERT INTO Pais (NOME) VALUES ('Guiana');
INSERT INTO Pais (NOME) VALUES ('Guiné');
INSERT INTO Pais (NOME) VALUES ('Guiné Equatorial');
INSERT INTO Pais (NOME) VALUES ('Guiné-Bissau');
INSERT INTO Pais (NOME) VALUES ('Haiti');
INSERT INTO Pais (NOME) VALUES ('Honduras');
INSERT INTO Pais (NOME) VALUES ('Hong Kong');
INSERT INTO Pais (NOME) VALUES ('Hungria');
INSERT INTO Pais (NOME) VALUES ('Iémen');
INSERT INTO Pais (NOME) VALUES ('Ilha Bouvet');
INSERT INTO Pais (NOME) VALUES ('Ilha do Natal');
INSERT INTO Pais (NOME) VALUES ('Ilha Norfolk');
INSERT INTO Pais (NOME) VALUES ('Ilhas Caimão');
INSERT INTO Pais (NOME) VALUES ('Ilhas Cook');
INSERT INTO Pais (NOME) VALUES ('Ilhas dos Cocos');
INSERT INTO Pais (NOME) VALUES ('Ilhas Falkland');
INSERT INTO Pais (NOME) VALUES ('Ilhas Heard e McDonald');
INSERT INTO Pais (NOME) VALUES ('Ilhas Marshall');
INSERT INTO Pais (NOME) VALUES ('Ilhas Salomão');
INSERT INTO Pais (NOME) VALUES ('Ilhas Turcas e Caicos');
INSERT INTO Pais (NOME) VALUES ('Ilhas Virgens Americanas');
INSERT INTO Pais (NOME) VALUES ('Ilhas Virgens Britânicas');
INSERT INTO Pais (NOME) VALUES ('Índia');
INSERT INTO Pais (NOME) VALUES ('Indian Ocean');
INSERT INTO Pais (NOME) VALUES ('Indonésia');
INSERT INTO Pais (NOME) VALUES ('Irão');
INSERT INTO Pais (NOME) VALUES ('Iraque');
INSERT INTO Pais (NOME) VALUES ('Irlanda');
INSERT INTO Pais (NOME) VALUES ('Islândia');
INSERT INTO Pais (NOME) VALUES ('Israel');
INSERT INTO Pais (NOME) VALUES ('Itália');
INSERT INTO Pais (NOME) VALUES ('Jamaica');
INSERT INTO Pais (NOME) VALUES ('Jan Mayen');
INSERT INTO Pais (NOME) VALUES ('Japão');
INSERT INTO Pais (NOME) VALUES ('Jersey');
INSERT INTO Pais (NOME) VALUES ('Jibuti');
INSERT INTO Pais (NOME) VALUES ('Jordânia');
INSERT INTO Pais (NOME) VALUES ('Kuwait');
INSERT INTO Pais (NOME) VALUES ('Laos');
INSERT INTO Pais (NOME) VALUES ('Lesoto');
INSERT INTO Pais (NOME) VALUES ('Letónia');
INSERT INTO Pais (NOME) VALUES ('Líbano');
INSERT INTO Pais (NOME) VALUES ('Libéria');
INSERT INTO Pais (NOME) VALUES ('Líbia');
INSERT INTO Pais (NOME) VALUES ('Listenstaine');
INSERT INTO Pais (NOME) VALUES ('Lituânia');
INSERT INTO Pais (NOME) VALUES ('Luxemburgo');
INSERT INTO Pais (NOME) VALUES ('Macau');
INSERT INTO Pais (NOME) VALUES ('Macedónia');
INSERT INTO Pais (NOME) VALUES ('Madagáscar');
INSERT INTO Pais (NOME) VALUES ('Malásia');
INSERT INTO Pais (NOME) VALUES ('Malávi');
INSERT INTO Pais (NOME) VALUES ('Maldivas');
INSERT INTO Pais (NOME) VALUES ('Mali');
INSERT INTO Pais (NOME) VALUES ('Malta');
INSERT INTO Pais (NOME) VALUES ('Isle of Man');
INSERT INTO Pais (NOME) VALUES ('Marianas do Norte');
INSERT INTO Pais (NOME) VALUES ('Marrocos');
INSERT INTO Pais (NOME) VALUES ('Maurícia');
INSERT INTO Pais (NOME) VALUES ('Mauritânia');
INSERT INTO Pais (NOME) VALUES ('Mayotte');
INSERT INTO Pais (NOME) VALUES ('México');
INSERT INTO Pais (NOME) VALUES ('Micronésia');
INSERT INTO Pais (NOME) VALUES ('Moçambique');
INSERT INTO Pais (NOME) VALUES ('Moldávia');
INSERT INTO Pais (NOME) VALUES ('Mónaco');
INSERT INTO Pais (NOME) VALUES ('Mongólia');
INSERT INTO Pais (NOME) VALUES ('Monserrate');
INSERT INTO Pais (NOME) VALUES ('Montenegro');
INSERT INTO Pais (NOME) VALUES ('Mundo');
INSERT INTO Pais (NOME) VALUES ('Namíbia');
INSERT INTO Pais (NOME) VALUES ('Nauru');
INSERT INTO Pais (NOME) VALUES ('Navassa Island');
INSERT INTO Pais (NOME) VALUES ('Nepal');
INSERT INTO Pais (NOME) VALUES ('Nicarágua');
INSERT INTO Pais (NOME) VALUES ('Níger');
INSERT INTO Pais (NOME) VALUES ('Nigéria');
INSERT INTO Pais (NOME) VALUES ('Niue');
INSERT INTO Pais (NOME) VALUES ('Noruega');
INSERT INTO Pais (NOME) VALUES ('Nova Caledónia');
INSERT INTO Pais (NOME) VALUES ('Nova Zelândia');
INSERT INTO Pais (NOME) VALUES ('Omã');
INSERT INTO Pais (NOME) VALUES ('Pacific Ocean');
INSERT INTO Pais (NOME) VALUES ('Países Baixos');
INSERT INTO Pais (NOME) VALUES ('Palau');
INSERT INTO Pais (NOME) VALUES ('Panamá');
INSERT INTO Pais (NOME) VALUES ('Papua-Nova Guiné');
INSERT INTO Pais (NOME) VALUES ('Paquistão');
INSERT INTO Pais (NOME) VALUES ('Paracel Islands');
INSERT INTO Pais (NOME) VALUES ('Paraguai');
INSERT INTO Pais (NOME) VALUES ('Peru');
INSERT INTO Pais (NOME) VALUES ('Pitcairn');
INSERT INTO Pais (NOME) VALUES ('Polinésia Francesa');
INSERT INTO Pais (NOME) VALUES ('Polónia');
INSERT INTO Pais (NOME) VALUES ('Porto Rico');
INSERT INTO Pais (NOME) VALUES ('Portugal');
INSERT INTO Pais (NOME) VALUES ('Quénia');
INSERT INTO Pais (NOME) VALUES ('Quirguizistão');
INSERT INTO Pais (NOME) VALUES ('Quiribáti');
INSERT INTO Pais (NOME) VALUES ('Reino Unido');
INSERT INTO Pais (NOME) VALUES ('República Centro-Africana');
INSERT INTO Pais (NOME) VALUES ('República Checa');
INSERT INTO Pais (NOME) VALUES ('República Dominicana');
INSERT INTO Pais (NOME) VALUES ('Roménia');
INSERT INTO Pais (NOME) VALUES ('Ruanda');
INSERT INTO Pais (NOME) VALUES ('Rússia');
INSERT INTO Pais (NOME) VALUES ('Salvador');
INSERT INTO Pais (NOME) VALUES ('Samoa');
INSERT INTO Pais (NOME) VALUES ('Samoa Americana');
INSERT INTO Pais (NOME) VALUES ('Santa Helena');
INSERT INTO Pais (NOME) VALUES ('Santa Lúcia');
INSERT INTO Pais (NOME) VALUES ('São Cristóvão e Neves');
INSERT INTO Pais (NOME) VALUES ('São Marinho');
INSERT INTO Pais (NOME) VALUES ('São Pedro e Miquelon');
INSERT INTO Pais (NOME) VALUES ('São Tomé e Príncipe');
INSERT INTO Pais (NOME) VALUES ('São Vicente e Granadinas');
INSERT INTO Pais (NOME) VALUES ('Sara Ocidental');
INSERT INTO Pais (NOME) VALUES ('Seicheles');
INSERT INTO Pais (NOME) VALUES ('Senegal');
INSERT INTO Pais (NOME) VALUES ('Serra Leoa');
INSERT INTO Pais (NOME) VALUES ('Sérvia');
INSERT INTO Pais (NOME) VALUES ('Singapura');
INSERT INTO Pais (NOME) VALUES ('Síria');
INSERT INTO Pais (NOME) VALUES ('Somália');
INSERT INTO Pais (NOME) VALUES ('Southern Ocean');
INSERT INTO Pais (NOME) VALUES ('Spratly Islands');
INSERT INTO Pais (NOME) VALUES ('Sri Lanca');
INSERT INTO Pais (NOME) VALUES ('Suazilândia');
INSERT INTO Pais (NOME) VALUES ('Sudão');
INSERT INTO Pais (NOME) VALUES ('Suécia');
INSERT INTO Pais (NOME) VALUES ('Suíça');
INSERT INTO Pais (NOME) VALUES ('Suriname');
INSERT INTO Pais (NOME) VALUES ('Svalbard e Jan Mayen');
INSERT INTO Pais (NOME) VALUES ('Tailândia');
INSERT INTO Pais (NOME) VALUES ('Taiwan');
INSERT INTO Pais (NOME) VALUES ('Tajiquistão');
INSERT INTO Pais (NOME) VALUES ('Tanzânia');
INSERT INTO Pais (NOME) VALUES ('Território Britânico do Oceano Índico');
INSERT INTO Pais (NOME) VALUES ('Territórios Austrais Franceses');
INSERT INTO Pais (NOME) VALUES ('Timor Leste');
INSERT INTO Pais (NOME) VALUES ('Togo');
INSERT INTO Pais (NOME) VALUES ('Tokelau');
INSERT INTO Pais (NOME) VALUES ('Tonga');
INSERT INTO Pais (NOME) VALUES ('Trindade e Tobago');
INSERT INTO Pais (NOME) VALUES ('Tunísia');
INSERT INTO Pais (NOME) VALUES ('Turquemenistão');
INSERT INTO Pais (NOME) VALUES ('Turquia');
INSERT INTO Pais (NOME) VALUES ('Tuvalu');
INSERT INTO Pais (NOME) VALUES ('Ucrânia');
INSERT INTO Pais (NOME) VALUES ('Uganda');
INSERT INTO Pais (NOME) VALUES ('União Europeia');
INSERT INTO Pais (NOME) VALUES ('Uruguai');
INSERT INTO Pais (NOME) VALUES ('Usbequistão');
INSERT INTO Pais (NOME) VALUES ('Vanuatu');
INSERT INTO Pais (NOME) VALUES ('Vaticano');
INSERT INTO Pais (NOME) VALUES ('Venezuela');
INSERT INTO Pais (NOME) VALUES ('Vietname');
INSERT INTO Pais (NOME) VALUES ('Wake Island');
INSERT INTO Pais (NOME) VALUES ('Wallis e Futuna');
INSERT INTO Pais (NOME) VALUES ('West Bank');
INSERT INTO Pais (NOME) VALUES ('Zâmbia');
INSERT INTO Pais (NOME) VALUES ('Zimbabué');
COMMIT;

/***********************************************************  
ESTADO
***********************************************************/
CREATE TABLE ESTADO
(
	COD_ESTADO		  			INT NOT NULL 	AUTO_INCREMENT,
	COD_PAIS		  			INT 			NOT NULL,
	SIGLA			  			VARCHAR(2)    NOT NULL,	
    NOME	  					VARCHAR(100) 	NULL,
    PRIMARY KEY (COD_ESTADO),
    FOREIGN KEY (COD_PAIS) REFERENCES PAIS(COD_PAIS)
);

ALTER TABLE ESTADO AUTO_INCREMENT = 1000;

﻿INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('AC',1035,'Acre');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('AL',1035,'Alagoas');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('AM',1035,'Amazonas');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('AP',1035,'Amapá');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('BA',1035,'Bahia');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('CE',1035,'Ceará');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('DF',1035,'Brasília (Distrito Federal)');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('ES',1035,'Espirito Santo');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('GO',1035,'Goias');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('MA',1035,'Maranhão');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('MG',1035,'Minas Gerais');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('MS',1035,'Mato Grosso do Sul');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('MT',1035,'Mato Grosso');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('PA',1035,'Pará');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('PB',1035,'Paraíba');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('PE',1035,'Pernambuco');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('PI',1035,'Piauí');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('PR',1035,'Paraná');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('RJ',1035,'Rio de Janeiro');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('RN',1035,'Rio Grande do Norte');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('RO',1035,'Rondônia');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('RR',1035,'Rorâima');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('RS',1035,'Rio Grande do Sul');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('SC',1035,'Santa Catarina');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('SE',1035,'Sergipe');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('SP',1035,'São Paulo');
INSERT INTO Estado (SIGLA,COD_PAIS,NOME) VALUES ('TO',1035,'Tocantins');
COMMIT;

/***********************************************************  
CIDADE
***********************************************************/
CREATE TABLE CIDADE
(
	COD_CIDADE		  			INT NOT NULL 	AUTO_INCREMENT,
	COD_ESTADO		  			INT 			NOT NULL,
    NOME	  					VARCHAR(100) 	NULL,
    PRIMARY KEY (COD_CIDADE),
    FOREIGN KEY (COD_ESTADO) REFERENCES ESTADO(COD_ESTADO)
);

ALTER TABLE CIDADE AUTO_INCREMENT = 1000;

/***********************************************************  
REGIAO
***********************************************************/
CREATE TABLE REGIAO
(
	COD_REGIAO		  			INT NOT NULL 	AUTO_INCREMENT,
	COD_PAIS		  			INT 			NOT NULL,
    NOME	  					VARCHAR(100) 	NULL,
    PRIMARY KEY (COD_REGIAO),
    FOREIGN KEY (COD_PAIS) REFERENCES PAIS(COD_PAIS)
);

ALTER TABLE REGIAO AUTO_INCREMENT = 1000;

INSERT INTO REGIAO (COD_PAIS,NOME) VALUES (1035,'Centro-Oeste');
INSERT INTO REGIAO (COD_PAIS,NOME) VALUES (1035,'Nordeste');
INSERT INTO REGIAO (COD_PAIS,NOME) VALUES (1035,'Norte');
INSERT INTO REGIAO (COD_PAIS,NOME) VALUES (1035,'Sul');
INSERT INTO REGIAO (COD_PAIS,NOME) VALUES (1035,'Sudeste');
COMMIT;

/***********************************************************  
BANCO
***********************************************************/
CREATE TABLE BANCO
(
	COD_BANCO		  			INT NOT NULL 	AUTO_INCREMENT,
    NOME	  					VARCHAR(100) 	NOT NULL,
	WEBSITE			  			VARCHAR(100) 	NULL,
	SIGLA			  			VARCHAR(100) 	NULL,
	FLAG_TIPO		  			CHAR(1) 		NULL,
	STATUS	  					CHAR(1) 		NOT NULL,
    PRIMARY KEY (COD_BANCO)
);

ALTER TABLE BANCO AUTO_INCREMENT = 1000;

/***********************************************************  
CEDENTE
***********************************************************/
CREATE TABLE CEDENTE
(
	COD_CEDENTE		  			INT NOT NULL 	AUTO_INCREMENT,
	COD_BANCO					INT 			NOT NULL,
    NOME	  					VARCHAR(100) 	NOT NULL,
    ENDERECO					VARCHAR(100) 	NOT NULL,
	NUMERO						NUMERIC(5)      DEFAULT 0,
	BAIRRO						VARCHAR(100) 	NOT NULL,
	CIDADE						VARCHAR(100) 	NOT NULL,
	UF							CHAR(2) 		NOT NULL,
	CEP							VARCHAR(10) 	NULL,	
	DTH_NASCIMENTO				DATETIME        NOT NULL,    
	CPF_CNPJ					VARCHAR(30) 	NULL,
	INSTRUCAO_BOLETO			VARCHAR(300) 	NULL,
	NUM_AGENCIA					VARCHAR(10) 	NOT NULL,
	NUM_CONTA_CORRENTE			VARCHAR(10) 	NOT NULL,
	STATUS	  					CHAR(1) 		NOT NULL,
    PRIMARY KEY (COD_CEDENTE),
    FOREIGN KEY (COD_BANCO) REFERENCES BANCO(COD_BANCO)
);

ALTER TABLE CEDENTE AUTO_INCREMENT = 1000;