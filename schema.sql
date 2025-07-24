CREATE DATABASE IF NOT EXISTS chronolabel;

USE chronolabel;

CREATE TABLE USUARIOS(
    cpf CHAR(11) PRIMARY KEY,
    nome VARCHAR(20) NOT NULL,
    senha VARCHAR(60) NOT NULL,
    tipo ENUM('operador', 'administrador') NOT NULL
);

CREATE TABLE PRODUTOS(
    id_produto VARCHAR(255) PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    peso INT NOT NULL,
    quantidade INT NOT NULL
);

CREATE TABLE RELATORIOS(
    id_relatorio INT AUTO_INCREMENT PRIMARY KEY,
    cpf_usuario CHAR(11) NOT NULL,
    id_produto VARCHAR(255) NOT NULL,
    quantidade_produto_operado INT NOT NULL,
    data_criacao DATETIME NOT NULL,
    data_termino DATETIME NOT NULL,
    duracao TIME NOT NULL,
    FOREIGN KEY (cpf_usuario) REFERENCES USUARIOS(cpf) ON DELETE CASCADE,
    FOREIGN KEY (id_produto) REFERENCES PRODUTOS(id_produto) ON DELETE CASCADE,
    INDEX idx_cpf_usuario (cpf_usuario),
    INDEX idx_id_produto (id_produto)
);
