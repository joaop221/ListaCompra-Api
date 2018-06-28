CREATE DATABASE ListaCompra
GO

USE ListaCompra
GO

CREATE TABLE perfil
(
	Id_Perfil int NOT NULL,
	Perfil nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_PERFIL PRIMARY KEY(Id_Perfil)
);


CREATE TABLE grupo
(
	Id_Grupo int IDENTITY(1,1) NOT NULL,
	Nome nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_GRUPO PRIMARY KEY(Id_Grupo)
);


CREATE TABLE provedor
(
	Id_Provedor int NOT NULL,
	Provedor nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_PROVEDOR PRIMARY KEY(Id_Provedor)
);


CREATE TABLE preferencia
(
	Id_Preferencia int NOT NULL,
	Preferencia nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_PREFERENCIA PRIMARY KEY(Id_Preferencia)
);



CREATE TABLE categoria
(
	Id_Categoria int NOT NULL,
	Categoria nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_CATEGORIA PRIMARY KEY(Id_Categoria)
);


CREATE TABLE tstatus
(
	Id_Status int NOT NULL,
	tstatus nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_STATUS PRIMARY KEY(Id_Status)
);


CREATE TABLE unidade
(
	Id_Unidade int NOT NULL,
	Unidade nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CONSTRAINT PK_TB_UNIDADE PRIMARY KEY(Id_Unidade)
);

CREATE TABLE usuario
(
	Id_Usuario int IDENTITY(1,1) NOT NULL,
	Id_Preferencia int NOT NULL,
	Nickname nvarchar(100) NOT NULL,
	Nome nvarchar(100) NOT NULL,
	Imagem nvarchar(100) NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	CONSTRAINT PK_TB_USUARIO PRIMARY KEY(Id_Usuario),
	CONSTRAINT FK_TB_USUARIO_TB_PREFERENCIA FOREIGN KEY(id_preferencia) REFERENCES preferencia(id_preferencia)
);


CREATE TABLE usuario_helper
(
	Id_Usuario int NOT NULL,
	Id_Provedor int NOT NULL,
	Hash_senha nvarchar(255) NOT NULL,
	Hash_Provedor nvarchar(255) NOT NULL,
	Email nvarchar(100) NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_USUARIO_HELPER PRIMARY KEY(Id_Usuario),
	CONSTRAINT FK_TB_USUARIO_HELPER_TB_USUARIO FOREIGN KEY(Id_Usuario) REFERENCES usuario (Id_Usuario),
	CONSTRAINT FK_TB_USUARIO_HELPER_TB_PROVEDOR FOREIGN KEY(Id_Provedor) REFERENCES provedor (Id_Provedor)
);




CREATE TABLE notificacoes
(
	Id_Notificacao int IDENTITY(1,1) NOT NULL,
	Texto nvarchar(255) NOT NULL,
	Titulo nvarchar(100) NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_NOTIFICACAO PRIMARY KEY(Id_Notificacao)
);


CREATE TABLE notif_user
(
	Id_Usuario int NOT NULL,
	Id_Notificacao int NOT NULL,
	Enviado BIT NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_NOTIF_USER PRIMARY KEY(Id_Usuario, Id_Notificacao),
	CONSTRAINT FK_TB_NOTIF_USER_TB_USUARIO FOREIGN KEY(Id_Usuario) REFERENCES usuario (Id_Usuario),
	CONSTRAINT FK_TB_NOTIF_USER_TB_NOTIFICACAO FOREIGN KEY(Id_Notificacao) REFERENCES notificacoes (Id_Notificacao)
);



CREATE TABLE usuario_grupo
(
	Id_Grupo int NOT NULL,
	Id_Perfil int NOT NULL,
	Id_Usuario int NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_USUARIO_GRUPO PRIMARY KEY(Id_Grupo, Id_Perfil,Id_Usuario),
	CONSTRAINT FK_TB_USUARIO_GRUPO_TB_GRUPO FOREIGN KEY(Id_Grupo) REFERENCES grupo (Id_Grupo),
	CONSTRAINT FK_TB_USUARIO_GRUPO_TB_PERFIL FOREIGN KEY(Id_Perfil) REFERENCES perfil (Id_Perfil),
	CONSTRAINT FK_TB_USUARIO_GRUPO_TB_USUARIO FOREIGN KEY(Id_Usuario) REFERENCES usuario (Id_Usuario)

);


CREATE TABLE lista
(
	Id_Lista int IDENTITY(1,1) NOT NULL,
	Id_Grupo int NOT NULL,
	Nome nvarchar(100) NOT NULL,
	Descricao nvarchar(100) NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_LISTA PRIMARY KEY(Id_Lista),
	CONSTRAINT FK_TB_LISTA_TB_GRUPO FOREIGN KEY(Id_Grupo) REFERENCES grupo (Id_Grupo)
);



CREATE TABLE produtos
(
	Id_Produtos int IDENTITY(1,1) NOT NULL,
	Id_Categoria int NOT NULL,
	Id_Grupo int,
	Nome nvarchar(100) NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_PRODUTOS PRIMARY KEY(Id_Produtos),
	CONSTRAINT FK_TB_PRODUTOS_TB_CATEGORIA FOREIGN KEY(Id_Categoria) REFERENCES categoria (Id_Categoria),
	CONSTRAINT FK_TB_PRODUTOS_TB_GRUPO FOREIGN KEY(Id_Grupo) REFERENCES grupo (Id_Grupo)
);



CREATE TABLE lista_produtos
(
	Id_Lista int NOT NULL,
	Id_Produtos int NOT NULL,
	Id_Status int NOT NULL,
	Id_Unidade int NOT NULL,
	quantidade int NOT NULL,
	estimado_uni float NOT NULL,
	CreateDate datetime NOT NULL DEFAULT ((getdate())),
	EditDate datetime NOT NULL DEFAULT ((getdate())),
	UserCreate int NOT NULL,
	UserEdit int NOT NULL,
	CONSTRAINT PK_TB_LISTA_PRODUTOS PRIMARY KEY(Id_Lista, Id_Produtos),
	CONSTRAINT FK_TB_LISTA_PRODUTOS_TB_LISTA FOREIGN KEY(Id_Lista) REFERENCES lista (Id_Lista),
	CONSTRAINT FK_TB_LISTA_PRODUTOS_TB_PRODUTOS FOREIGN KEY(Id_Produtos) REFERENCES produtos (Id_Produtos),
	CONSTRAINT FK_TB_LISTA_PRODUTOS_TB_STATUS FOREIGN KEY(Id_Status) REFERENCES tstatus (Id_Status),
	CONSTRAINT FK_TB_LISTA_PRODUTOS_TB_UNIDADE FOREIGN KEY(Id_Unidade) REFERENCES unidade (Id_Unidade)
);

CREATE TABLE token_auth
(
	Id_Token int IDENTITY(1,1) NOT NULL,
	Id_Usuario int NOT NULL,
	Descricao nvarchar(50) NOT NULL,
	Token char(5000) NOT NULL,
	DataExp timestamp NOT NULL 
	CONSTRAINT PK_TB_TOKEN_AUTH PRIMARY KEY(Id_Token),
	CONSTRAINT FK_TB_TOKEN_AUTH_TB_USUARIO FOREIGN KEY(Id_usuario) REFERENCES usuario (Id_usuario),
);

